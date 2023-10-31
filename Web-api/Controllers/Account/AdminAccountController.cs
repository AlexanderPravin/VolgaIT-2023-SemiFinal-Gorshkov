using App.VolgaIT.DTOs;
using App.VolgaIT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VolgaIT_2023_SemiFinal.Controllers.Account
{
    [Route("api/Admin/Account")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminAccountController : ControllerBase
    {
        private readonly AdminUserService _adminUserService;

        public AdminAccountController(AdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserResponseDTO>> GetUsersList([FromQuery(Name = "start")]int start, 
            [FromQuery(Name ="count")]int count)
        {
            return await _adminUserService.GetUsersInRange(start, count);
        }

        [HttpGet("{id}")]
        public async Task<UserResponseDTO> GetUser(string id)
        {
            return await _adminUserService.GetUserByIdAsync(id);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostUser(AdminUserRequestDTO dto)
        {
            await _adminUserService.RegisterUserAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(AdminUserRequestDTO dto)
        {
            await _adminUserService.UpdateUserAsync(dto, HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _adminUserService.DeleteUser(id);
            return Ok();
        }

    }
}
