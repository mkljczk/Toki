using Microsoft.Extensions.DependencyInjection;
using Toki.ActivityPub.Persistence.DatabaseContexts;
using Toki.ActivityPub.WebFinger;

namespace Toki.ActivityPub;

/// <summary>
/// Extensions for the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the ActivityPub services.
    /// </summary>
    /// <param name="collection">The collection to add them to.</param>
    public static IServiceCollection AddActivityPubServices(this IServiceCollection collection)
    {
        collection.AddDbContext<TokiDatabaseContext>();
        
        collection.AddHttpClient()
            .AddTransient<WebFingerResolver>();
        
        return collection;
    }
}