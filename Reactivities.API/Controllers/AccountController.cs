using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reactivities.API.DTOs;
using Reactivities.Domain;

namespace Reactivities.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null) return Unauthorized();

            var result = await userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (result)
            {
                return new UserDTO
                {
                    DisplayName = user.DisplayName,
                    Image = null,
                    Token = "this will be a token",
                    Userame = user.UserName,
                };
            }

            return Unauthorized();
        }
    }
}
