using Stripe;

namespace NurseryApp.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentIntent> CreatePaymentIntent(decimal amount, string currency);
    }
}
