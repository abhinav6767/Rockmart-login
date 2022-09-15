using Rockmart_login.Repo;
using Rockmart_login.Security_Model;
using System.Security.Claims;

namespace Rockmart_login.Service
{
    public class JWTService : IJWTService
    {
        private readonly IJWTManagerRepository _repository;

        public JWTService(IJWTManagerRepository repository)
        {
            _repository = repository;
        }

        public Tokens GenerateToken(string userName) => _repository.GenerateToken(userName);
        public Tokens GenerateRefreshToken(string username) => _repository.GenerateRefreshToken(username);

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token) => _repository.GetPrincipalFromExpiredToken(token);

        public Tokens Authenticate(Users users) => _repository.Authenticate(users);



    }
}
