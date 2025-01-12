using SiapBackups.Configuration.Directories;
using SiapBackups.Configuration.Helpers.Paths;

namespace SiapBackups.Configuration.Helpers.Messages.Error;

public readonly struct ErrorMessages
{
    public const string FDB_NOT_FOUND = "✖ Erro: Nenhum arquivo .FDB foi enconrado dentro do diretório fornecido!\n";
    public const string EMPTY_DIRECTORY = "✖ Erro: O diretório informado está vazio!";
    public const string DIRECTORY_SEARCH_FAIL = $"\n✖ Erro: Erro ao localizar o diretório informado!";
    public const string ZIP_FAIL = $"\n✖ Erro: Erro no processo de compactação dos arquivos!";
    public const string MAIL_NOTIFICATION_FAIL = "✖ Erro ao enviar o e-mail de notificação!";
    public const string BKP_FAIL = "\n✖ Erro durante o processo de backup!\n";

    public static readonly string HOME_DIRECTORY_NOT_FOUND = $"✖ Erro: O diretório de origem '{BaseDirectory.HomeDirectory}' não foi encontrado!";

    public static readonly string DESTINATION_DIRECTORY_NOT_FOUD = $"✖ Erro: Diretório de origem não encontrado: {ConstPaths.FOLDER_NAME}!";
}