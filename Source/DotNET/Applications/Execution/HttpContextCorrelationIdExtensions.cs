// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Execution;

namespace Microsoft.AspNetCore.Http;

/// <summary>
/// Extension methods related to <see cref="CorrelationId"/> on <see cref="HttpContext"/>.
/// </summary>
public static class HttpContextCorrelationIdExtensions
{
    /// <summary>
    /// Get the <see cref="CorrelationId"/> for an <see cref="HttpContext"/>.
    /// </summary>
    /// <param name="httpContext"><see cref="HttpContext"/> to get for.</param>
    /// <returns>The <see cref="CorrelationId"/>.</returns>
    /// <remarks>
    /// If the correlation ID is not set, it will return an empty <see cref="CorrelationId"/>.
    /// </remarks>
    public static CorrelationId GetCorrelationId(this HttpContext httpContext) => ((httpContext.Items["CorrelationId"] ?? string.Empty) as CorrelationId)!;
}
