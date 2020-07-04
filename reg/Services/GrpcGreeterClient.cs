using Grpc.Net.Client;
using regGRPC;
using System.Threading.Tasks;

namespace reg.Services
{
    public interface IGrpcGreeterClient
    {
        Task Say();
    }
    public class GrpcGreeterClient : IGrpcGreeterClient
    {
        public async Task Say()
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            //var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
            //var daniel = reply;
           
            var body = Newtonsoft.Json.JsonConvert.SerializeObject( new Body
            {
                Name = "Teste", NormalizedName = "TESTE"
            });

            var reply = await client.SendLevelTypeAsync(new LevelTypeRequest { Name = "Teste", Normalized = "Teste" });

            var daniel = reply;
        }

        private class Body
        {
            public string Name { get; set; }
            public string NormalizedName { get; set; }
        }
    }
}
