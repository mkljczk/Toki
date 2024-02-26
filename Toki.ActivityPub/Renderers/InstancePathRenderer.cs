using Microsoft.Extensions.Options;
using Toki.ActivityPub.Configuration;
using Toki.ActivityPub.Models;

namespace Toki.ActivityPub.Renderers;

/// <summary>
/// A class that helps with rendering paths from within the instance.
/// </summary>
/// <param name="opts">The instance configuration options.</param>
public class InstancePathRenderer(
    IOptions<InstanceConfiguration> opts)
{
    /// <summary>
    /// Gets the path to an actor from a user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The path to the <see cref="Toki.ActivityStreams.Objects.ASActor"/> object on the server.</returns>
    public string GetPathToActor(User user) =>
        GetPathToActor(user.Handle);
    
    /// <summary>
    /// Gets the path to an actor from a handle.
    /// </summary>
    /// <param name="handle">The handle.</param>
    /// <returns>The path to the <see cref="Toki.ActivityStreams.Objects.ASActor"/> object on the server.</returns>
    public string GetPathToActor(string handle) =>
        $"https://{opts.Value.Domain}/users/{handle}";

    /// <summary>
    /// Gets the path to a follow on this server.
    /// </summary>
    /// <param name="followerRelation">The follow.</param>
    /// <returns>The path to the follow.</returns>
    public string GetPathToFollow(FollowerRelation followerRelation) =>
        $"https://{opts.Value.Domain}/follows/{followerRelation.Id}";
}