using NurseryApp.Application.Dtos.AttenDanceDto;
using NurseryApp.Application.Dtos.FeeDto;
using NurseryApp.Application.Dtos.GroupDto;

namespace NurseryApp.Application.Dtos.StudentDto
{
    public class StudentReturnDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string FileName { get; set; }
        public GroupReturnDto Group { get; set; }
        public List<FeeReturnDto> Fees { get; set; }
        public AttenDanceReturnDto? AttenDance { get; set; }
    }
}
