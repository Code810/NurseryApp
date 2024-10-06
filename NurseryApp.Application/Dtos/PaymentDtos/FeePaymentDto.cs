namespace NurseryApp.Application.Dtos.PaymentDtos
{
    public class FeePaymentDto
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
