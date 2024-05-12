// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Build.Framework;
using Task = Microsoft.Build.Utilities.Task;

namespace Cratis.Applications.ProxyGenerator.Build;

/// <summary>
/// MSBuild task for generating proxies.
/// </summary>
public class ProxyGeneratorBuildTask : Task
{
    /// <summary>
    /// Gets or sets the input assembly to generate proxies from.
    /// </summary>
    [Required]
    public required ITaskItem InputAssembly { get; set; }

    /// <summary>
    /// Gets or sets the output path for the generated proxies.
    /// </summary>
    [Required]
    public required ITaskItem OutputPath { get; set; }

    /// <inheritdoc/>
    public override bool Execute()
    {
        Log.LogMessage(MessageImportance.High, "Cratis Proxy Generator\n");
        Log.LogMessage(MessageImportance.High, $"  Using assembly: {InputAssembly.ItemSpec}");
        Log.LogMessage(MessageImportance.High, $"  Outputting to: {OutputPath.ItemSpec}");

        return Generator.Generate(
            InputAssembly.ItemSpec,
            OutputPath.ItemSpec,
            s => Log.LogMessage(MessageImportance.High, s),
            s => Log.LogError(s)).GetAwaiter().GetResult();
    }
}