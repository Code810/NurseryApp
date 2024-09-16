namespace NurseryApp.Application.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string body, List<string> emails, string title, string subject);
    }
}
