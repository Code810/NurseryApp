namespace NurseryApp.Application.Dtos.Contact
{
    public class ContactReturnDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Message { get; set; }
    }
}
