using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasksApi.Entities;
using TasksApi.Models;

namespace TasksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
    }
}


