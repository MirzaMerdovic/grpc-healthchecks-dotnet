using System;
using ConsoleOut.Book.Contract;
using Grpc.Core;
using Grpc.Health.V1;

namespace Book.Client
{
    internal class Program
    {
        private const string ServerAddress = "127.0.0.1:4242";
        private const string ServiceName = "ConosleOut.Book.BookService";

        private static void Main(string[] args)
        {
            var channel = new Channel(ServerAddress, ChannelCredentials.Insecure);
            var healthCheckClient = new Health.HealthClient(channel);
            var healthCheckResponse = healthCheckClient.Check(new HealthCheckRequest { Service = ServiceName });

            Console.WriteLine($"Service: '{ServiceName}' has a health status: {healthCheckResponse.Status.ToString()}");
            try
            {
                healthCheckClient.Check(new HealthCheckRequest { Service = "Bad Name" });
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"Service: 'Bad Name' has a health status: {ex.Status.StatusCode.ToString()}");
            }

            var bookClient = new BookService.BookServiceClient(channel);
            var book = bookClient.GetBookById(new GetBookByIdRequest { Id = 1 });

            Console.WriteLine($"Retrieved book: {book.Name}");

            channel.ShutdownAsync().GetAwaiter().GetResult();

            Console.WriteLine("Press key to shut down ... .. .");
            Console.ReadKey();
        }
    }
}