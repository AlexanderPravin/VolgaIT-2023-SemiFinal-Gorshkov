using App.VolgaIT.DTOs;
using App.VolgaIT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VolgaIT_2023_SemiFinal.Controllers.Transport
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransportController : ControllerBase
    {
        private readonly TransportService _transportService;

        public TransportController(TransportService transportService)
        {
            _transportService = transportService;
        }

        [HttpPost]
        public async Task<IActionResult> PostTransport(TransportRequestDTO transportRequestDTO)
        {
            var res = await _transportService.CreateTransport(transportRequestDTO, HttpContext.User.Claims.First().Value);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransport(TransportRequestDTO dto, string id)
        {
            await _transportService.UpdateTransport(dto, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransport(string id)
        {
            await _transportService.DeleteTransport(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<TransportResponseDTO> GetTrasport(string id)
        {
            return await _transportService.GetTrasportByIdAsync(id);
        }
    }
}
