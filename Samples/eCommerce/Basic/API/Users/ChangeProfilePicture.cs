// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts.Users;

namespace API.Users;

/// <summary>
/// Represents the command for changing the profile picture of a user.
/// </summary>
/// <param name="Id">The user id.</param>
/// <param name="Picture">The picture.</param>
public record ChangeProfilePicture(
    [FromRoute] UserId Id,
    string Picture);