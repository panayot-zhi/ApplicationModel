// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Cratis.Applications.Tenancy;

/// <summary>
/// Represents an implementation of <see cref="IAsyncActionFilter"/> that sets the correlation ID for the request.
/// </summary>
/// <param name="options">The options for the correlation ID.</param>
public class TenantIdActionFilter(IOptions<ApplicationModelOptions> options) : IAsyncActionFilter
{
    /// <inheritdoc/>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var tenantId = context.HttpContext.Request.Headers[options.Value.Tenancy.HttpHeader].ToString() ?? string.Empty;
        if (!string.IsNullOrEmpty(tenantId))
        {
            context.HttpContext.Items[Constants.TenantIdItemKey] = tenantId;
            TenantIdAccessor.SetCurrent(tenantId);
        }

        await next();
    }
}
