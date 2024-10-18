namespace NurseryApp.Application.Dtos.ParentDto
{
    public class ParentListDto
    {
        public int TotalCount { get; set; }
        public IEnumerable<ParentReturnDto> Items { get; set; }
    }
}
