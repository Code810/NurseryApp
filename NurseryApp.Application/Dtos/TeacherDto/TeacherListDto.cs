namespace NurseryApp.Application.Dtos.TeacherDto
{
    public class TeacherListDto
    {
        public int TotalCount { get; set; }
        public IEnumerable<TeacherReturnDto> Items { get; set; }
    }
}
