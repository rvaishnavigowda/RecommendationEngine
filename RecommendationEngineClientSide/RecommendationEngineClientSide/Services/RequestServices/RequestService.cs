using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.RequestServices
{
    public class RequestService : IRequestService
    {
        private const string ServerIp = "127.0.0.1";
        private const int ServerPort = 5000;

        public async Task<string> SendRequestAsync(string requestJson)
        {
            using TcpClient client = new TcpClient();
            await client.ConnectAsync(ServerIp, ServerPort);

            NetworkStream stream = client.GetStream();
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            return response;
        }
    }
}
