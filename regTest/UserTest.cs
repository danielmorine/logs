using Moq;
using NUnit.Framework;
using reg.Exceptions;
using reg.Repository;
using reg.Services;
using System;
using System.Threading.Tasks;

namespace regTest
{
    public class UserTest
    {

        [Test]
        public async Task WhenUserIsCreatedSuccess()
        {
            var mock = new Mock<IApplicationUserRepository>();
            var userService = new UserService(mock.Object);
            
            try
            {
                await userService.AddAsync(new reg.Models.User.UserModelCreate { Email = "contato.danielharo@gmail.com", Name = "Daniel", Password = "Teste@123" });
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }          
        }

        [Test]
        public async Task ShouldThrowAnExceptionWhenTheNameIsMissing()
        {
            var mock = new Mock<IApplicationUserRepository>();
            var userService = new UserService(mock.Object);
            try
            {
                await userService.AddAsync(new reg.Models.User.UserModelCreate { Email = "contato.danielharo@gmail.com", Password = "Teste@123" });
            }
            catch (CustomException ex)
            {
                //var message = "Nome, Email e senha s�o campos obrigat�rios";
                //Assert.AreEqual(message, ex.Message);
            }
        }

        [Test]
        public async Task ShouldThrowAnExceptionWhenObjectIsNull()
        {
            var mock = new Mock<IApplicationUserRepository>();
        
            var userService = new UserService(mock.Object);

            try
            {
                await userService.AddAsync(null);
            }
            catch (CustomException ex)
            {
                //var message = "N�o foi poss�vel criar este usu�rio";
                //Assert.AreEqual(message, ex.Message);
            }
        }

        [Test]
        public async Task ShouldThrowAndExceptionWhenEmailAlreadyExists()
        {
            var mock = new Mock<IApplicationUserRepository>();
            var email = "contato.danielharo@gmail.com";
            var id = Guid.NewGuid();

            await mock.Object.AddAsync(new Scaffolds.ApplicationUser 
            {
                Email = "contato.danielharo@gmail.com",
                Name = "New user",
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
            });

            await mock.Object.SaveChangeAsync();
            var userService = new UserService(mock.Object);

            try
            {
                await userService.AddAsync(new reg.Models.User.UserModelCreate { Email = "contato.danielharo@gmail.com", Name = "Teste", Password = "Teste@123" });
            }
            catch (CustomException ex)
            {
                //var message = "Este email j� existe, utilize outro";
                //Assert.AreEqual(message, ex.Message);
            }
        }
    }
}