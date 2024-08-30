namespace NurseryApp.Core.Entities
{
    public class HomeWork : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public List<HomeWorkSubmission> HomeWorkSubmissions { get; set; }
    }
}
