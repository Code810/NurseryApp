namespace NurseryApp.Application.Dtos.GroupDto
{
    public class GroupReturnDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string RoomNumber { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
    }
}
