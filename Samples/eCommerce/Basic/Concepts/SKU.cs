// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Concepts;

/// <summary>
/// Represents the concept of a Stock Keeping Unit (SKU).
/// </summary>
/// <param name="Value">The value to initialize with.</param>
public record SKU(string Value) : ConceptAs<string>(Value)
{
    /// <summary>
    /// Implicitly convert from a <see cref="string"/> to a <see cref="SKU"/>.
    /// </summary>
    /// <param name="value">String to convert from.</param>
    public static implicit operator SKU(string value) => new(value);
}
