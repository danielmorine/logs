using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using reg.Configurations;
using Scaffolds;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reg.Services
{
    public interface ITokenService
    {
        string GetToken(ApplicationUser user);
    }
    public class TokenService : ITokenService
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _seconds;
        private readonly string _key;
        private readonly SigningConfiguration _signingConfigurations;

        public TokenService(IConfiguration configuration, SigningConfiguration signingConfigurations)
        {
            _seconds = configuration.GetValue<int>("TokenConfiguration:Seconds");
            _issuer = configuration.GetValue<string>("TokenConfiguration:Issuer");
            _audience = configuration.GetValue<string>("TokenConfiguration:Audience");
            _key = configuration.GetValue<string>("TokenConfiguration:Secret");
            _signingConfigurations = signingConfigurations;
        }

        public string GetToken(ApplicationUser user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor 
            {
                Audience = _audience,
                Expires = DateTime.UtcNow.AddSeconds(_seconds),
                NotBefore= DateTime.UtcNow,
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
            });

            return handler.WriteToken(securityToken);
        }
    }
}
