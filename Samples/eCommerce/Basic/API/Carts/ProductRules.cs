// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;
using Concepts.Carts;
using Domain.Products;

namespace API.Carts;

/// <summary>
/// Represents the rules for product.
/// </summary>
public static class ProductRules
{
    /// <summary>
    /// Product must exist.
    /// </summary>
    /// <param name="ruleBuilder"><see cref="IRuleBuilder{AddItemToCart, string}"/> for building the rule.</param>
    /// <param name="grainFactory"><see cref="IGrainFactory"/> for working with grains.</param>
    /// <returns><see cref="IRuleBuilderOptions{AddItemToCart, string}"/> for continuation.</returns>
    public static IRuleBuilderOptions<AddItemToCart, string> ProductMustExist(this IRuleBuilder<AddItemToCart, string> ruleBuilder, IGrainFactory grainFactory)
    {
        return ruleBuilder.MustAsync(async (sku, token) =>
        {
            var product = grainFactory.GetGrain<IProduct>(sku);
            return await product.Exists();
        });
    }
}