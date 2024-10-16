using System.Linq;
using JetBrains.Annotations;

/// <summary>
/// Convention for container images
/// </summary>
[PublicAPI]
public sealed class ContainerImage
{
    private const string Latest = "latest";

    private readonly string ContainerRegistryHost;

    readonly string ContainerRegistry;

    /// <summary>
    /// The simple name of the container image
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The version of the container image
    /// </summary>
    public string Version { get; }

    /// <summary>
    /// The tags used to build container images
    /// </summary>
    public string[] BuildTags => [BuildTag(Version), BuildTag(Latest)];


    /// <summary>
    /// The tags used to deploy containers to a container registry
    /// </summary>
    public string[] DeployTags => [DeployTag(Version), DeployTag(Latest)];

    /// <summary>
    /// The combined build & deployment tags
    /// </summary>
    public (string BuildTag, string DeployTag)[] Tags => BuildTags.Zip(DeployTags).ToArray();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name"></param>
    /// <param name="version"></param>
    /// <param name="containerRegistry"></param>
    /// <param name="containerRegistryHost"></param>
    public ContainerImage(string name, string version, string containerRegistry, [CanBeNull] string containerRegistryHost = null)
    {
        Name = name;
        Version = version;
        ContainerRegistryHost = containerRegistryHost ?? "build";
        ContainerRegistry = containerRegistry;
    }

    private string DeployTag(string version) => $"{ContainerRegistryHost}/{ContainerRegistry}/{Name}:{version}";
    private string BuildTag(string version) => $"build/{Name}:{version}";
}
