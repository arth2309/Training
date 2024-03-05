using HallodocServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Implementation
{
    public class JwtServices : IJwtServices
    {
        private readonly IConfiguration configuration;
        public JwtServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateJWTAuthetication(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtHeaderParameterNames.Jku, email),
                new Claim(JwtHeaderParameterNames.Kid, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, email)
            };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(20);
               

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
