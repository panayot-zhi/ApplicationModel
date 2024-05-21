// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;
using Concepts.Carts;
using Domain.Products;

namespace API.Carts;

/// <summary>
/// Represents the validation for the <see cref="AddItemToCart"/> command.
/// </summary>
public class AddItemToCartValidator : CommandValidator<AddItemToCart>
{
    /// <summary>
    /// Initializes a new instance of <see cref="AddItemToCartValidator"/>.
    /// </summary>
    /// <param name="grainFactory"><see cref="IGrainFactory"/> for working with grains.</param>
    public AddItemToCartValidator(IGrainFactory grainFactory)
    {
        RuleFor(_ => _.Sku).NotEmpty();
        RuleFor(_ => _.Quantity).GreaterThan(0);
        RuleFor(_ => _.Sku).ProductMustExist(grainFactory).WithMessage(_ => $"Product with SKU '{_.Sku}' does not exist");
    }
}
