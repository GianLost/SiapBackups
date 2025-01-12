namespace SiapBackups.Domain.Interfaces.Mail;
public interface IEmailSender
{
    Task SendEmailAsync(string recipient, string subject, string body, bool isHtml = false);
}