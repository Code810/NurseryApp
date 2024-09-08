namespace NurseryApp.Application.Dtos.GroupDto
{
    public class GroupCreateDto
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string RoomNumber { get; set; }
        public int TeacherId { get; set; }
    }
}
