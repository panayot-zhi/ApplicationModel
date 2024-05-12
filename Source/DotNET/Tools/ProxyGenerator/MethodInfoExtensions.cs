// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Cratis.Applications.ProxyGenerator.Templates;
using Microsoft.AspNetCore.Mvc;

namespace Cratis.Applications.ProxyGenerator;

/// <summary>
/// Extension methods for <see cref="MethodInfo"/>.
/// </summary>
public static class MethodInfoExtensions
{
    /// <summary>
    /// Get the route for a method.
    /// </summary>
    /// <param name="method">Method to get for.</param>
    /// <returns>The full route.</returns>
    public static string GetRoute(this MethodInfo method)
    {
        var routeTemplates = new string[]
        {
            method.DeclaringType?.GetCustomAttribute<RouteAttribute>()?.Template ?? string.Empty,
            method.GetCustomAttribute<HttpGetAttribute>()?.Template ?? string.Empty,
            method.GetCustomAttribute<HttpGetAttribute>()?.Template ?? string.Empty
        };

        var route = string.Empty;

        foreach (var template in routeTemplates)
        {
            route = $"{route}/{template}".Trim('/');
        }

        if (!route.StartsWith('/')) route = $"/{route}";
        return route;
    }

    /// <summary>
    /// Get argument descriptors for a method.
    /// </summary>
    /// <param name="methodInfo">Method to get for.</param>
    /// <returns>Collection of <see cref="RequestArgumentDescriptor"/>. </returns>
    public static IEnumerable<RequestArgumentDescriptor> GetArgumentDescriptors(this MethodInfo methodInfo) =>
        methodInfo.GetParameters().Where(_ => _.IsRequestArgument()).Select(_ => _.ToRequestArgumentDescriptor());

    /// <summary>
    /// Check if a method is a query method.
    /// </summary>
    /// <param name="method">Method to check.</param>
    /// <returns>True if it is a query method, false otherwise.</returns>
    public static bool IsQueryMethod(this MethodInfo method)
    {
        var attributes = method.GetCustomAttributesData().Select(_ => _.AttributeType.Name);
        return attributes.Any(_ => _ == nameof(HttpGetAttribute)) &&
            !attributes.Any(_ => _ == nameof(AspNetResultAttribute));
    }

    /// <summary>
    /// Check if a method is a query method.
    /// </summary>
    /// <param name="method">Method to check.</param>
    /// <returns>True if it is a query method, false otherwise.</returns>
    public static bool IsCommandMethod(this MethodInfo method)
    {
        var attributes = method.GetCustomAttributesData().Select(_ => _.AttributeType.Name);
        return attributes.Any(_ => _ == nameof(HttpPostAttribute)) &&
            !attributes.Any(_ => _ == nameof(AspNetResultAttribute));
    }

    /// <summary>
    /// Get properties from a <see cref="MethodInfo"/>.
    /// </summary>
    /// <param name="method">Method to get for.</param>
    /// <returns>Collection of <see cref="PropertyDescriptor"/>.</returns>
    public static IEnumerable<PropertyDescriptor> GetPropertyDescriptors(this MethodInfo method) =>
        method.GetParameters().ToList().ConvertAll(_ => _.ToPropertyDescriptor());
}
