using System.Collections.Generic;
using System.IO;
using System.Linq;
using Components;
using Extensions;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Utilities.Collections;

/// <summary>
/// Targets for running .NET tests
/// </summary>
[PublicAPI]
public interface IRunTests : IGitRepository, IDotNet, IPipeline
{
    /// <summary>
    /// The projects to run tests for
    /// </summary>
    IEnumerable<Project> Projects => Solution.AllProjects.Where(p => p.IsTest() && !p.IsSmokeTest() && !p.IsSystemTest());

    /// <summary>
    /// The directory where test results are published
    /// </summary>
    AbsolutePath PublishDirectory => TemporaryDirectory / "TestResults";

    bool PublishCodeCoverage => false;

    /// <summary>
    /// Run the tests
    /// </summary>
    Target RunTests => d => d
        .TryTriggeredBy<xBuild>(b => b.Build)
        .DependsOn(DotNetBuildSolution)
        .Requires(() => Projects != null)
        .Requires(() => PublishDirectory != null)
        .OnlyWhenStatic(() => IsLocalBuild || !Repository.IsOnMasterBranch())
        .Executes(() => Projects.ForEach(p => DotNetTest(p, PublishDirectory, c => PublishCodeCoverage ? c.EnableCollectCoverage() : c)));

    /// <summary>
    /// Publish the test results
    /// </summary>
    Target PublishTestResults => d => d
        .TriggeredBy(RunTests)
        .Requires(() => PublishDirectory != null)
        .OnlyWhenStatic(() => IsServerBuild && !Repository.IsOnMasterBranch())
        .OnlyWhenDynamic(() => HasTestResult)
        .AssuredAfterFailure()
        .Executes(() =>
        {
            var testResults = PublishDirectory.GlobFiles("**/*.trx").Select(file => Path.GetFullPath(file));
        });

    private bool HasTestResult => PublishDirectory.GlobFiles("**/*.trx").Any();
}
