// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using Aksio.Execution;
using Autofac.Core;

namespace Aksio.Extensions.Autofac;

/// <summary>
/// Represents an implementation of <see cref="IComponentLifetime"/> for scoping per microservice.
/// </summary>
public class SingletonPerMicroserviceComponentLifetime : IComponentLifetime
{
    /// <summary>
    /// Gets the global instance.
    /// </summary>
    public static readonly SingletonPerMicroserviceComponentLifetime Instance = new();

    readonly ConcurrentDictionary<MicroserviceId, SingletonLifetimeScope<SingletonPerMicroserviceComponentLifetime>> _scopes = new();

    /// <inheritdoc/>
    public ISharingLifetimeScope FindScope(ISharingLifetimeScope mostNestedVisibleScope)
    {
        var context = ExecutionContextManager.GetCurrent();
        if (!_scopes.ContainsKey(context.MicroserviceId))
        {
            _scopes[context.MicroserviceId] = new SingletonLifetimeScope<SingletonPerMicroserviceComponentLifetime>(mostNestedVisibleScope);
        }

        return _scopes[context.MicroserviceId];
    }
}
