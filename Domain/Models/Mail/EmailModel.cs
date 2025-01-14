namespace SiapBackups.Domain.Models.Mail;

public struct EmailModel
{
    public EmailModel() { }

    public string SenderEmail { get; set; } = default!;
    public string SenderPassword { get; set; } = default!;
    public string RecipientEmail { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Body { get; set; } = default!;

    public bool IsHtml = false;
}