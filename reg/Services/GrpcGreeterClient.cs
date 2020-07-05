﻿using Google.Protobuf;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Identity;
using reg.Exceptions;
using reg.Models.RegistrationProcess;
using reg.Queries.RegistrationProcess;
using regGRPC;
using Scaffolds;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace reg.Services
{
    public interface IGrpcGreeterClient
    {
        Task AddRegistrationProcessAsync(RegistrationProcessModel model);
        Task<IEnumerable<RegistrationProcessQuery>> GetAllAsync();
        Task<RegistrationProcessByIdQuery> GetByID(Guid? RegistrationProcessID);
        Task ArchiveAsync(RegistrationProcessArchiveModel model);
        Task DeleteAsync(RegistrationProcessDeleteModel model);

    }
    public class GrpcGreeterClient : IGrpcGreeterClient
    {
        private readonly string _url = "https://localhost:5001";
        private readonly UserManager<ApplicationUser> _userManager;

        public GrpcGreeterClient(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task ArchiveAsync(RegistrationProcessArchiveModel model)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            await ValidateArrayList(model, client);
            var archive = new ArchiveRequest();

            foreach (var value in model.IDs)
            {
                archive.List.Add( new ArchiveRequestObject { Id = value.ToString() });
            }          

            var reply = await client.ArchiveAsync(archive);

            if (!reply.Status)
            {
                throw new CustomException("Não foi possível arquivar");
            }
        }

        public async Task DeleteAsync(RegistrationProcessDeleteModel model)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            await ValidateArrayList(model, client);
            var deleteRequest = new DeleteRequest();

            foreach (var value in model.IDs)
            {
                deleteRequest.List.Add(new ArchiveRequestObject { Id = value.ToString() });
            }

            var reply = await client.DeleteAsync(deleteRequest);

            if (!reply.Status)
            {
                throw new CustomException("Não foi possível deletar");
            }
        }

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

        public async Task<RegistrationProcessByIdQuery> GetByID(Guid? registrationProcessID)
        {
            if (!registrationProcessID.HasValue)
            {
                throw new CustomException("Não identificamos o id informado, tente novamente");
            }
            using var channel = GrpcChannel.ForAddress(_url);
            var client = new Greeter.GreeterClient(channel);

            await ValidateRegistrationProcessID(registrationProcessID.Value, client);

            var reply = await client.
                GetByIdRegistrationProcessAsync(new GetByIdRegistrationProcessRequest { RegistrationProcessID = registrationProcessID.ToString() });
            
            CultureInfo cult = new CultureInfo("pt-BR");

            return new RegistrationProcessByIdQuery
            {
                CreatedDate = DateTimeOffset.Parse(reply.CreatedDate).ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss", cult),
                Details = reply.Details,
                EnvironmentTypeName = reply.EnvironmentTypeName,
                Events = reply.Events,
                LevelTypeName = reply.LevelTypeName,
                OwnerID = Guid.Parse(reply.OwnerID),
                RegistrationProcessID = Guid.Parse(reply.RegistrationProcessID),
                ReportSource = reply.ReportSource,
                Title = reply.Title,                 
            };

        }
        public async Task<IEnumerable<RegistrationProcessQuery>> GetAllAsync()
        {
            using var channel = GrpcChannel.ForAddress(_url);
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SendGetAllRegistrationProcessAsync(new GetAllRegistrationProcessRequest { });

            var list = new List<RegistrationProcessQuery>();
            CultureInfo cult = new CultureInfo("pt-BR");

            if (reply.List.ToArray().Length > 0)
            {
                foreach (var value in reply.List.ToArray())
                {
                    list.Add(new RegistrationProcessQuery
                    {
                        EnvironmentTypeName = value.EnvironmentTypeName,
                        Events = value.Events,
                        LevelTypeName = value.LevelTypeName,
                        ReportDescription = value.ReportDescription,
                        ReportSource = value.ReportSource,
                        RegistrationProcessID = Guid.Parse(value.RegistrationProcessID),
                        CreatedDate = DateTimeOffset.Parse(value.CreatedDate).ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss", cult)
                    });
                }
            }
            return list;
        }
        private async Task ValidateArrayList(RegistrationProcessArchiveModel model, Greeter.GreeterClient client)
        {
            if (model.IDs.ToArray().Length == 0)
            {
                throw new CustomException("Não foi encontrado nenhum ID");
            }

            foreach (var value in model.IDs)
            {
                await ValidateRegistrationProcessID(value, client);
            }
        }
        private async Task ValidateArrayList(RegistrationProcessDeleteModel model, Greeter.GreeterClient client)
        {
            if (model.IDs.ToArray().Length == 0)
            {
                throw new CustomException("Não foi encontrado nenhum ID");
            }

            foreach (var value in model.IDs)
            {
                await ValidateRegistrationProcessID(value, client);
            }
        }
        private async Task ValidateRegistrationProcessID(Guid registrationProcessID, Greeter.GreeterClient client)
        {
            var reply = await client.ValidateRegistrationProcessIdAsync(new ValidateRegistrationProcessIdRequest {  RegistrationProcessID = registrationProcessID.ToString()});

            if (!reply.Status)
            {
                throw new CustomException("O id informado não existe, verifique o dado digitado");
            }
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
