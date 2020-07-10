using Moq;
using NUnit.Framework;
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
            catch (Exception ex)
            {
                var messageException = ex.Message.Replace("ã", "a").Replace("ó", "o");
                Assert.AreEqual("Nome, Email e senha sao campos obrigatorios", messageException);
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
            catch (Exception ex)
            {
                var messageException = ex.Message.Replace("ã", "a").Replace("í", "i").Replace("á", "a");
                Assert.AreEqual("Nao foi possivel criar este usuario", messageException);
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
            catch (Exception ex)
            {
                var messageException = ex.Message.Replace("á", "a");
                Assert.AreEqual("Este email ja existe, utilize outro", messageException);
            }
        }
    }
}