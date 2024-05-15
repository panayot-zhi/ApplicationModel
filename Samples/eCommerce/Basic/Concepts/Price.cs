// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Concepts;

/// <summary>
/// Represents the domain concept of price.
/// </summary>
/// <param name="Value">Value to initialize it with.</param>
public record Price(decimal Value) : ConceptAs<decimal>(Value)
{
    /// <summary>
    /// Implicitly convert from a <see cref="decimal"/> to a <see cref="Price"/>.
    /// </summary>
    /// <param name="value">Decimal to convert from.</param>
    public static implicit operator Price(decimal value) => new(value);
}