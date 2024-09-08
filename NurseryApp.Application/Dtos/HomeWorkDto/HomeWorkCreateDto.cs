namespace NurseryApp.Application.Dtos.HomeWork
{
    public class HomeWorkCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int GroupId { get; set; }
    }
}
