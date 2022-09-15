using Rockmart_login.Repo;
using Rockmart_login.Security_Model;

namespace Rockmart_login.Service
{
    public class UserService :  IUserService
    { 
        private readonly IUserServiceRepository _repository;
         public UserService(IUserServiceRepository repository)
        {
            _repository = repository;
        }

        public UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user) => _repository.AddUserRefreshTokens(user);

        public void DeleteUserRefreshTokens(string username, string refreshToken) => _repository.DeleteUserRefreshTokens(username, refreshToken);

        public UserRefreshTokens GetSavedRefreshTokens(string username, string refreshToken) => _repository.GetSavedRefreshTokens(username, refreshToken);

        public int SaveCommit() => _repository.SaveCommit();

        public Task<bool> IsValidUserAsync(Users users) => _repository.IsValidUserAsync(users);

    }
}
