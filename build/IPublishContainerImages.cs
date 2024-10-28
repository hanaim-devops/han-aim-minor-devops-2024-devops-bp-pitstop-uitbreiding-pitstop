using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using Extensions;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.Docker;

public interface IPublishContainerImages : IArtifacts, IDotNet, IGitRepository
{
    [Parameter] string ContainerRegistryUsername => Environment.GetEnvironmentVariable("DOCR_EU_DEV_USERNAME");
    [Parameter] string ContainerRegistryPassword => Environment.GetEnvironmentVariable("DOCR_EU_DEV_PASSWORD");
    
    /// <summary>
    /// The container registry to publish to 
    /// </summary>    
    string ContainerRegistryHost => "docker.io";
    
    /// <summary>
    /// 
    /// </summary>
    string ContainerRegistry => "registry-name";
    
    /// <summary>
    /// The projects for which containers should be published
    /// </summary>  
    IReadOnlyCollection<Project> Projects => Solution.AllProjects.Where(p => p.IsContainerApp()).ToList();
    
    private IReadOnlyDictionary<Project, ContainerImage> Images => Projects.ToDictionary(p => p, p =>
        new ContainerImage(p.Name.ToLower(), Version.FullVersion, ContainerRegistry, ContainerRegistryHost));

    private AbsolutePath PublishPath(string projectName) => ArtifactsDirectory / projectName;
    
    /// <summary>
    /// Build container images
    /// </summary>
    Target BuildContainerImages => d => d
        .TryTriggeredBy<xBuild>(b => b.Build)
        .Requires(() => Images.Any())
        .DependsOn(DotNetBuildSolution)
        .OnlyWhenStatic(() => IsLocalBuild || Repository.IsOnMasterBranch())
        .Executes(() =>
        {
            BuildDotNetSdkBaseImage();
            BuildDotNetRuntimeBaseImage();
            BuildDotNetAspNetBaseImage();
            
            var tags = Images.SelectMany(i => i.Value.BuildTags).ToArray();
            foreach (var (project, image) in Images)
            {
                DotNetPublish(project, PublishPath(project.Name));

                DockerTasks.DockerBuild(config => config
                    .SetPath(project.Directory)
                    .SetFile($"{project.Directory}/Dockerfile")
                    .SetTag(image.BuildTags)
                    .SetQuiet(true));
            }

            DockerTasks.DockerSave(config => config.AddImages(tags)
                .SetOutput(ArtifactsDirectory / "images.tar"));
            
            UploadArchiveArtifact("images", ArchiveFormat.Tar);
        });

    /// <summary>
    /// Push container images to azure container registry
    /// </summary>
    Target PublishContainerImages => d => d
        .DependsOn(BuildContainerImages)
        .TryTriggeredBy<xBuild>(b => b.Deploy)
        .OnlyWhenStatic(() => IsServerBuild && Repository.IsOnMasterBranch())
        .Executes(() =>
        {
            DockerTasks.DockerLoad(config =>
                config.SetInput(GetArchiveArtifact("images", ArchiveFormat.Tar)).SetQuiet(true));

            var tags = Images.SelectMany(i => i.Value.Tags).ToArray();

            foreach (var (source, target) in tags)
            {
                DockerTasks.DockerTag(config => config.SetSourceImage(source).SetTargetImage(target));
            }
            
            DockerTasks.DockerLogin(settings => 
                settings.SetServer(ContainerRegistryHost)
                    .SetUsername(ContainerRegistryUsername)
                    .SetPassword(ContainerRegistryPassword));

            foreach (var (_, target) in tags)
            {
                DockerTasks.DockerPush(config => config.SetName(target));
            }
        });
    
    private void BuildDotNetSdkBaseImage()
    {
        DockerTasks.DockerBuild(config => config
            .SetPath(RootDirectory / "src")
            .SetFile(RootDirectory / "src" / "dotnet-sdk-base-dockerfile")
            .SetTag("pitstop-dotnet-sdk-base:1.0")
            .SetQuiet(true));
    }

    private void BuildDotNetRuntimeBaseImage()
    {
        DockerTasks.DockerBuild(config => config
            .SetPath(RootDirectory / "src")
            .SetFile(RootDirectory / "src" / "dotnet-runtime-base-dockerfile")
            .SetTag("pitstop-dotnet-runtime-base:1.0")
            .SetQuiet(true));
    }

    private void BuildDotNetAspNetBaseImage()
    {
        DockerTasks.DockerBuild(config => config
            .SetPath(RootDirectory / "src")
            .SetFile(RootDirectory / "src" / "dotnet-aspnet-base-dockerfile")
            .SetTag("pitstop-dotnet-aspnet-base:1.0")
            .SetQuiet(true));
    }
}
