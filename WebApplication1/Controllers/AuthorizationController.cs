using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;


        public AuthorizationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {


            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register-admin")]
        //endpoint for registering admin role only 
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Registration parameters invalid.");
            }

            var userExists = await _userManager.FindByNameAsync(model.Username);

            if (userExists != null)
            {
                return BadRequest("User already exists");
            }

            ApplicationUser user = new()
            {

                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest("user creation failed");
            }

            if (!_roleManager.RoleExistsAsync(UserRole.Admin).GetAwaiter().GetResult())
            {
                 _roleManager.CreateAsync(new IdentityRole(UserRole.Admin)).GetAwaiter().GetResult();

            }

            if (_roleManager.RoleExistsAsync(UserRole.Admin).GetAwaiter().GetResult())
            {
                _userManager.AddToRoleAsync(user, UserRole.Admin).GetAwaiter().GetResult();
            }
            return Ok(result);

        }


        [HttpGet]
        [Route("allroles")]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return Ok(roles);
        }

        [HttpGet]
        [Route("list-users")]
        public IActionResult ListAllUsers()
        {
            var users = _userManager.Users;
            return Ok(users);
        }


    }
}

