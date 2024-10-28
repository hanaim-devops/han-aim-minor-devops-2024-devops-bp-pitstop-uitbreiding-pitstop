using System;
using System.IO;
using System.Linq;
using System.Threading;
using Components;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.Tools.Helm;
using Nuke.Common.Tools.Kubernetes;
using Serilog;

public interface IDeployClusterInfra : IGitRepository, IArtifacts, IVersion
{
    AbsolutePath HelmChartDirectory => RootDirectory / "charts";
    string LocalClusterContext => "docker-desktop";
    
    string ServerClusterContext => "server-cluster";
    
    Target ClusterAuthentication => _ => _
        .TryTriggeredBy<xBuild>(x => x.Deploy)
        .OnlyWhenStatic(() => IsLocalBuild || Repository.IsOnMasterBranch())
        .Executes(() =>
        {
            var kubeConfigCluster = Environment.GetEnvironmentVariable("KUBECONFIG_CLUSTER");
            var kubeConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".kube", "config");
            var contextName = IsLocalBuild ? LocalClusterContext : ServerClusterContext;

            if (string.IsNullOrEmpty(kubeConfigCluster))
            {
                if (IsLocalBuild)
                {
                    kubeConfigCluster = File.ReadAllText(kubeConfigPath);
                }
                else
                {
                    throw new Exception("KUBECONFIG_CLUSTER environment variable must be set.");
                }
            }

            File.WriteAllText(kubeConfigPath, kubeConfigCluster);

            KubernetesTasks.Kubernetes($"config use-context {contextName} --kubeconfig {kubeConfigPath}");
            KubernetesTasks.KubernetesClusterInfo();
        });
    
    Target DeployHelmCharts => _ => _
        .DependsOn(ClusterAuthentication)
        .OnlyWhenStatic(() => IsLocalBuild || Repository.IsOnMasterBranch())
        .TryTriggeredBy<xBuild>(x => x.Deploy)
        .TryAfter<IPublishContainerImages>(x => x.PublishContainerImages)
        .Executes(() =>
        {
            HelmTasks.Helm($"version --client");
            
            HelmTasks.HelmRepoAdd(_ => _
                .SetName("argo")
                .SetUrl("https://argoproj.github.io/argo-helm"));
            
            HelmTasks.HelmUpgrade(s => s
                .SetRelease("argo-cd")
                .SetChart("argo/argo-cd")
                .SetVersion("7.6.9")
                .SetNamespace("argocd")
                .SetCreateNamespace(true)
                .EnableInstall()
                .SetWait(true)
            );
            
            if (IsLocalBuild)
            {
                // Retrieve and print the initial admin secret
                var secret = KubernetesTasks.Kubernetes($"get secret -n argocd argocd-initial-admin-secret -o jsonpath=\"{{.data.password}}\"")
                    .FirstOrDefault()
                    .Text;
                
                var decodedSecret = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(secret));

                Log.Information($"Argo CD initial admin password: {decodedSecret}");
                
                // Port-forward Argo CD server to local port 8080
                KubernetesTasks.Kubernetes("port-forward svc/argo-cd-argocd-server -n argocd 8080:443");
            }
        });
}
