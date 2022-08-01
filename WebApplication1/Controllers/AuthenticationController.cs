using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication1.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));


            var token = new JwtSecurityToken(

                  issuer: _configuration["Jwt:Issuer"],
                  audience: _configuration["Jwt:Audience"],
                  expires: DateTime.Now.AddMinutes(30),
                  claims: authClaims,
                  signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)

              );
            return token;

        }

        [HttpPost]
        [Route("/register")]

        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return  BadRequest(ModelState);
            }

            var userCheck = await _userManager.FindByNameAsync(model.Username);

            if (userCheck != null)
            {
                return BadRequest("user already exists");
            }

            ApplicationUser user = new ApplicationUser()
            {

                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!_roleManager.RoleExistsAsync(UserRole.User).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRole.User));
            }

            if (_roleManager.RoleExistsAsync(UserRole.User).GetAwaiter().GetResult())
            {

                 await _userManager.AddToRoleAsync(user, UserRole.User);
            }

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            
            return Ok();

        }


        [HttpPost]
        [Route("/login")]

        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("invalid model");
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));

            }
            return Unauthorized("invalid credentials");
        }


    }
}
