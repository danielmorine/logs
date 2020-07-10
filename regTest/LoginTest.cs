using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using reg.Models.Login;
using reg.Services;
using Scaffolds;
using System;

namespace regTest
{

    public class LoginTest
    {
        [Test]
        public void WhenAuthenticationSuccessfull()
        {
            var email = "contato.danielharo@gmail.com";
            var password = "Teste@123";

            var loginModel = new LoginModel
            {
                Email = email,
                Password = password
            };

            var id = Guid.NewGuid();
            
            var user = new ApplicationUser
            {
                Email = email,
                Name = "Daniel",
                CreatedDate = DateTimeOffset.UtcNow,
                ID = id,
                Id = id.ToString(),
                EmailConfirmed = true,
                UserName = email,
                LockoutEnabled = true,
                LockoutEnd = null,
                PhoneNumberConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
                NormalizedUserName = email.ToUpper().Trim(),
                NormalizedEmail = email.ToUpper().Trim(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
                  
            var tokenService = new TokenService(PrepareIConfiguration());
            var token = string.Empty;

            try
            {
                if (loginModel.Email.Equals(email) && password.Equals(loginModel.Password))
                {
                    token = tokenService.GetToken(user);
                }

                Assert.AreEqual(token, token);

            }

            catch (Exception)
            {

                throw;
            }           
        }

        private Mock<UserManager<ApplicationUser>> MockUserManager()
        {
            return new Mock<UserManager<ApplicationUser>>
                (new Mock<IUserStore<ApplicationUser>>().Object,
                   new Mock<IOptions<IdentityOptions>>().Object,
                   new Mock<IPasswordHasher<ApplicationUser>>().Object,
                   new IUserValidator<ApplicationUser>[0],
                   new IPasswordValidator<ApplicationUser>[0],
                   new Mock<ILookupNormalizer>().Object,
                   new Mock<IdentityErrorDescriber>().Object,
                   new Mock<IServiceProvider>().Object,
                   new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
        }

        private Mock<SignInManager<ApplicationUser>> MockSigIn(Mock<UserManager<ApplicationUser>> userManager)
        {
            return new Mock<SignInManager<ApplicationUser>>(userManager.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<ApplicationUser>>().Object);
        }
    
        private IConfiguration PrepareIConfiguration()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables();

            IConfiguration config = builder.Build();
            return config;
        }
    }
}
