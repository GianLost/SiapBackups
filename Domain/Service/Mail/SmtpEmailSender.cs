using System.Net;
using System.Net.Mail;
using SiapBackups.Domain.Models.Mail;
using Microsoft.Extensions.Configuration;
using SiapBackups.Configuration.Helpers.Messages.Error;
using SiapBackups.Configuration.Helpers.Messages.Success;

namespace SiapBackups.Domain.Service.Mail
{
    public class SmtpEmailSender
    {
        private static readonly IConfiguration _configuration;

        static SmtpEmailSender()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static async Task SendEmailAsync(EmailModel mail)
        {
            // Obtém as credenciais do appsettings.json
            var smtpCredentials = _configuration.GetSection("Smtp_Credentials").Get<List<EmailModel>>()
                ?? throw new ArgumentNullException(nameof(mail), "As credenciais de SMTP são nulas ou incompatíveis!");

            foreach (var credential in smtpCredentials)
            {
                // Usa as credenciais específicas de cada iteração
                using SmtpClient smtpClient = new(DomainMailModel.SmtpHost, DomainMailModel.SmtpPort)
                {
                    Credentials = new NetworkCredential(credential.SenderEmail, credential.SenderPassword),
                    EnableSsl = true
                };

                MailMessage mailMessage = new()
                {
                    From = new MailAddress(credential.SenderEmail),
                    Subject = mail.Subject,
                    Body = mail.Body,
                    IsBodyHtml = mail.IsHtml
                };

                // Adiciona o destinatário específico para cada credencial
                mailMessage.To.Add(credential.RecipientEmail);

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);

                    Console.WriteLine($"{SuccessMessages.MAIL_NOTIFICATION_SUCCESS} {credential.RecipientEmail}");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ErrorMessages.MAIL_NOTIFICATION_FAIL} {credential.RecipientEmail} \n EXCEPTION_MESSAGE: {ex.Message} \n EXCEPTION: {ex.InnerException}");
                    Console.WriteLine();
                    throw;
                }
            }
        }
    }
}