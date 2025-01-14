using SiapBackups.Configuration.Directories;
using SiapBackups.Configuration.Helpers.Paths;

namespace SiapBackups.Configuration.Helpers.Messages.Success;

public readonly struct SuccessMessages
{
    public const string DIRECTORY_VERIFIED_SUCCESSFULLY = "✓ Diretório verificado com sucesso!\n";
    public const string MAIL_NOTIFICATION_SUCCESS = "✓ E-mail de notificação enviado com sucesso para:";
    public const string BKP_SUCCESS = "✓ Backup concluído com sucesso!";

    public static readonly string ZIP_SUCCESS = $"\n✓ Arquivos compactados com sucesso em: {BaseDirectory.HomeDirectory}\\{ConstPaths.ZIP_NAME} !\n";

    public static readonly string CREATED_NEW_DIRECTORY = $"✓ Novo diretório criado com sucesso! {BaseDirectory.FinalDirectory}\n";

    public static readonly string COPY_ZIP_SUCCESS = $"\n✓ Arquivo compactado transferido com sucesso para: {BaseDirectory.FinalDirectory}";

    public static readonly string UNZIP_SUCCESS = $"\n✓ Arquivo {ConstPaths.ZIP_NAME} descompactado com sucesso em: {BaseDirectory.FinalDirectory}";

    public static readonly string DELETED_FILE_SUCCESS = $"\n✓ Arquivo {ConstPaths.ZIP_NAME} removido com sucesso de: {BaseDirectory.FinalDirectory} !\n";

    public static readonly string MAIL_BODY_MESSAGE = $"O backup foi concluído com sucesso no diretório: {ConstPaths.FOLDER_NAME}";
}