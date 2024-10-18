using System.Collections.Generic;
using JetBrains.Annotations;
using Nuke.Common.IO;
using Nuke.Common.CI.GitHubActions;

namespace Components;

/// <summary>
/// Component for adding and retrieving artifacts
/// </summary>
[PublicAPI]
public interface IArtifacts : IPipeline
{
    /// <summary>
    /// The artifacts directory (by default: artifacts)
    /// </summary>
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    /// <summary>
    /// The pipeline artifacts directory (used in non-local builds)
    /// </summary>
    AbsolutePath PipelineArtifactsDirectory => RootDirectory.Parent;

    /// <summary>
    /// Upload an archive as a build artifact
    /// </summary>
    void UploadArchiveArtifact(string artifactName, ArchiveFormat archiveFormat = ArchiveFormat.Zip)
    {
        GitHubActions.Instance?.WriteCommand(
            command: "upload-artifact",
            message: null,
            dictionaryConfigurator: dict =>
            {
                dict["name"] = artifactName;
                dict["path"] = AbsolutePath.Create(ArtifactsDirectory + $"/'{artifactName}.{archiveFormat.ToString().ToLowerInvariant()}'");
                return null;
            });
    }
        
    
    /// <summary>
    /// Get an archive file artifact by name
    /// </summary>
    AbsolutePath GetArchiveArtifact(string artifactName, ArchiveFormat archiveFormat = ArchiveFormat.Zip)
    {
        var extension = archiveFormat.ToString().ToLowerInvariant();
        var localArchive = ArtifactsDirectory / $"{artifactName}.{extension}";
        var serverArchive = PipelineArtifactsDirectory / artifactName / $"{artifactName}.{extension}";
        return IsLocalBuild ? localArchive : serverArchive;
    }
    
    // /// <summary>
    // /// Add and upload a directory as an artifact
    // /// </summary>
    // void AddDirectoryArtifact(string artifactName, AbsolutePath source)
    // {
    //     var target = ArtifactsDirectory / artifactName;
    //     source.Copy(target, ExistsPolicy.MergeAndOverwrite);
    //     AzurePipeline?.UploadArtifacts("", artifactName, target);
    // }

    /// <summary>
    /// Get a directory artifact by name
    /// </summary>
    AbsolutePath GetDirectoryArtifact(string artifactName)
    {
        var localZipFile  = ArtifactsDirectory / artifactName;
        var serverZipFile = PipelineArtifactsDirectory / artifactName;
        return IsLocalBuild ? localZipFile : serverZipFile;
    }
}

/// <summary>
/// Archive format
/// </summary>
public enum ArchiveFormat
{
    /// <summary>
    /// Zip archive
    /// </summary>
    Zip,

    /// <summary>
    /// Tar archive
    /// </summary>
    Tar
}
