using System.Security.Claims;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Productivity.Services;
using Task = System.Threading.Tasks.Task;

namespace API.Controllers
{

    public class AccountController : BaseApiController
    {
        private DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;

        public AccountController(DataContext context, UserManager<User> userManager,
            SignInManager<User> signInManager, TokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        private async Task SetRefreshToken(User user)
        {
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshTokens.Add(refreshToken);

            await _userManager.UpdateAsync(user);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
        
        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserDto>> CurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            
            return CreateUserObject(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                await _userManager.UpdateAsync(user);
                await SetRefreshToken(user);
                return CreateUserObject(user);
            }
            if (result.IsLockedOut)
            {
                // Account is locked out
                var errorMessage = "Account is locked out. Please try again later.";
                return Unauthorized(errorMessage);
                // Handle the error message...
            }
            if (result.IsNotAllowed)
            {
                // User is not allowed to log in
                var errorMessage = "Login is not allowed for this user.";
                return Unauthorized(errorMessage);
                // Handle the error message...
            }
            if (result.RequiresTwoFactor)
            {
                // Two-factor authentication is required
                var errorMessage = "Two-factor authentication is required for this user.";
                return Unauthorized(errorMessage);
                // Handle the error message...
            }
            else
            {
                // Authentication failed for other reasons
                var errorMessage = "Invalid username or password.";
                return Unauthorized(errorMessage);
                // Handle the error message...
            }

        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest("Problem registering user.");
            }

            return Ok("Registration successful.");
        }

        private UserDto CreateUserObject(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}