using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reactivities.API.DTOs;
using Reactivities.API.Services;
using Reactivities.Domain;

namespace Reactivities.API.Controllers
{
    [AllowAnonymous] //ensures that all of these endpoints no longer need authentication in order to access them
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly TokenService tokenService;

        public AccountController(UserManager<AppUser> userManager, TokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
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
                    Token = tokenService.CreateToken(user),
                    Userame = user.UserName,
                };
            }

            return Unauthorized();
        }
    }
}
