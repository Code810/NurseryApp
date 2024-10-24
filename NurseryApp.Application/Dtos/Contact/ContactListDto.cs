namespace NurseryApp.Application.Dtos.Contact
{
    public class ContactListDto
    {
        public int IsReadCount { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<ContactReturnDto> Items { get; set; }
    }
}
