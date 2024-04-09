using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Toki.ActivityPub.Models;
using Toki.ActivityPub.Persistence.DatabaseContexts;
using Toki.ActivityPub.Persistence.Repositories;
using Toki.ActivityPub.Resolvers;
using Toki.ActivityStreams.Activities;
using Toki.ActivityStreams.Objects;
using Toki.HTTPSignatures;
using Toki.HTTPSignatures.Models;

namespace Toki.ActivityPub.Cryptography;

/// <summary>
/// The message validation service for ActivityPub messages.
/// </summary>
/// <param name="db">The database context.</param>
/// <param name="resolver">The AP resolver.</param>
/// <param name="validator">The HTTP signature validator.</param>
/// <param name="repo">The user repository.</param>
public class ActivityPubMessageValidationService(
    TokiDatabaseContext db,
    ActivityPubResolver resolver,
    HttpSignatureValidator validator,
    UserRepository repo,
    ILogger<ActivityPubMessageValidationService> logger)
{
    /// <summary>
    /// Validates a message.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="asObject">The object to validate.</param>
    /// <returns>Whether it's valid or not.</returns>
    public async ValueTask<bool> Validate(
        HttpRequest request,
        ASObject? asObject)
    {
        if (asObject is not ASActivity activity)
        {
            logger.LogWarning(
                $"Tried to validate an ASObject, but it's not an activity! Is it some type we don't support yet? {asObject?.Type}");
            return false;
        }

        var signature = Signature.FromHttpRequest(request);

        if (signature is null)
        {
            logger.LogWarning("Couldn't parse signature from HTTP headers!");
            return false;
        }
        
        var keypair = await FetchKeypair(
            signature,
            activity.Actor);

        if (keypair is null)
        {
            logger.LogWarning("Couldn't fetch the keypair!");
            return false;
        }
        
        return validator.Validate(
            signature,
            keypair.PublicKey);
    }

    /// <summary>
    /// Fetches a keypair for the given actor.
    /// </summary>
    /// <param name="sig"></param>
    /// <param name="actor"></param>
    /// <returns></returns>
    private async Task<Keypair?> FetchKeypair(
        Signature sig,
        ASObject actor)
    {
        var keypair = await db.Keypairs.Where(k => k.RemoteId == sig.KeyId)
            .Include(keypair => keypair.Owner)
            .FirstOrDefaultAsync();

        if (keypair is not null)
        {
            // Verify that the owner is actually the owner we want.
            if (keypair.Owner?.RemoteId != actor.Id)
                logger.LogWarning($"Actor {actor.Id} wanted to sign their request with a key for {keypair.Owner?.RemoteId}.");
            return keypair.Owner?.RemoteId != actor.Id ? null : keypair;
        }
        
        // Fetch the actor, if we don't have them.
        var resolvedActor = await resolver.Fetch<ASActor>(actor);
        if (resolvedActor?.PublicKey is null)
        {
            logger.LogWarning($"Actor {actor.Id} doesn't have a public key.");
            return null;
        }
        
        // Ensure the key id isn't fake.
        if (resolvedActor.PublicKey?.Id != sig.KeyId)
        {
            logger.LogWarning($"Key {resolvedActor.PublicKey?.Id} doesn't match the key from the signature! {sig.KeyId}.");
            return null;
        }
        
        var user = await repo.ImportFromActivityStreams(resolvedActor);
        return user?.Keypair;
    }
}