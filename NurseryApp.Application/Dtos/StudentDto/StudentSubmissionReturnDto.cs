using NurseryApp.Application.Dtos.AttenDanceDto;
using NurseryApp.Application.Dtos.HomeWorkSubmission;

namespace NurseryApp.Application.Dtos.StudentDto
{
    public class StudentSubmissionReturnDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FileName { get; set; }
        public AttenDanceReturnDto? AttenDance { get; set; }
        public HomeWorkSubmissionReturnDto? HomeWorkSubmission { get; set; }
    }
}
