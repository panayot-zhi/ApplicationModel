// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Cratis.Applications.ProxyGenerator.Templates;

namespace Cratis.Applications.ProxyGenerator;

/// <summary>
/// Extension methods for <see cref="ParameterInfo"/>.
/// </summary>
public static class ParameterInfoExtensions
{
    /// <summary>
    /// Convert a <see cref="ParameterInfo"/> to a <see cref="RequestArgumentDescriptor"/>.
    /// </summary>
    /// <param name="parameterInfo">Parameter to convert.</param>
    /// <returns>Converted <see cref="RequestArgumentDescriptor"/>.</returns>
    public static RequestArgumentDescriptor ToRequestArgumentDescriptor(this ParameterInfo parameterInfo)
    {
        var type = parameterInfo.ParameterType.GetTargetType();
        return new RequestArgumentDescriptor(parameterInfo.ParameterType, parameterInfo.Name!, type.Type, parameterInfo.HasDefaultValue);
    }

    /// <summary>
    /// Get request argument descriptors for a parameter, typically one adorned with [FromRequest].
    /// </summary>
    /// <param name="parameterInfo">The parameter to get for.</param>
    /// <returns>Collection of <see cref="RequestArgumentDescriptor"/>.</returns>
    public static IEnumerable<RequestArgumentDescriptor> GetRequestArgumentDescriptors(this ParameterInfo parameterInfo)
    {
        var parameters = parameterInfo.ParameterType.GetConstructors().SelectMany(_ => _.GetParameters()).ToArray();
        var properties = parameterInfo.ParameterType.GetProperties();
        bool HasConstructorParameterWithRequestArgument(PropertyInfo propertyInfo) => parameters.Any(_ => _.Name == propertyInfo.Name && _.IsRequestArgument());
        var requestProperties = properties.Where(_ => _.IsRequestArgument() || HasConstructorParameterWithRequestArgument(_)).ToArray();
        return requestProperties.Select(_ => _.ToRequestArgumentDescriptor());
    }

    /// <summary>
    /// Check if a parameter is a request argument which will make it part of the query string either as route variable or a query string parameter.
    /// </summary>
    /// <param name="parameter">Method to check.</param>
    /// <returns>True if it is a request argument, false otherwise.</returns>
    public static bool IsRequestArgument(this ParameterInfo parameter)
    {
        var attributes = parameter.GetCustomAttributesData().Select(_ => _.AttributeType.Name);
        return attributes.Any(_ => _ == WellKnownTypes.FromRouteAttribute) ||
               attributes.Any(_ => _ == WellKnownTypes.FromQueryAttribute);
    }

    /// <summary>
    /// Check if a a parameter is adorned with the FromRequestAttribute from Application Model, which means we need to investigate the internals for any request arguments.
    /// </summary>
    /// <param name="parameter">Method to check.</param>
    /// <returns>True if it is a from request argument, false otherwise.</returns>
    public static bool IsFromRequestArgument(this ParameterInfo parameter)
    {
        var attributes = parameter.GetCustomAttributesData().Select(_ => _.AttributeType.Name);
        return attributes.Any(_ => _ == WellKnownTypes.FromRequestAttribute);
    }
}
