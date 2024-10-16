using Conventions;
using JetBrains.Annotations;
using Nuke.Common;

namespace Components;

/// <summary>
/// Build configuration (Release, Debug)
/// </summary>
[PublicAPI]
public interface IConfiguration : INukeBuild
{
    /// <summary>
    /// Parameter for the build configuration (Debug or Release)
    /// </summary>
    [Parameter("Build configuration (Debug or Release)")]
    Configuration Configuration => TryGetValue(() => Configuration) ??
                                   (IsLocalBuild ? Configuration.Debug : Configuration.Release);
}
