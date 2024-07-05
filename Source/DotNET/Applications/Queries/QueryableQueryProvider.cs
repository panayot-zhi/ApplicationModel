// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Cratis.Applications.Queries;

/// <summary>
/// Represents a <see cref="IQueryProviderFor{TQuery}"/> for <see cref="IQueryable"/> and derivatives.
/// </summary>
/// <remarks>
/// This extends any <see cref="IQueryable"/> with `.Skip()` and `.Take()` methods.
/// </remarks>
public class QueryableQueryProvider : IQueryProviderFor<IQueryable>
{
    /// <inheritdoc/>
    public QueryProviderResult Execute(IQueryable query, QueryContext queryContext)
    {
        // TODO: Do you want to count here, possibly iterating the whole queryable?
        var totalItems = query.Count();
        if (queryContext.Paging.IsPaged)
        {
            query = query.Skip(queryContext.Paging.Page * queryContext.Paging.Size)
                         .Take(queryContext.Paging.Size);

            // TODO: Only supports sorting when paging is enabled, I think that we can support both independently.
            if (queryContext.Sorting != Sorting.None)
            {
                query = query.OrderBy(queryContext.Sorting.Field, queryContext.Sorting.Direction);
            }
        }

        return new(totalItems, query);
    }
}
