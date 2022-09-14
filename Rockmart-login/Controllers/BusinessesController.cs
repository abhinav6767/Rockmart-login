using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rockmart_login.Entities;
using Rockmart_login.Repo;
using Rockmart_login.Security_Model;

namespace Rockmart_login.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessesController : ControllerBase
    {
        private readonly RockMartContext _context;
        private readonly IJWTManagerRepository _jWTManager;
        private readonly IUserServiceRepository userServiceRepository;

        public BusinessesController(RockMartContext context, IJWTManagerRepository jWTManager, IUserServiceRepository userServiceRepository)
        {
            _context = context;
            _jWTManager = jWTManager;
            this.userServiceRepository = userServiceRepository;
        }

        // GET: api/Businesses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Business>>> GetBusinesses()
        {
            if (_context.Businesses == null)
            {
                return NotFound();
            }
            return await _context.Businesses.ToListAsync();
        }

        // GET: api/Businesses/5

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(Users usersdata)
        {
            var validUser = await userServiceRepository.IsValidUserAsync(usersdata);

            if (!validUser)
            {
                return Unauthorized("Incorrect username or password!");
            }

            var token = _jWTManager.GenerateToken(usersdata.BusinessName);

            if (token == null)
            {
                return Unauthorized("Invalid Attempt!");
            }

            // saving refresh token to the db
            UserRefreshTokens obj = new UserRefreshTokens
            {
                RefreshToken = token.RefreshToken,
                UserName = usersdata.BusinessName
            };

            userServiceRepository.AddUserRefreshTokens(obj);
            userServiceRepository.SaveCommit();
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(Tokens token)
        {
            var principal = _jWTManager.GetPrincipalFromExpiredToken(token.Token);
            var username = principal.Identity?.Name;

            //retrieve the saved refresh token from database
            var savedRefreshToken = userServiceRepository.GetSavedRefreshTokens(username, token.RefreshToken);

            if (savedRefreshToken.RefreshToken != token.RefreshToken)
            {
                return Unauthorized("Invalid attempt!");
            }

            var newJwtToken = _jWTManager.GenerateRefreshToken(username);

            if (newJwtToken == null)
            {
                return Unauthorized("Invalid attempt!");
            }

            // saving refresh token to the db
            UserRefreshTokens obj = new UserRefreshTokens
            {
                RefreshToken = newJwtToken.RefreshToken,
                UserName = username
            };

            userServiceRepository.DeleteUserRefreshTokens(username, token.RefreshToken);
            userServiceRepository.AddUserRefreshTokens(obj);
            userServiceRepository.SaveCommit();

            return Ok(newJwtToken);
        }


    }
}
