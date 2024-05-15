// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Concepts;

/// <summary>
/// Represents the domain concept of quantity.
/// </summary>
/// <param name="Value">The value to initialize with.</param>
public record Quantity(int Value) : ConceptAs<int>(Value)
{
    /// <summary>
    /// Implicitly convert from an <see cref="int"/> to a <see cref="Quantity"/>.
    /// </summary>
    /// <param name="value">Int value to convert.</param>
    public static implicit operator Quantity(int value) => new(value);
}
