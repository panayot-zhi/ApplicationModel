// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Concepts;

/// <summary>
/// Represents the recipient of an order.
/// </summary>
/// <param name="FirstName">The first name of the recipient.</param>
/// <param name="LastName">The last name of the recipient.</param>
/// <param name="Address">The address of the recipient.</param>
/// <param name="City">The city of the recipient.</param>
/// <param name="PostalCode">The postal code of the recipient.</param>
/// <param name="Country">The country of the recipient.</param>
public record Recipient(FirstName FirstName, LastName LastName, Address Address, City City, PostalCode PostalCode, Country Country);
