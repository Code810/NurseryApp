using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.PaymentDtos;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentRequestDto paymentRequest)
        {
            var paymentIntent = await _paymentService.CreatePaymentIntent(paymentRequest.Amount, paymentRequest.Currency);
            return Ok(new { clientSecret = paymentIntent.ClientSecret });
        }
    }


}
