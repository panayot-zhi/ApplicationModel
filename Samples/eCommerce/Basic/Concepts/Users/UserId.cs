// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Concepts.Users;

/// <summary>
/// Represents the domain concept of a user identifier.
/// </summary>
/// <param name="value">Value to initialize with.</param>
public record UserId(Guid value) : ConceptAs<Guid>(value)
{
    /// <summary>
    /// The user id value representing not set.
    /// </summary>
    public static readonly UserId NotSet = Guid.Empty;

    /// <summary>
    /// Implicitly convert from a <see cref="Guid"/> to a <see cref="UserId"/>.
    /// </summary>
    /// <param name="value">Guid value to convert from.</param>
    public static implicit operator UserId(Guid value) => new(value);
}
