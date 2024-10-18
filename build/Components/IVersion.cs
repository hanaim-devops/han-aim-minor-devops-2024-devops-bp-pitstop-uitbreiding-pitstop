using Conventions;
using Nuke.Common;
using Nuke.Common.CI.AzurePipelines;
using Nuke.Common.Tools.GitVersion;

namespace Components;

[ParameterPrefix("Version")]
public interface IVersion : INukeBuild
{
    [GitVersion(Framework = "net8.0", NoFetch = true, UpdateBuildNumber = false), Required]
    private GitVersion Versioning => TryGetValue(() => Versioning) ?? Versioning;

    private static readonly int Revision = (int)(AzurePipelines.Instance?.BuildId % short.MaxValue ?? 0);

    private Version CalendarVersion => new CalendarBasedVersion(Versioning, Revision);

    private Version GitVersion => new GitBasedVersion(Versioning, Revision);

    [Parameter] public VersioningStrategy Strategy => TryGetValue<VersioningStrategy?>(() => Strategy) ?? VersioningStrategy.Git;

    public Version Version => Strategy switch
    {
        VersioningStrategy.Calendar => CalendarVersion,
        _ => GitVersion
    };
}
