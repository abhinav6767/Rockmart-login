using Microsoft.AspNetCore.Identity;
using Rockmart_login.Entities;
using Rockmart_login.Security_Model;

namespace Rockmart_login.Repo
{
    public class UserServiceRepository : IUserServiceRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _db;
        Dictionary<string, string> UsersRecords = new Dictionary<string, string>
    {
        { "user1","password1"},
        { "user2","password2"},
        { "user3","password3"},
    };
        public UserServiceRepository(UserManager<IdentityUser> userManager, AppDbContext db)
        {
            this._userManager = userManager;
            this._db = db;
        }

        public UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user)
        {
            _db.UserRefreshToken.Add(user);
            return user;
        }

        public void DeleteUserRefreshTokens(string username, string refreshToken)
        {
            var item = _db.UserRefreshToken.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken);
            if (item != null)
            {
                _db.UserRefreshToken.Remove(item);
            }
        }

        public UserRefreshTokens GetSavedRefreshTokens(string username, string refreshToken)
        {
            return _db.UserRefreshToken.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken && x.IsActive == true);
        }

        public int SaveCommit()
        {
            return _db.SaveChanges();
        }

        public async Task<bool> IsValidUserAsync(Users users)
        {
            if (!UsersRecords.Any(x => x.Key == users.BusinessName && x.Value == users.Password))
            {
                return false;
            }
            //var u = _userManager.Users.FirstOrDefault(o => o.UserName == users.BusinessName);
            //var result = await _userManager.CheckPasswordAsync(u, users.Password);
            return true;

        }
    }
}
