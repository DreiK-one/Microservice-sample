using Grpc.Core;
using GrpcServer;

namespace GrpcServer.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task SayHelloStream(IAsyncStreamReader<HelloRequest> requestStream, 
            IServerStreamWriter<HelloReply> responceStream, 
            ServerCallContext context)
        {
            await foreach (var request in requestStream.ReadAllAsync())
            {
                await responceStream.WriteAsync(new HelloReply()
                {
                    Message = $"{DateTime.UtcNow}: Hello, {request.Name}"
                });
            }
        }
    }
}