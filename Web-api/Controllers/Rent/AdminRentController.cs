using App.VolgaIT.DTOs;
using App.VolgaIT.Services.RentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VolgaIT_2023_SemiFinal.Controllers.Rent
{
    [Route("api/Admin/")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminRentController : ControllerBase
    {
        private readonly AdminRentService _adminRentService;

        public AdminRentController(AdminRentService adminRentService)
        {
            _adminRentService = adminRentService;
        }

        [HttpGet("Rent/{id}")]
        public async Task<RentResponseDTO> GetRentById(string id)
        {
            return await _adminRentService.GetRentInfo(id);
        }

        [HttpGet("UserHistory/{id}")]
        public async Task<IEnumerable<RentResponseDTO>> GetRentForUser(string id)
        {
            return await _adminRentService.GetRentHistoryForUser(id);
        }

        [HttpGet("TransportHistory/{id}")]
        public async Task<IEnumerable<RentResponseDTO>> GetRentForTransport(string id)
        {
            return await _adminRentService.GetRentHistoryForTransport(id);
        }

        [HttpPost("Rent")]
        public async Task<IActionResult> CreateRent(AdminRentRequestDTO dto)
        {
            return Ok(await _adminRentService.CreateNewRent(dto));
        }

        [HttpPost("Rent/End/{id}")]
        public async Task<IActionResult> EndRent([FromQuery(Name = "lat")]double latitude,
            [FromQuery(Name = "long")]double longtitude,
            string id)
        {
            await _adminRentService.EndRent(id, latitude, longtitude);
            return Ok();
        }

        [HttpPut("Rent/{id}")]
        public async Task<IActionResult> UpdateRent(string id, AdminRentRequestDTO dto)
        {
            await _adminRentService.UpdateRent(id, dto);
            return Ok();
        }

        [HttpDelete("Rent/{id}")]
        public async Task<IActionResult> DeleteRent(string id)
        {
            await _adminRentService.DeleteRent(id);
            return Ok();
        }
    }
}
