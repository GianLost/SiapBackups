using SiapBackups.Configuration.Helpers.Messages.Error;
using SiapBackups.Configuration.Helpers.Messages.Success;
using SiapBackups.Domain.Models.Mail;
using System.Net.Mail;
using System.Net;

namespace SiapBackups.Domain.Service.Mail
{
    public class SmtpEmailSender
    {
        public static async Task SendEmailAsync(string recipient, string subject, string body, bool isHtml = false)
        {
            using SmtpClient smtpClient = new(DomainMailModel.SmtpHost, DomainMailModel.SmtpPort)
            {
                Credentials = new NetworkCredential(EmailModel.SenderEmail, DomainMailModel.FirstSenderPassword),
                EnableSsl = true
            };

            MailMessage mailMessage = new()
            {
                From = new MailAddress(EmailModel.SenderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            mailMessage.To.Add(recipient);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine(SuccessMessages.MAIL_NOTIFICATION_SUCCESS);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ErrorMessages.MAIL_NOTIFICATION_FAIL} - EXCEPTION_MESSAGE: {ex.Message} {ex.InnerException}");
                throw;
            }
        }
    }
}