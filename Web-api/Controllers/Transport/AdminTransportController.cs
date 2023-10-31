using App.VolgaIT.DTOs;
using App.VolgaIT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VolgaIT_2023_SemiFinal.Controllers.Transport
{
    [Route("api/Admin/Transport")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminTransportController : ControllerBase
    {
        private readonly AdminTransportService _adminTransportService;

        public AdminTransportController(AdminTransportService adminTransportService)
        {
            _adminTransportService = adminTransportService;
        }

        [HttpGet]
        public async Task<IEnumerable<TransportResponseDTO>> GetTransportInRange([FromQuery(Name = "start")]int start, 
            [FromQuery(Name = "count")]int count,
            [FromQuery(Name = "type")]string type)
        {
            return await _adminTransportService.GetTransportInRange(start, count, type);
        }
        [HttpGet("{id}")]
        public async Task<TransportResponseDTO> GetTransportById(string id)
        {
            return await _adminTransportService.GetTransportById(id);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTransport(AdminTransportRequestDTO dto)
        {
            await _adminTransportService.CreateTransportAsync(dto);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransport(AdminTransportRequestDTO dto ,string id)
        {
            await _adminTransportService.UpdateTransport(dto ,id);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransport(string id)
        {
            await _adminTransportService.DeleteTransport(id);
            return Ok();
        }
    }
}
