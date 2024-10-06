using Microsoft.Extensions.Configuration;
using NurseryApp.Application.Interfaces;
using Stripe;

namespace NurseryApp.Application.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;

        public PaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        public async Task<PaymentIntent> CreatePaymentIntent(decimal amount, string currency)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(amount * 100), // Amount in cents
                    Currency = currency,
                    PaymentMethodTypes = new List<string> { "card" },
                };
                var service = new PaymentIntentService();
                return await service.CreateAsync(options);
            }
            catch (StripeException ex)
            {
                // Log the error to help diagnose the issue
                Console.WriteLine($"Stripe Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log any other errors
                Console.WriteLine($"General Error: {ex.Message}");
                throw;
            }
        }
    }
}
