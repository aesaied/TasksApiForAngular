using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TasksApi.Entities;
using TasksApi.Models;

namespace TasksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public UsersController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]

        public async Task<ActionResult> Register(RegistrationDto input)
        {

            AppUser appUser = new AppUser() { Email = input.Email, FullName = input.FullName, UserName = input.UserName };

            var result = await _userManager.CreateAsync(appUser, input.Password!);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                if (result.Errors.Any())
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }



                }



                return BadRequest(ModelState);

            }
        }


        [HttpPost("Login")]

        public async Task<ActionResult<LoginResultDto>> Login(LoginDto input)
        {
          var  user= await  _userManager.FindByNameAsync(input.UserName);

            if (user == null) {

                ModelState.AddModelError("", "Invalid username or password");

                return BadRequest(ModelState);
            }

            var  result =await  _userManager.CheckPasswordAsync(user, input.Password);

            if (!result)
            {
                ModelState.AddModelError("", "Invalid username or password");

                return BadRequest(ModelState);

            }


            var token = GenerateJwtToken(user);

            return Ok(new LoginResultDto() { UserName= user.UserName, Token= token });

            ///  Generate Token


        }

        private string GenerateJwtToken(AppUser user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim("FullName",user.FullName),
            new Claim("Email",user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new Claim(ClaimTypes.Name,user.UserName),

            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SignKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audiance"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


