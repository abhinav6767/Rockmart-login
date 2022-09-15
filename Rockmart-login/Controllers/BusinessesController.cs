using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rockmart_login.Entities;
using Rockmart_login.Repo;
using Rockmart_login.Service;
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
        private readonly IUserService userService;
        private readonly IJWTService jWTService;

        public BusinessesController(RockMartContext context, IJWTManagerRepository jWTManager, IUserServiceRepository userServiceRepository, IUserService userService, IJWTService jWTService)
        {
            _context = context;
            _jWTManager = jWTManager;
            this.userServiceRepository = userServiceRepository;
            this.userService = userService;
            this.jWTService = jWTService;
        }

        // GET: api/Businesses
        [Authorize(AuthenticationSchemes = "Bearer")]
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
            var validUser = await userService.IsValidUserAsync(usersdata);

            if (!validUser)
            {
                return Unauthorized("Incorrect username or password!");
            }

            var token = jWTService.GenerateToken(usersdata.BusinessName);

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

            userService.AddUserRefreshTokens(obj);
            userService.SaveCommit();
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(Tokens token)
        {
            var principal = jWTService.GetPrincipalFromExpiredToken(token.Token);
            var username = principal.Identity?.Name;

            //retrieve the saved refresh token from database
            var savedRefreshToken = userService.GetSavedRefreshTokens(username, token.RefreshToken);

            if (savedRefreshToken.RefreshToken != token.RefreshToken)
            {
                return Unauthorized("Invalid attempt!");
            }

            var newJwtToken = jWTService.GenerateRefreshToken(username);

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

            userService.DeleteUserRefreshTokens(username, token.RefreshToken);
            userService.AddUserRefreshTokens(obj);
            userService.SaveCommit();

            return Ok(newJwtToken);
        }


    }
}
