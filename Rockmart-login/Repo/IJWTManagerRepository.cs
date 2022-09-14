using Rockmart_login.Security_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rockmart_login.Repo
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
        Tokens GenerateToken(string userName);
        Tokens GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
