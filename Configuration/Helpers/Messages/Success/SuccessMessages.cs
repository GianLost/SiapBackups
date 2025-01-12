using SiapBackups.Configuration.Helpers.Paths;

namespace SiapBackups.Configuration.Helpers.Messages.Success;

public readonly struct SuccessMessages
{
    public const string DIRECTORY_VERIFIED_SUCCESSFULLY = "✓ Diretório verificado com sucesso!\n";
    public const string MAIL_NOTIFICATION_SUCCESS = "✓ E-mail de notificação enviado com sucesso! \n";
    public const string BKP_SUCCESS = "✓ Backup concluído com sucesso!";

    public static readonly string ZIP_SUCCESS = $"\n✓ Arquivos compactados com sucesso! {ConstPaths.ZIP_NAME}\n";

    public static readonly string MAIL_BODY_MESSAGE = $"O backup foi concluído com sucesso no diretório: {ConstPaths.FOLDER_NAME}";
}