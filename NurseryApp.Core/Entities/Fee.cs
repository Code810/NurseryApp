namespace NurseryApp.Core.Entities
{
    public class Fee : BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime PaidDate { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
