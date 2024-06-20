using Microsoft.Extensions.DependencyInjection;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Controller;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class SocketServer
{
    private const int Port = 5000;
    private readonly ControllerRouter _controllerRouter;

    public SocketServer(ControllerRouter controllerRouter)
    {
        _controllerRouter = controllerRouter;
    }

    public async Task StartAsync()
    {
        var listener = new TcpListener(IPAddress.Any, Port);
        listener.Start();
        Console.WriteLine($"Server started on port {Port}");

        while (true)
        {

            var client = await listener.AcceptTcpClientAsync();
            _ = Task.Run(() => HandleClientAsync(client));
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {

        var buffer = new byte[2048];
        var stream = client.GetStream();

        try
        {
            while (client.Connected)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;

                var requestJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {requestJson}");

                var responseJson = await HandleRequestAsync(requestJson);

                var responseBytes = Encoding.UTF8.GetBytes(responseJson);
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

    }

    private async Task<string> HandleRequestAsync(string requestJson)
    {
        var requestObject = JsonSerializer.Deserialize<SocketRequestDTO>(requestJson);

        if (requestObject == null || string.IsNullOrEmpty(requestObject.Controller) || string.IsNullOrEmpty(requestObject.Action))
        {
            return "Invalid request format";
        }
        var response = (await _controllerRouter.RouteRequestAsync(requestObject.Controller, requestObject.Action, requestObject.Data));
        return response;
    }


}


