using App.VolgaIT.Services.PaymentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VolgaIT_2023_SemiFinal.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("Hesoyam/{id}")]
        [Authorize]
        public async Task DobavitDengi(string id)
        {
             var Role = HttpContext.User.Claims.Skip(1).First().Value.ToString();

            if (Role == "Admin") await _paymentService.AddMoney(id);
            else await _paymentService.AddMoney(id, HttpContext.User.Claims.First().Value);
        }
    }
}
