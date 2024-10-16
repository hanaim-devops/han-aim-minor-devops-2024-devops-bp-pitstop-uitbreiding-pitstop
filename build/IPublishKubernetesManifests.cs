using Components;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Helm;
using Nuke.Common.Tools.Kubernetes;

public interface IPublishKubernetesManifests : INukeBuild
{
    AbsolutePath KubernetesManifestDirectory => RootDirectory / "k8s";
    
    AbsolutePath HelmChartDirectory => RootDirectory / "charts";
    AbsolutePath LegacyStartScriptDirectory => KubernetesManifestDirectory / "scripts";

    Target RunLegacyScrips => _ => _
        .TryTriggeredBy<xBuild>(x => x.Deploy)
        .OnlyWhenStatic(() => IsLocalBuild)
        .Executes(() =>
        {
            ProcessTasks.DefaultWorkingDirectory = LegacyStartScriptDirectory;
            ProcessTasks.StartProcess(LegacyStartScriptDirectory + "/start-all.sh", "--nomesh").AssertZeroExitCode();
        });
    
    // Target ApplyConfiguration => _ => _
    //     .TryTriggeredBy<xBuild>(x => x.Deploy)
    //     .Executes(() =>
    //     {
    //         KubernetesTasks.KubernetesApply(s => s
    //             .SetFilename(KubernetesManifestDirectory));
    //     });
    

    // Target HelmInstall => _ => _
    //     .TryTriggeredBy<xBuild>(x => x.Deploy)
    //     .Executes(() =>
    //     {
    //         HelmTasks.HelmRepoAdd(_ => _
    //             .SetName("argo")
    //             .SetUrl("https://argoproj.github.io/argo-helm"));
    //         
    //         HelmTasks.HelmUpgrade(s => s
    //             .SetRelease("argo-cd")
    //             .SetChart("argo/argo-cd")
    //             .SetVersion("7.6.9")
    //             .SetNamespace("argocd")
    //             .SetCreateNamespace(true)
    //             .EnableInstall()
    //             .SetWait(true)
    //         );
    //     });
}
