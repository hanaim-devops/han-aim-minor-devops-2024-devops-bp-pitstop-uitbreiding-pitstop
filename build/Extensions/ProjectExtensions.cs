using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Utilities;

namespace Extensions;

/// <summary>
/// Extension methods on the Project class to make it easier to find certain projects
/// </summary>
[PublicAPI]
public static class ProjectExtensions
{
    /// <summary>
    /// Get the name for a Project without the dots.
    /// </summary>
    public static string ArtifactName(this Project project)
        => project.Name?.Replace(".", "");

    /// <summary>
    /// Get the assembly name of a Project
    /// </summary>
    public static string GetAssemblyName(this Project project)
        => project.GetProperty("AssemblyName");

    /// <summary>
    /// Returns true if the Project is a test project (has property IsTestProject set to True)
    /// </summary>
    public static bool IsTest(this Project project)
        => bool.TrueString.EqualsOrdinalIgnoreCase(project.GetProperty("IsTestProject"));

    /// <summary>
    /// Returns true if the Project is a smoke test (name contains SmokeTest)
    /// </summary>
    public static bool IsSmokeTest(this Project project)
        => project.Name.ContainsOrdinalIgnoreCase("SmokeTest");

    /// <summary>
    /// Returns true if the Project is a system test (name contains SystemTest)
    /// </summary>
    public static bool IsSystemTest(this Project project)
        => project.Name.ContainsOrdinalIgnoreCase("SystemTest");

    /// <summary>
    /// Returns true if the Project is packable (has property IsPackable set to True and isn't named build or _build)
    /// </summary>
    public static bool IsPackable(this Project project)
        => bool.TrueString.EqualsOrdinalIgnoreCase(project.GetProperty("IsPackable")) && !project.IsBuild();
    
    /// <summary>
    /// Returns true if the Project has the property NuGetPublishOnlyWhenChanged set to true
    /// </summary>
    public static bool IsNuGetPublishOnlyWhenChanged(this Project project)
        => bool.TrueString.EqualsOrdinalIgnoreCase(project.GetProperty("NuGetPublishOnlyWhenChanged"));

    /// <summary>
    /// Returns true if the Project is a container application
    /// </summary>
    public static bool IsContainerApp(this Project project)
        => bool.TrueString.EqualsOrdinalIgnoreCase(project.GetProperty("IsContainerApp"));
        
    /// <summary>
    /// Returns true if the Project is a build project
    /// </summary>
    public static bool IsBuild(this Project project)
        => project.GetProperty("NukeRootDirectory") is not null;
}
