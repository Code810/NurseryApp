namespace NurseryApp.Core.Entities
{
    public class Banner : BaseEntity
    {
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Title3 { get; set; }
        public string Description { get; set; }
        public string LeftFileName { get; set; }
        public string RightFileName { get; set; }
        public string BottomFileName { get; set; }
    }
}
