using System.Linq;
using Extensions;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;

namespace Components;

/// <summary>
/// Component for sharing .NET operations
/// </summary>
[PublicAPI]
public interface IDotNet : IConfiguration, IVersion, ISolution
{
    /// <summary>
    /// Clean dotnet project
    /// </summary>
    void DotNetClean(Project project) => project.Directory.GlobDirectories("**/bin", "**/obj").ForEach(path => path.DeleteDirectory());

    /// <summary>
    /// Restore dotnet project
    /// </summary>
    void DotNetRestore(Project project, Configure<DotNetRestoreSettings> configure = null) =>
        DotNetTasks.DotNetRestore(c =>
        {
            var settings = c.SetProjectFile(project);
            return configure?.Invoke(settings) ?? settings;
        });

    /// <summary>
    /// Build dotnet project
    /// </summary>
    void DotNetBuild(Project project, Configure<DotNetBuildSettings> configure = null)
    {
        DotNetClean(project);
        DotNetRestore(project);
        DotNetTasks.DotNetBuild(c =>
        {
            var settings = c
                .SetProjectFile(project)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .SetAssemblyVersion(Version.AssemblyVersion)
                .SetFileVersion(Version.FileVersion)
                .SetInformationalVersion(Version.InformationalVersion)
                .SetProperty("IncludeSourceRevisionInInformationalVersion", false);
            return configure?.Invoke(settings) ?? settings;
        });
    }

    /// <summary>
    /// Publish dotnet project
    /// </summary>
    void DotNetPublish(Project project, AbsolutePath outputDirectory, Configure<DotNetPublishSettings> configure = null) =>
        DotNetTasks.DotNetPublish(c =>
        {
            var settings = c
                .SetProject(project)
                .SetConfiguration(Configuration)
                .SetOutput(outputDirectory)
                .EnableNoBuild()
                .EnableNoRestore()
                .SetAssemblyVersion(Version.AssemblyVersion)
                .SetFileVersion(Version.FileVersion)
                .SetInformationalVersion(Version.InformationalVersion);

            return configure?.Invoke(settings) ?? settings;
        });

    /// <summary>
    /// Test dotnet project
    /// </summary>
    void DotNetTest(Project project, AbsolutePath outputDirectory, Configure<DotNetTestSettings> configure = null) =>
        DotNetTasks.DotNetTest(c =>
        {
            var settings = c
                .SetProjectFile(project)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .SetLoggers("trx")
                 //https://stackoverflow.com/questions/43469400/asp-net-core-the-configured-user-limit-128-on-the-number-of-inotify-instance
                .SetProcessEnvironmentVariable("DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE", "false")
                .SetProcessEnvironmentVariable("DOTNET_USE_POLLING_FILE_WATCHER", "true")
                .SetResultsDirectory(outputDirectory);
            return configure?.Invoke(settings) ?? settings;
        });

    /// <summary>
    /// Allows additional build settings when executing target <see cref="DotNetBuildSolution"/>
    /// </summary>
    public Configure<DotNetBuildSettings> ConfigureSolutionBuild => null;

    /// <summary>
    /// Target for building the entire .NET solution
    /// </summary>
    Target DotNetBuildSolution => d => d
        .TryAfter<xBuild>(b => b.Build)
        .Executes(() =>
        {
            Solution.Projects.Where(p => !p.IsBuild()).ForEach(DotNetClean);
            DotNetTasks.DotNetToolRestore();
            
            DotNetTasks.DotNetRestore(s => s.SetProjectFile(Solution).SetProcessExitHandler((settings, process) =>
            {
                if (process.ExitCode != 0 && process.Output.Any(o => o.Text.Contains("error NU1102") || o.Text.Contains("error NU1103")))
                {
                    Serilog.Log.Warning("Retrying restore with --no-cache");
                    DotNetTasks.DotNetRestore(settings.EnableNoCache().EnableProcessAssertZeroExitCode());
                    return;
                }

                process.AssertZeroExitCode();
            }));

            DotNetTasks.DotNetBuild(s =>
            {
                var settings = s
                    .SetProjectFile(Solution)
                    .SetConfiguration(Configuration)
                    .EnableNoRestore()
                    .SetAssemblyVersion(Version.AssemblyVersion)
                    .SetFileVersion(Version.FileVersion)
                    .SetInformationalVersion(Version.InformationalVersion)
                    .SetProperty("IncludeSourceRevisionInInformationalVersion", false);
                return ConfigureSolutionBuild?.Invoke(settings) ?? settings;
            });
        });
}
