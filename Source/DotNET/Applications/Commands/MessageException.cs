// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Http;

namespace Cratis.Applications.Commands;

/// <summary>
/// Represents an exception that carries a structured error code, status code, and optional state for API error responses.
/// </summary>
/// <param name="statusCode">The optional HTTP status code; defaults to 500 if null.</param>
/// <param name="code">The structured error code identifying the type of error.</param>
/// <param name="message">The exception message.</param>
/// <param name="state">Optional additional state/context associated with the error.</param>
public abstract class MessageException(
    int? statusCode,
    string code,
    string message,
    IDictionary<string, object?>? state = null)
    : Exception(message)
{
    /// <summary>
    /// Gets the structured error code identifying the type of error.
    /// </summary>
    public string Code { get; } = code;

    /// <summary>
    /// Gets additional state/context associated with the error.
    /// </summary>
    public IDictionary<string, object?> State { get; } = state ?? new Dictionary<string, object?>();

    /// <summary>
    /// Gets the HTTP status code to use in the response.
    /// </summary>
    public int StatusCode { get; } = statusCode ?? StatusCodes.Status500InternalServerError;
}
