namespace NurseryApp.Application.Dtos.HomeWorkSubmission
{
    public class HomeWorkSubmissionReturnDto
    {
        public int HomeWorkTitle { get; set; }
        public int StudentId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public decimal Grade { get; set; }
        public string? FeedBack { get; set; }
    }
}
