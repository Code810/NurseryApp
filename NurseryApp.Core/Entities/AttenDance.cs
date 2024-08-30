namespace NurseryApp.Core.Entities
{
    public class AttenDance : BaseEntity
    {
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
