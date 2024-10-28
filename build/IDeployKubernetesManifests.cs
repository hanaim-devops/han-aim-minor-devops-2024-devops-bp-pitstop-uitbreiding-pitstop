using Components;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Kubernetes;

public interface IDeployKubernetesManifests : INukeBuild
{
    AbsolutePath KubernetesManifestDirectory => RootDirectory / "k8s";
    AbsolutePath LegacyStartScriptDirectory => KubernetesManifestDirectory / "scripts";

    // Target RunLegacyScrips => _ => _
    //     .TryTriggeredBy<xBuild>(x => x.Deploy)
    //     .TryAfter<IDeployClusterInfra>(x => x.DeployHelmCharts)
    //     .OnlyWhenStatic(() => IsLocalBuild)
    //     .Executes(() =>
    //     {
    //         ProcessTasks.DefaultWorkingDirectory = LegacyStartScriptDirectory;
    //         ProcessTasks.StartProcess(LegacyStartScriptDirectory + "/start-all.sh", "--nomesh").AssertZeroExitCode();
    //     });
    
    // Target DeployManifests => _ => _
    //     .TryTriggeredBy<xBuild>(x => x.Deploy)
    //     .TryAfter<IDeployClusterInfra>(x => x.DeployHelmCharts)
    //     .OnlyWhenStatic(() => IsLocalBuild)
    //     .Executes(() =>
    //     {
    //         KubernetesTasks.KubernetesApply(s => s
    //             .SetFilename(KubernetesManifestDirectory));
    //     });
}
