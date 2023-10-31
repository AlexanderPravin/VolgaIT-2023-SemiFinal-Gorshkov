using App.VolgaIT.DTOs;
using App.VolgaIT.Services.RentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VolgaIT_2023_SemiFinal.Controllers.Rent
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RentController : ControllerBase
    {
        private readonly RentService _rentService;

        public RentController(RentService rentService)
        {
            _rentService = rentService;
        }

        [HttpGet("Transport")]
        [AllowAnonymous]
        public async Task<IEnumerable<TransportResponseDTO>> GetTransport([FromQuery(Name = "lat")]double latitude,
            [FromQuery(Name = "long")]double longtitude,
            [FromQuery(Name = "radius")]double radius,
            [FromQuery(Name = "type")]string type)
        {
            return  await _rentService.GetTransportToRent(latitude, longtitude, radius, type);
        }

        [HttpGet("{id}")]
        public async Task<RentResponseDTO> GetRentById(string id)
        {
            return await _rentService.GetRentInfoById(id, HttpContext.User.Claims.First(x=>x.ValueType == ClaimTypes.Name).Value);
        }

        [HttpGet("MyHistory")]
        public async Task<IEnumerable<RentResponseDTO>> GetRentHistoryForUser()
        {
            return await _rentService.GetRentInfoForUser(HttpContext.User.Claims.First(x => x.ValueType == ClaimTypes.Name).Value);
        }

        [HttpGet("TransportHistory/{id}")]
        public async Task<IEnumerable<RentResponseDTO>> GetRentHistoryForTransport(string id)
        {
            return await _rentService.GetRentInfoForTransport(id, HttpContext.User.Claims.First(x => x.ValueType == ClaimTypes.Name).Value);
        }

        [HttpPost("New/{id}")]
        public async Task<IActionResult> CreateNewRent([FromQuery(Name = "rentType")]string rentType, string id)
        {
            await _rentService.RentTransport(id, rentType, HttpContext.User.Claims.First(x => x.ValueType == ClaimTypes.Name).Value);
            return Ok();
        }
        [HttpPost("End/{id}")]
        public async Task<IActionResult> EndRent(string id,
            [FromQuery(Name = "lat")]double latitude,
            [FromQuery(Name = "long")] double longtitude)
        {
            await _rentService.EndRent(latitude, longtitude, id);
            return Ok();
        }
    }
}
