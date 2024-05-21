// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Concepts.Orders;

/// <summary>
/// Represents the domain concept of an order identifier.
/// </summary>
/// <param name="value">Value to initialize with.</param>
public record OrderId(Guid value) : ConceptAs<Guid>(value)
{
    /// <summary>
    /// The cart id value representing not set.
    /// </summary>
    public static readonly OrderId NotSet = Guid.Empty;

    /// <summary>
    /// Implicitly convert from a <see cref="Guid"/> to a <see cref="OrderId"/>.
    /// </summary>
    /// <param name="value">Guid value to convert from.</param>
    public static implicit operator OrderId(Guid value) => new(value);
}
