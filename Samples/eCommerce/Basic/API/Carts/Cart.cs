// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Security.Principal;
using Concepts.Carts;
using Cratis.Applications.ModelBinding;
using Cratis.Applications.Queries;
using Cratis.Applications.Queries.MongoDB;
using Domain.Carts;
using Domain.Products;
using MongoDB.Driver;

namespace API.Carts;

/// <summary>
/// Represents the cart API.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="Cart"/>.
/// </remarks>
/// <param name="grainFactory"><see cref="IGrainFactory"/> for working with grains.</param>
/// <param name="collection"><see cref="IMongoCollection{TDocument}"/> for working with MongoDB.</param>
[Route("/api/carts")]
public class Cart(IGrainFactory grainFactory, IMongoCollection<Read.Carts.Cart> collection) : ControllerBase
{
    /// <summary>
    /// Add an item to the cart.
    /// </summary>
    /// <param name="addItemToCart">Payload holding the command.</param>
    /// <returns>Awaitable task.</returns>
    [HttpPost("add-item")]
    public async Task AddItem(
        [FromBody] AddItemToCart addItemToCart)
    {
        var cartId = (CartId)(User.Identity?.GetUserIdAsGuid() ?? Guid.Empty);
        var price = await grainFactory.GetGrain<IProductPrice>(addItemToCart.Sku).GetPrice();
        await grainFactory.GetGrain<ICart>(cartId).AddItem(addItemToCart.Sku, price, addItemToCart.Quantity);
    }

    /// <summary>
    /// Observes the cart for the current user.
    /// </summary>
    /// <returns><see cref="ClientObservable{T}"/> for <see cref="Read.Carts.Cart"/>. </returns>
    [HttpGet]
    public Task<ClientObservable<Read.Carts.Cart>> CartForCurrentUser()
    {
        // var cartId = Guid.Parse("792d4260-b68b-4c06-8af0-31436f859df2");
        var cartId = (CartId)(User.Identity?.GetUserIdAsGuid() ?? Guid.Empty);
        return collection.ObserveById(cartId);
    }
}
