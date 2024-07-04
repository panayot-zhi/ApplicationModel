// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Read.Products;

/// <summary>
/// Defines the queries for working with the catalog.
/// </summary>
public interface ICatalogQueries
{
    /// <summary>
    /// Gets all products.
    /// </summary>
    /// <returns>Queryable of products.</returns>
    IQueryable<Product> All();
}
