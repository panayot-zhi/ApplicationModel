// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Mvc.Filters;

namespace Cratis.Applications.Execution;

/// <summary>
/// Represents an implementation of <see cref="IAsyncActionFilter"/> that sets the correlation ID for the request.
/// </summary>
public class CorrelationIdActionFilter : IAsyncActionFilter
{
    /// <inheritdoc/>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var correlationId = context.HttpContext.Request.Headers[Constants.CorrelationIdHeader].ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
            context.HttpContext.Request.Headers[Constants.CorrelationIdHeader] = correlationId;
        }

        context.HttpContext.Items[Constants.CorrelationIdItemKey] = correlationId;
        CorrelationIdAccessor.SetCurrent(correlationId);

        await next();
    }
}
