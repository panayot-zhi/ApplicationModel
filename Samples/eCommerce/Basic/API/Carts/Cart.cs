// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reactive.Subjects;
using System.Security.Principal;
using Concepts.Carts;
using Cratis.Applications.ModelBinding;
using Cratis.Applications.Queries;
using Domain.Carts;
using Domain.Products;
using MongoDB.Driver;
using Read.Carts;

namespace API.Carts;

/// <summary>
/// Represents the cart API.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="Cart"/>.
/// </remarks>
/// <param name="grainFactory"><see cref="IGrainFactory"/> for working with grains.</param>
/// <param name="cartQueries"><see cref="ICartQueries"/> for working with carts.</param>
[Route("/api/carts")]
public class Cart(IGrainFactory grainFactory, ICartQueries cartQueries) : ControllerBase
{
    /// <summary>
    /// Add an item to the cart.
    /// </summary>
    /// <param name="addItemToCart">Payload holding the command.</param>
    /// <returns>Awaitable task.</returns>
    [HttpPost("add-item")]
    public async Task AddItem([FromBody] AddItemToCart addItemToCart)
    {
        var cartId = (CartId)(User.Identity?.GetUserIdAsGuid() ?? Guid.Empty);
        var price = await grainFactory.GetGrain<IProductPrice>(addItemToCart.Sku).GetPrice();
        await grainFactory.GetGrain<ICart>(cartId).AddItem(addItemToCart.Sku, price, addItemToCart.Quantity);
    }

    /// <summary>
    /// Get the cart for the current user.
    /// </summary>
    /// <returns><see cref="ClientObservable{T}"/> for <see cref="Read.Carts.Cart"/>. </returns>
    [HttpGet]
    public async Task<Read.Carts.Cart> CartForCurrentUser()
    {
        var cartId = (CartId)(User.Identity?.GetUserIdAsGuid() ?? Guid.Empty);
        return await cartQueries.Get(cartId);
    }

    /// <summary>
    /// Observes the cart for the current user.
    /// </summary>
    /// <returns><see cref="ClientObservable{T}"/> for <see cref="Read.Carts.Cart"/>. </returns>
    [HttpGet("observe")]
    public async Task<ISubject<Read.Carts.Cart>> ObserveCartForCurrentUser()
    {
        var cartId = (CartId)(User.Identity?.GetUserIdAsGuid() ?? Guid.Empty);
        return await cartQueries.Observe(cartId);
    }
}
