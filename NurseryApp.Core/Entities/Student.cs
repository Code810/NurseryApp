namespace NurseryApp.Core.Entities
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
        public List<AttenDance> AttenDances { get; set; }
        public List<Fee> Fees { get; set; }
        public List<HomeWorkSubmission> HomeWorkSubmissions { get; set; }
    }
}
