namespace NurseryApp.Application.Dtos.GroupDto
{
    public class GroupListDto
    {
        public int TotalCount { get; set; }
        public IEnumerable<GroupReturnDto> Items { get; set; }
    }
}
