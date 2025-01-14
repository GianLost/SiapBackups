using SiapBackups.Domain.Models.Mail;
using SiapBackups.Domain.Service.Mail;
using SiapBackups.Domain.Service.Backup;
using SiapBackups.Configuration.Directories;
using SiapBackups.Configuration.Helpers.Paths;
using SiapBackups.Configuration.Helpers.Messages.Error;
using SiapBackups.Configuration.Helpers.Messages.Tasks;
using SiapBackups.Configuration.Helpers.Messages.Success;

// *** Iniciando o BKP... ***
Console.WriteLine();
Console.WriteLine(TaskMessages.START);

try
{
    // *** Verificando o diretório inicial... ***
    Console.WriteLine(TaskMessages.INITIAL_CHECK);
    await BackupServices.CheckDirectories(BaseDirectory.HomeDirectory);

    // *** Listando os arquivos do diretório inicial, se existirem... ***
    await BackupServices.ListFoundFiles();

    // *** Criando os caminhos de compactação e descompactação dos arquivos... ***
    string initialZipPath = Path.Combine(BaseDirectory.HomeDirectory, $"{ConstPaths.FOLDER_NAME}.zip");
    string finallyZipPath = Path.Combine(BaseDirectory.FinalDirectory, $"{ConstPaths.FOLDER_NAME}.zip");

    // *** Compactando os arquivos no diretório inicial... ***
    BackupServices.ZipFiles(BaseDirectory.HomeDirectory, initialZipPath);

    // *** Veficando a existencia do diretório final. Caso exista prossegue com a cópia do arquivo .zip, se não, o diretório é criado e o arquivo é copiado... ***
    Console.WriteLine(TaskMessages.FINAL_CHECK);
    await BackupServices.CreateFinalDirectory(BaseDirectory.FinalDirectory);

    // *** Iniciando a cópia do arquivo .zip para o diretório final... ***
    await BackupServices.CopyZipFile(initialZipPath, finallyZipPath);

    // *** Extraindo o arquivo no diretório final... ***
    await BackupServices.ExtractZipFile(finallyZipPath, BaseDirectory.FinalDirectory);

    // *** Excluindo o arquivo .zip no diretório final... ***
    await BackupServices.DeleteFile(finallyZipPath);

    // *** Enviando e-mail após a conclusão do backup ser realizada com sucesso... ***
    Console.WriteLine(TaskMessages.SEND_MAIL_NOTIFICATION);

    EmailModel mail = new()
    {
        Subject = SuccessMessages.BKP_SUCCESS,
        Body = SuccessMessages.MAIL_BODY_MESSAGE
    };

    await SmtpEmailSender.SendEmailAsync(mail);

    // *** Finalizando o BKP... ***
    Console.WriteLine(SuccessMessages.BKP_SUCCESS);
    Console.WriteLine(TaskMessages.END);
}
catch (Exception ex)
{
    // *** Erro no BKP... (Captura de erro genérico)***
    Console.WriteLine($"{ErrorMessages.BKP_FAIL} - EXCEPTION_MESSAGE: {ex.Message} {ex.InnerException}");
    Console.WriteLine(TaskMessages.END);
}