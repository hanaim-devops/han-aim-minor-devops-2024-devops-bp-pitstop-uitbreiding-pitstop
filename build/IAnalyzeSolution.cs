using System.IO;
using Components;
using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.ReSharper;
using Nuke.Common.Utilities;

public interface IAnalyzeSolution : IDotNet, IGitRepository
{
    /// <summary>
    /// Inspect code with ReSharper
    /// </summary>
    Target InspectCodeWithResharper => d => d
        .TryTriggeredBy<xBuild>(b => b.Build)
        .DependsOn(DotNetBuildSolution)
        .Executes(() =>
        {
            // fix issue when resharper selects the wrong msbuild tools for code inspection
            var sdk = DotNetTasks.DotNet("--version").StdToText();
            var inspectionFile = TemporaryDirectory / "Resharper" / "inspection_results.txt";

            ReSharperTasks.ReSharperInspectCode(s => s
                .SetTargetPath(Solution.Path)
                .SetOutput(inspectionFile)
                .SetDisableSettingsLayers(ReSharperSettingsLayers.SolutionPersonal)
                .SetSeverity(ReSharperSeverity.WARNING)
                .SetFormat(ReSharperFormat.Text)
                .SetVerbosity(ReSharperVerbosity.WARN)
                .SetDotNetCoreSdk(sdk)
                .SetProcessArgumentConfigurator(a => a.Add("--no-build"))
            );

            var output = File.ReadAllLines(inspectionFile);
            if (output.Length > 1)
            {
                Assert.Fail(output.JoinNewLine());
            }
        });
}
