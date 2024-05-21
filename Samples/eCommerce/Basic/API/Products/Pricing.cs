// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;
using Domain.Products;

namespace API.Products;

/// <summary>
/// Represents the pricing API.
/// </summary>
/// <param name="grainFactory"><see cref="IGrainFactory"/> for working with grains.</param>
[Route("/api/products/pricing")]
public class Pricing(IGrainFactory grainFactory) : ControllerBase
{
    /// <summary>
    /// Set the price of a product.
    /// </summary>
    /// <param name="setPrice">Payload holding the command.</param>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    [HttpPost("set-price")]
    public async Task SetPrice([FromBody] SetPrice setPrice)
    {
        var productPrice = grainFactory.GetGrain<IProductPrice>(setPrice.SKU);
        await productPrice.SetPrice(setPrice.Price);
    }
}
