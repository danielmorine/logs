using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Design;
using reg.Exceptions;
using reg.Models.RegistrationProcess;
using regGRPC;
using System.Threading.Tasks;

namespace reg.Services
{
    public interface IGrpcGreeterClient
    {
        Task Say();
        Task AddRegistrationProcessAsync(RegistrationProcessModel model);
    }
    public class GrpcGreeterClient : IGrpcGreeterClient
    {
        public async Task AddRegistrationProcessAsync(RegistrationProcessModel model)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            await ValidateModel(model, client);            

            var reply = await client.SendRegistrationProcessAsync( 
                new RegistrationProcessRequest 
                { 
                    EnvironmentTypeID = model.EnvironmentTypeID.Value,
                    Events = model.Events.Value,
                    LevelTypeID = model.LevelTypeID.Value,
                    OwnerID = model.OwnerID.ToString(),
                    ReportDescription = model.ReportDescription,
                    ReportSource = model.ReportSource,
                    Title = model.Title
                });

            if (!reply.Status)
            {
                throw new CustomException("Não foi possível inserir este registro");
            }
        }  

        public async Task Say()
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            //var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
            //var daniel = reply;        
        }
       
        private async Task ValidateModel(RegistrationProcessModel model, Greeter.GreeterClient client)
        {
            if (model == null)
            {
                throw new CustomException("Não foi possível criar este log, verifique os dados informados");
            } else if(!model.OwnerID.HasValue)
            {
                throw new CustomException("Não foi possível identificar o usuário");
            } else if (string.IsNullOrEmpty(model.ReportDescription) || string.IsNullOrEmpty(model.ReportSource) || string.IsNullOrEmpty(model.Title))
            {
                throw new CustomException("Título, descrição e a origem são obrigatórios");
            } else if (!model.Events.HasValue)
            {
                throw new CustomException("Quantidade de eventos é obrigatório");
            } else if (!model.EnvironmentTypeID.HasValue || !model.LevelTypeID.HasValue)
            {
                throw new CustomException("EnvironmentTypeID e LevelTypeID são obrigatórios");
            } else if (!await ValidateEnvironmentTypeAsync(model.EnvironmentTypeID.Value, client))
            {
                throw new CustomException("O valor de EnvironmentTypeID não é válido");
            } else if (!await ValidateLevelTypeAsync(model.LevelTypeID.Value, client))
            {
                throw new CustomException("O valor de LevelTypeID não é válido");
            }
        }

        private async Task<bool> ValidateEnvironmentTypeAsync(byte environmentTypeID, Greeter.GreeterClient client)
        {
            var result = await client.SendValidateEnvironmentTypeRequestAsync(new ValidateEnvironmentTypeRequest { EnvironmentTypeID = environmentTypeID });
            return result.Status;
        }

        private async Task<bool> ValidateLevelTypeAsync(byte levelTypeID, Greeter.GreeterClient client)
        {
            var result = await client.SendValidateLevelTypeRequestAsync(new ValidateLevelTypeRequest {LevelTypeID = levelTypeID });
            return result.Status;
        }

    }
}
