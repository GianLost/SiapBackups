using SiapBackups.Configuration.Directories;
using SiapBackups.Configuration.Helpers.Paths;

namespace SiapBackups.Configuration.Helpers.Messages.Error;

public readonly struct ErrorMessages
{
    public const string FDB_NOT_FOUND = "✖ Erro: Nenhum arquivo .FDB foi enconrado dentro do diretório fornecido!\n";
    public const string EMPTY_DIRECTORY = "✖ Erro: O diretório informado está vazio!";
    public const string DIRECTORY_SEARCH_FAIL = $"\n✖ Erro: Erro ao localizar o diretório informado!";
    public const string ZIP_FAIL = $"\n✖ Erro: Erro no processo de compactação dos arquivos!";
    public const string UNZIP_FAIL = $"\n✖ Erro: Erro no processo de descompactação dos arquivos!";
    public const string COPY_ZIP_FAIL = $"\n✖ Erro: Erro no processo de transferência do arquivo compactado!";
    public const string DELETED_FILE_FAIL = "\n✖ Falha ao tentar remover o arquivo .zip!\n";
    public const string MAIL_NOTIFICATION_FAIL = "✖ Erro ao enviar o e-mail de notificação para: \n";
    public const string BKP_FAIL = "\n✖ Erro durante o processo de backup!\n";
    public const string SMTM_CREDENTIALS_INVALID = "\n✖ As credenciais de smtp são nulas ou incompatíveis! \n";

    public static readonly string HOME_DIRECTORY_NOT_FOUND = $"✖ Erro: O diretório de origem '{BaseDirectory.HomeDirectory}' não foi encontrado!";

    public static readonly string DESTINATION_DIRECTORY_NOT_FOUD = $"✖ Erro: Diretório de origem não encontrado: {ConstPaths.FOLDER_NAME}!";
}