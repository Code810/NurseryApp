namespace NurseryApp.Core.Entities
{
    public class Contact : BaseEntity
    {
        public bool IsRead { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Message { get; set; }
    }
}
