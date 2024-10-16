using Nuke.Common;
using Nuke.Common.Git;

namespace Components;

/// <summary>
/// Reference the git repository
/// </summary>
public interface IGitRepository : INukeBuild
{
    /// <summary>
    /// Reference to the git repo
    /// </summary>
    [GitRepository, Required]
    GitRepository Repository => TryGetValue(() => Repository);
}
