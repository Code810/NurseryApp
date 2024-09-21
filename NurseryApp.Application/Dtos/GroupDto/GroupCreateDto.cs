namespace NurseryApp.Application.Dtos.GroupDto
{
    public class GroupCreateDto
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string RoomNumber { get; set; }
        public int TeacherId { get; set; }
        public int MaxAge { get; set; }
        public int MinAge { get; set; }
        public string Language { get; set; }
    }
}

