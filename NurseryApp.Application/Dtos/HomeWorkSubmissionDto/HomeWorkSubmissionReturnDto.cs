namespace NurseryApp.Application.Dtos.HomeWorkSubmission
{
    public class HomeWorkSubmissionReturnDto
    {
        public int Id { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public DateTime SubmissionDate { get; set; }
        public decimal Grade { get; set; }
        public string? FeedBack { get; set; }
    }
}
