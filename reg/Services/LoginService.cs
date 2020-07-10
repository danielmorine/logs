using Microsoft.AspNetCore.Identity;
using reg.Exceptions;
using reg.Models.Login;
using Scaffolds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reg.Services
{
    public interface ILoginService
    {
        Task<string> LoginAsync(LoginModel model);
    }
    public class LoginService : ILoginService
    {
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginService(ITokenService tokenService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> LoginAsync(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);            
            if (result == null)
            {
                throw new CustomException("Email ou senha inválida");
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            return _tokenService.GetToken(user);
        }
    }
}
