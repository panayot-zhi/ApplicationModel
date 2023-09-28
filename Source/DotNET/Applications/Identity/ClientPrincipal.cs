// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.Applications.Identity;

/// <summary>
/// Represents the structure of a Microsoft Client Principal.
/// </summary>
public class ClientPrincipal
{
#pragma warning disable CS8618
    /// <summary>
    /// Gets or sets the auth type - also referred to as the IDP type.
    /// </summary>
    public string auth_type { get; set; } = string.Empty;

    /// <summary>
    /// Get or sets the claims.
    /// </summary>
    public IEnumerable<ClientPrincipalClaim> claims { get; set; } = Enumerable.Empty<ClientPrincipalClaim>();

    /// <summary>
    /// Gets or sets which claim type holds the name information.
    /// </summary>
    public string name_typ { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets which claim type holds the role(s) information.
    /// </summary>
    public string role_typ { get; set; } = string.Empty;
#pragma warning restore CS8618
}
