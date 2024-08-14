//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Sockets;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Formats.Asn1.AsnWriter;
//using System.Net.Http;

//namespace RecommendationEngineServerSide
//{
//    public class StartSocket
//    {
//        private IServiceProvider _serviceProvider;
//        private const int Port = 5000;

//        public StartSocket(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        public async Task StartAsync()
//        {
//            var listener = new TcpListener(IPAddress.Any, Port);
//            listener.Start();
//            Console.WriteLine($"Server started on port {Port}");

//            while (true)
//            {
//                var client = await listener.AcceptTcpClientAsync();
//                var scope = _serviceProvider.CreateScope();
//                var SocketServer = scope.ServiceProvider.GetService<SocketServer>();

//                Thread thread = new Thread(new ParameterizedThreadStart(SocketServer.HandleClientAsync));
//                thread.Start();
//            }
//        }
//    }
//}
