namespace NurseryApp.Application.Dtos.FeeDto
{
    public class FeeReturnDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime PaidDate { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
    }
}
