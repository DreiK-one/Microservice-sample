using GrpcServer;
using Grpc.Net.Client;


namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5008");
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(new HelloRequest() { Name = "World"});

            Console.WriteLine($"Hello, {reply.Message}");
            Console.ReadKey();
        }
    }
}

