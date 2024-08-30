namespace NurseryApp.Core.Entities
{
    public class HomeWorkSubmission : BaseEntity
    {
        public int HomeWorkId { get; set; }
        public HomeWork HomeWork { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime SubmissionDate { get; set; }
        public decimal Grade { get; set; }
        public string? FeedBack { get; set; }
    }
}
