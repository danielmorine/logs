using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using reg.Repository;
using reg.Services;
using Scaffolds;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace regTest
{

    public class LoginTest
    {
        [Test]
        public async Task WhenAuthenticationSuccessfull()
        {       
            var mockUserManager = MockUserManager();

            var id = Guid.NewGuid();
            var email = "contato.danielharo@gmail.com";
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

            var pass = new PasswordHasher<ApplicationUser>().HashPassword(user, "Teste@123");

            await mockUserManager.Object.CreateAsync(user, "Teste@123");

            var mockSigIn = MockSigIn(mockUserManager);        
          
            var tokenService = new TokenService(PrepareIConfiguration());

            var loginService = new LoginService(tokenService, mockSigIn.Object, mockUserManager.Object);

            try
            {
                await loginService.LoginAsync(new reg.Models.Login.LoginModel { Email = email, Password = "Teste@123" });
            }
            catch (Exception ex)
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
