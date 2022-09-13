using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public BusinessesController(RockMartContext context, IJWTManagerRepository jWTManager)
        {
            _context = context;
            _jWTManager= jWTManager;
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
        public IActionResult Authenticate(Users usersdata)
        {
            var token = _jWTManager.Authenticate(usersdata);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
