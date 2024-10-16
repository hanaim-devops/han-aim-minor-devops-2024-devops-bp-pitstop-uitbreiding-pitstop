using JetBrains.Annotations;
using Microsoft.Build.Evaluation;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;

namespace Components;

/// <summary>
/// Base component for a Saros build defining the default 'Build' and 'Deploy' target.
/// </summary>
[PublicAPI]
public abstract class xBuild : NukeBuild, IVersion
{
    /// <inheritdoc />
    protected xBuild()
    {
        NoLogo = true;
        ProcessTasks.DefaultLogOutput = Verbosity <= Verbosity.Normal;
        ProcessTasks.DefaultLogInvocation = Verbosity <= Verbosity.Normal;
        ProjectModelTasks.Initialize(); // fixes MSBuild locating issues
    }

    /// <inheritdoc />
    // protected override void OnBuildInitialized()
    // {
    //     Serilog.Log.Information("Nedap Common Nuke Components Version: {Version}", typeof(xBuild).Assembly.GetName().Version);
    //     From<IAzurePipeline>().Initialize();
    // }

    /// <summary>
    /// Gets the name of this build based on <see cref="ISolution"/>
    /// </summary>
    [CanBeNull]
    protected virtual string Name => From<ISolution>()?.Solution.Name;

    /// <summary>
    /// Default build target, used to trigger build targets in components
    /// </summary>
    public virtual Target Build => d => d
        .Executes(() =>
        {
            var artifactsDirectory = From<IArtifacts>()?.ArtifactsDirectory;

            // ReSharper disable once InvertIf
            if (artifactsDirectory != null)
            {
                artifactsDirectory.CreateDirectory();
                artifactsDirectory.CreateOrCleanDirectory();
            }
        });

    /// <summary>
    /// Default deploy target, used to trigger deploy targets in components
    /// </summary>
    public virtual Target Deploy => d => d
        .Executes(() =>
        {
        });
    
    /// <summary>
    /// Helper method to obtain a specific interface implementation
    /// </summary>
    protected T From<T>() where T : class, INukeBuild => this as T;

    private static void TriggerAssemblyResolution() => _ = new ProjectCollection();
}
