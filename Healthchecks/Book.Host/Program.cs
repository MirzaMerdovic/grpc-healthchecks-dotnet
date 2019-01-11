using System;
using ConsoleOut.Book.Contract;
using Grpc.Core;
using Grpc.Health.V1;
using Grpc.HealthCheck;

namespace ConsoleOut.Book.Host
{
    internal class Program
    {
        private const string Host = "127.0.0.1";
        private const int Port = 4242;
        private const string ServiceName = "ConosleOut.Book.BookService";

        private static void Main(string[] args)
        {
            var server = new Server
            {
                Ports = { new ServerPort(Host, Port, ServerCredentials.Insecure) }
            };

            RegisterHealthCheck(server, ServiceName);

            server.Services.Add(BookService.BindService(new BookServiceImplementation()));

            server.Start();
            Console.WriteLine($"Server is up on: {Host}:{Port}");

            server.ShutdownTask.GetAwaiter().GetResult();
        }

        private static void RegisterHealthCheck(Server server, string serviceName)
        {
            var healthImplementation = new HealthServiceImpl();
            healthImplementation.SetStatus(serviceName, HealthCheckResponse.Types.ServingStatus.Serving);
            server.Services.Add(Health.BindService(healthImplementation));
        }
    }
}
