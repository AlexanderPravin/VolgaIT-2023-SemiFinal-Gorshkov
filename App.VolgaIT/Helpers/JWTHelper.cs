using Domain.VolgaIT.Entities;
using Domain.VolgaIT.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.VolgaIT.Helpers
{
    public class JWTHelper
    {
        private readonly JwtOptions _options;

        public JWTHelper(JwtOptions options)
        {
            _options = new JwtOptions();
        }

        public string GetJwtToken(User user)
        {
            var protectedSecret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Secret));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var claimsIdentities = new ClaimsIdentity(claims);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _options.Issuer,
                Subject = claimsIdentities,
                Expires = DateTime.UtcNow.AddHours(_options.ExpiresHours),
                SigningCredentials = new SigningCredentials(protectedSecret, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
