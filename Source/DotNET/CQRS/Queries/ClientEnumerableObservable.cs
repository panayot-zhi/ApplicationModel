// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net.WebSockets;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cratis.Applications.Queries;

/// <summary>
/// Represents an implementation of <see cref="IClientEnumerableObservable"/>.
/// </summary>
/// <typeparam name="T">Type of data being observed.</typeparam>
/// <param name="enumerable">The <see cref="IAsyncEnumerable{T}"/> to use for streaming.</param>
/// <param name="jsonOptions">The <see cref="JsonOptions"/>.</param>
public class ClientEnumerableObservable<T>(IAsyncEnumerable<T> enumerable, JsonOptions jsonOptions) : IClientEnumerableObservable
{
    /// <inheritdoc/>
    public async Task HandleConnection(ActionExecutingContext context)
    {
        using var webSocket = await context.HttpContext.WebSockets.AcceptWebSocketAsync();
        var queryResult = new QueryResult<object>();
        using var cts = new CancellationTokenSource();

        _ = Task.Run(async () =>
        {
            await foreach (var item in enumerable.WithCancellation(cts.Token))
            {
                if (item is null)
                {
                    break;
                }

                queryResult.Data = item;

                try
                {
                    var message = JsonSerializer.SerializeToUtf8Bytes(queryResult, jsonOptions.JsonSerializerOptions);
                    await webSocket.SendAsync(message, WebSocketMessageType.Text, true, cts.Token);
                    message = null!;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error sending message to client: {ex.Message}\n{ex.StackTrace}");
                    break;
                }
            }
        });

        var buffer = new byte[1024 * 4];
        try
        {
            var received = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cts.Token);

            while (!received.CloseStatus.HasValue)
            {
                received = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cts.Token);
            }

            await webSocket.CloseAsync(received.CloseStatus.Value, received.CloseStatusDescription, cts.Token);
        }
        catch
        {
            Console.WriteLine("Client disconnected");
        }
        finally
        {
            cts.Cancel();
        }
    }
}
