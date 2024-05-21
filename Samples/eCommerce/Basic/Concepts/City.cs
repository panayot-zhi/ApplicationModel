// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Concepts;

/// <summary>
/// Represents the domain concept for a city.
/// </summary>
/// <param name="value">Value to initialize with.</param>
public record City(string value) : ConceptAs<string>(value)
{
    /// <summary>
    /// Implicitly convert from a <see cref="string"/> to a <see cref="City"/>.
    /// </summary>
    /// <param name="value">String value to convert from.</param>
    public static implicit operator City(string value) => new(value);
}
