using SiapBackups.Configuration.Helpers.Messages.Success;
using SiapBackups.Configuration.Helpers.Messages.Error;
using SiapBackups.Configuration.Helpers.Messages.Tasks;
using SiapBackups.Configuration.Directories;
using SiapBackups.Domain.Service.Backup;
using SiapBackups.Domain.Service.Mail;
using SiapBackups.Domain.Models.Mail;

Console.WriteLine();
Console.WriteLine(TaskMessages.START);

try
{
    // Verificar o diretório inicial
    Console.WriteLine(TaskMessages.INITIAL_CHECK);
    await BackupServices.CheckDirectories(BaseDirectory.HomeDirectory);

    // Listar os arquivos enontrados no diretório (ajuda de auditoria)
    await BackupServices.ListFoundFiles();

    // Criando novo diretório para armazenagem do backup
    Console.WriteLine(TaskMessages.CREATE_NEW_DIRECTORY);
    await BackupServices.CreateNewDirectory();

    // Copiando arquivos para o diretório final
    Console.WriteLine(TaskMessages.COPY_FILES);
    BackupServices.CopyFiles();

    // Compactando arquivos para adicionar ao diretório final
    BackupServices.ZipFiles();

    // Envio de Email após a conclusão do backup ser realizada com sucesso
    Console.WriteLine(TaskMessages.SEND_MAIL_NOTIFICATION);
    await SmtpEmailSender.SendEmailAsync(EmailModel.SenderEmail, SuccessMessages.BKP_SUCCESS, SuccessMessages.MAIL_BODY_MESSAGE);

    Console.WriteLine(SuccessMessages.BKP_SUCCESS);
    Console.WriteLine(TaskMessages.END);
}
catch (Exception ex)
{
    Console.WriteLine($"{ErrorMessages.BKP_FAIL} - EXCEPTION_MESSAGE: {ex.Message} {ex.InnerException}");
    Console.WriteLine(TaskMessages.END);
}