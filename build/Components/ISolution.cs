using Nuke.Common;
using Nuke.Common.ProjectModel;

namespace Components;

/// <summary>
/// Reference to the Solution
/// </summary>
public interface ISolution : INukeBuild
{
    /// <summary>
    /// The current solution
    /// </summary>
    [Solution]
    [Required]
    Solution Solution => TryGetValue(() => Solution);
}
