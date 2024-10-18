using System;
using JetBrains.Annotations;
using Nuke.Common.Tools.GitVersion;

namespace Conventions;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public abstract record Version(VersioningStrategy Strategy, int RevisionInput)
{
    public virtual     string FullVersion          => $"{Major}.{Minor}.{Patch}.{Revision}{Label}";
    public virtual     string AssemblyVersion      => $"{Major}.{Minor}.{Patch}";
    public virtual     string FileVersion          => $"{Major}.{Minor}.{Patch}";
    public virtual     string InformationalVersion => FullVersion;
    public abstract    string Major                { get; }
    public abstract    string Minor                { get; }
    public abstract    string Patch                { get; }
    public virtual     string Revision             => RevisionInput.ToString();
    protected abstract string Label                { get; }
}

public sealed record GitBasedVersion(GitVersion GitVersion, int RevisionInput) : Version(VersioningStrategy.Git, RevisionInput)
{
    public override    string Major => GitVersion.Major.ToString();
    public override    string Minor => GitVersion.Minor.ToString();
    public override    string Patch => GitVersion.Patch.ToString();
    protected override string Label => GitVersion.PreReleaseLabelWithDash;
}

public sealed record CalendarBasedVersion(GitVersion GitVersion, int RevisionInput) : Version(VersioningStrategy.Calendar, RevisionInput)
{
    public override    string Major       => CommitDate.Year.ToString();
    public override    string Minor       => CommitDate.Month.ToString("00");
    public override    string Patch       => CommitDate.Day.ToString("00");
    public override    string Revision    => RevisionInput.ToString("00000");
    public override    string FileVersion => $"{Major}.{Minor}.{Patch}.{GitVersion.Minor:00000}";
    protected override string Label       => GitVersion.PreReleaseLabelWithDash;

    private DateOnly CommitDate => DateOnly.Parse(GitVersion.CommitDate);
}

public enum VersioningStrategy
{
    Git,
    Calendar
}
