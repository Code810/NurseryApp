namespace NurseryApp.Application.Dtos.Contact
{
    public class ContactListDto
    {
        public int TotalCount { get; set; }
        public IEnumerable<ContactReturnDto> Items { get; set; }
    }
}
