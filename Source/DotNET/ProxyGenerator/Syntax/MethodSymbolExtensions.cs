// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.CodeAnalysis;

namespace Aksio.Applications.ProxyGenerator.Syntax;

/// <summary>
/// Extension methods for working with <see cref="IMethodSymbol"/>.
/// </summary>
public static class MethodSymbolExtensions
{
    /// <summary>
    /// Check if a method if a command method.
    /// </summary>
    /// <param name="method">Method to check.</param>
    /// <returns>True if it is, false if not.</returns>
    public static bool IsCommandMethod(this IMethodSymbol method) =>
        method.GetAttributes().Any(_ => _.IsHttpPostAttribute()) &&
        !method.GetAttributes().Any(_ => _.IsAspNetResultAttribute());

    /// <summary>
    /// Check if a method if a command method.
    /// </summary>
    /// <param name="method">Method to check.</param>
    /// <returns>True if it is, false if not.</returns>
    public static bool IsQueryMethod(this IMethodSymbol method) =>
        method.GetAttributes().Any(_ => _.IsHttpGetAttribute()) &&
        !method.GetAttributes().Any(_ => _.IsAspNetResultAttribute());
}
