using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Reactivities.API.DTOs;
using Reactivities.API.Services;
using Reactivities.Domain;
using System.Security.Claims;

namespace Reactivities.API.Controllers
{
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

        [AllowAnonymous] //ensures that this endpoint no longer needs authentication in order to access it
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await userManager.Users
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

            if (user == null) return Unauthorized();

            var result = await userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (result) return CreateUserObject(user);

            return Unauthorized();
        }

        [AllowAnonymous] //ensures that this endpoint no longer needs authentication in order to access it
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await userManager.Users.AnyAsync(x => x.UserName == registerDTO.Username))
            {
                ModelState.AddModelError("username", "Username is already taken");
                return ValidationProblem();
            }

            if (await userManager.Users.AnyAsync(x => x.Email == registerDTO.Email))
            {
                ModelState.AddModelError("email", "Username is already taken");
                return ValidationProblem();
            }

            var user = new AppUser
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.Username
            };

            var result = await userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded) return CreateUserObject(user);

            return BadRequest(result.Errors);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user = await userManager.Users
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(u => u.Email == User.FindFirstValue(ClaimTypes.Email));

            return CreateUserObject(user);
        }

        private UserDTO CreateUserObject(AppUser user) => new UserDTO
        {
            DisplayName = user.DisplayName,
            Image = user?.Photos?.FirstOrDefault(p => p.IsMain)?.Url,
            Token = tokenService.CreateToken(user),
            Username = user.UserName,
        };
    }
}
