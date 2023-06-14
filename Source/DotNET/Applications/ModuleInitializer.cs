// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using Aksio.Types;

namespace Microsoft.Extensions.Hosting;

#pragma warning disable CA2255 // Allow module initializer

/// <summary>
/// Module initializer for the Application Model.
/// </summary>
internal static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    [ModuleInitializer]
    public static void Initialize()
    {
        PackageReferencedAssemblies.Instance.AddAssemblyPrefixesToExclude(
            "AutoMapper",
            "Autofac",
            "Azure",
            "Elasticsearch",
            "FluentValidation",
            "Grpc",
            "Handlebars",
            "NJsonSchema",
            "MongoDB",
            "Orleans",
            "Serilog",
            "Swashbuckle");
    }
}
