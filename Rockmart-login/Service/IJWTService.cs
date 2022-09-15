using NuGet.Protocol.Core.Types;
using Rockmart_login.Security_Model;
using System.Security.Claims;

namespace Rockmart_login.Service
{
    public interface IJWTService
    {
        Tokens GenerateToken(string userName);
        Tokens GenerateRefreshToken(string username);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Tokens Authenticate(Users users);

    }
}
