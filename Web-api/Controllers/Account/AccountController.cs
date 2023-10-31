using App.VolgaIT.DTOs;
using App.VolgaIT.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VolgaIT_2023_SemiFinal.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }
        // POST api/<AccountController>
        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<string> SignIn(UserRequestDTO UserDTO)
        {
            return await _userService.SignInAsync(UserDTO);
        }

        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task SignUp(UserRequestDTO UserDTO)
        {
            await _userService.RegisterAsync(UserDTO);
        }

        [HttpPut("Update")]
        [Authorize()]
        public async Task Update(UserRequestDTO UserDTO)
        {
            await _userService.UpdateUserAsync(UserDTO, HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value);
        }

        [HttpGet("Me")]
        [Authorize]
        public async Task<UserResponseDTO> GetMe()
        {
            return await _userService.GetUserAsync(HttpContext.User.Claims.First(x=>x.Type == ClaimTypes.Name).Value);
        }
    }
}
