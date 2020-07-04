using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Scaffolds;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public TokenService(IConfiguration configuration)
        {
            _seconds = configuration.GetValue<int>("TokenConfiguration:Seconds");
            _issuer = configuration.GetValue<string>("TokenConfiguration:Issuer");
            _audience = configuration.GetValue<string>("TokenConfiguration:Audience");
            _key = configuration.GetValue<string>("TokenConfiguration:Secret");
        }

        public string GetToken(ApplicationUser user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var expireDate = DateTimeOffset.UtcNow;

            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                new Claim("CreateAt", expireDate.ToString()),
            };

            var securityToken = new JwtSecurityToken(
              issuer: _issuer,
              audience: _audience,
              expires: DateTime.UtcNow.AddDays(20),
              signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
              SecurityAlgorithms.HmacSha256Signature),
              claims: claims
            );


            //var securityToken = handler.CreateToken(new SecurityTokenDescriptor 
            //{
            //    Audience = _audience,
            //    Expires = DateTime.UtcNow.AddSeconds(_seconds),
            //    NotBefore= DateTime.UtcNow,
            //    Issuer = _issuer,
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
            //    SecurityAlgorithms.HmacSha256Signature),
            //    Claims = claims
            //});

            return handler.WriteToken(securityToken);
        }
    }
}
