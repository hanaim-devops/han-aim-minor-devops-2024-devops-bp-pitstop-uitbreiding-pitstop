using Components;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;

[GitHubActions("Nuke Build", 
    GitHubActionsImage.UbuntuLatest,
    On = [GitHubActionsTrigger.PullRequest],
    InvokedTargets = [nameof(Build)])]
[GitHubActions(
    "Nuke Deploy",
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = ["main"],
    InvokedTargets = [nameof(Deploy)],
    ImportSecrets = ["DOCR_EU_DEV_PASSWORD", "DOCR_EU_DEV_USERNAME"]
)]
class Build : xBuild,
    //IAnalyzeSolution,
    //IRunTests,
    IPublishContainerImages,
    IPublishKubernetesManifests
{
    public static int Main() => Execute<Build>(x => x.Build);
    
    string IPublishContainerImages.ContainerRegistryHost => "registry.digitalocean.com";
    string IPublishContainerImages.ContainerRegistry => "docr-eu-dev-monaco";
    
    AbsolutePath IPublishKubernetesManifests.KubernetesManifestDirectory => RootDirectory / "src" / "k8s";
}
