using Rockmart_login.Security_Model;

namespace Rockmart_login.Service
{
    public interface IUserService
    {
        UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user);
        void DeleteUserRefreshTokens(string username, string refreshToken);
        UserRefreshTokens GetSavedRefreshTokens(string username, string refreshToken);
        int SaveCommit();

        Task<bool> IsValidUserAsync(Users users);
    }
}
