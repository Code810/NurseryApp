namespace NurseryApp.Application.Dtos.AttenDanceDto
{
    public class AttenDanceReturnDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }

    }
}
