using Microsoft.AspNetCore.Identity;
using reg.Exceptions;
using reg.Models.User;
using reg.Repository;
using System.Threading.Tasks;
using reg.Scaffolds;
using Scaffolds;
using System;

namespace reg.Services
{
    public interface IUserService
    {
        Task AddAsync(UserModelCreate model);
    }
    public class UserService : IUserService
    {
        private readonly IApplicationUserRepository _applicationUserRepository;

        public UserService(IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }
        public async Task AddAsync(UserModelCreate model)
        {
            await ValidateAsync(model);
            var id = Guid.NewGuid();
            var user = new ApplicationUser
            {
                Email = model.Email,
                Name = model.Name,
                CreatedDate = DateTimeOffset.UtcNow,
                ID = id,
                Id = id.ToString(),
                EmailConfirmed = true,
                UserName = model.Email,
                LockoutEnabled = true,
                LockoutEnd = null,
                PhoneNumberConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
                NormalizedUserName = model.Email.ToUpper().Trim(),
                NormalizedEmail = model.Email.ToUpper().Trim(),
                ConcurrencyStamp = Guid.NewGuid().ToString()                
            };

            var pass = new PasswordHasher<ApplicationUser>().HashPassword(user, model.Password);
            user.PasswordHash = pass;


            await _applicationUserRepository.AddAsync(user);
            await _applicationUserRepository.SaveChangeAsync();

        }

        private async Task ValidateAsync(UserModelCreate model)
        {
            if (model == null)
            {
                throw new CustomException("Não foi possível criar este usuário");
            }
            else if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password) ||
              string.IsNullOrEmpty(model.Name))
            {
                throw new CustomException("Nome, Email e senha são campos obrigatórios");
            }

            if (await _applicationUserRepository.AnyAsync(x => x.Email.Equals(model.Email)))
            {
                throw new CustomException("Este email já existe, utilize outro");
            }
        }

    }
}
