namespace NurseryApp.Application.Dtos.GroupDto
{
    public class GroupUpdateDto
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string RoomNumber { get; set; }
        public int TeacherId { get; set; }
    }
}
