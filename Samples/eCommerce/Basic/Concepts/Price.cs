// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Concepts;

/// <summary>
/// Represents the domain concept of price.
/// </summary>
/// <param name="Net">The net price.</param>
/// <param name="Gross">The gross price.</param>
public record Price(decimal Net, decimal Gross)
{
    /// <summary>
    /// The zero price.
    /// </summary>
    public static readonly Price Zero = new(0, 0);
}
