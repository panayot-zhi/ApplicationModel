// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Mvc.Filters;

namespace Cratis.Applications.Queries;

/// <summary>
/// Defines an enumerable that is observed by a connected client.
/// </summary>
public interface IClientEnumerableObservable
{
    /// <summary>
    /// Handle the action context and result from the action.
    /// </summary>
    /// <param name="context"><see cref="ActionExecutingContext"/> to handle for.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task HandleConnection(ActionExecutingContext context);
}
