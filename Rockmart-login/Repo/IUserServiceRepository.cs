using Rockmart_login.Security_Model;

namespace Rockmart_login.Repo
{
    public interface IUserServiceRepository
    {
        Task<bool> IsValidUserAsync(Users users);

        UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user);

        UserRefreshTokens GetSavedRefreshTokens(string username, string refreshtoken);

        void DeleteUserRefreshTokens(string username, string refreshToken);

        int SaveCommit();
    }
}
