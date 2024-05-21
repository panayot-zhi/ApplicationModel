// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Concepts;

/// <summary>
/// Represents the domain concept for a first name.
/// </summary>
/// <param name="value">Value to initialize with.</param>
public record FirstName(string value) : ConceptAs<string>(value)
{
    /// <summary>
    /// Implicitly convert from a <see cref="string"/> to a <see cref="FirstName"/>.
    /// </summary>
    /// <param name="value">String value to convert from.</param>
    public static implicit operator FirstName(string value) => new(value);
}
