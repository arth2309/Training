using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IJwtServices
    {
        string GenerateJWTAuthetication(string email);
        bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken);
    }
}
