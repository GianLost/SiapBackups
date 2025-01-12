using SiapBackups.Configuration.Helpers.Paths;

namespace SiapBackups.Configuration.Helpers.Messages.Tasks;

public readonly struct TaskMessages
{
    public const string START = "⏳ Iniciando Backup da base de dados...\n";
    public const string INITIAL_CHECK = "⏳ Verificando diretório inicial...\n";
    public const string CREATE_NEW_DIRECTORY = "\n⏳ Iniciando a criação do diretório final para o backup...\n";
    public const string COPY_FILES = "⏳ Iniciando a cópia de arquivos...\n";
    public const string FILE_LIST = "✓ Arquivos localizados no diretório\n";
    public const string TANSFERRED_FILES = "✓ Arquivo copiado\n";
    public const string ZIP = "\n⏳ Iniciando compactação...\n";
    public const string SEND_MAIL_NOTIFICATION = "✉️ Enviando e-mail de notificação...\n";
    public const string END = "\nFIM...\n";

    public static readonly string TOTAL_FILES = $"[>] Total de arquivos encontrados no diretório de origem: {ConstPaths.FILES.Length} itens\n";

    public static readonly string CREATED_NEW_DIRECTORY = $"[>] Novo diretório criado: {ConstPaths.FOLDER_PATH}\n";
}