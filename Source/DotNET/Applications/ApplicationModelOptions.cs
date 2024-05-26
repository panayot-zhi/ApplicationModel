// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications.Execution;

namespace Cratis.Applications;

/// <summary>
/// Represents the options for the application model.
/// </summary>
public class ApplicationModelOptions
{
    /// <summary>
    /// Gets or sets the options for the correlation ID.
    /// </summary>
    public CorrelationIdOptions CorrelationId { get; set; } = new();
}