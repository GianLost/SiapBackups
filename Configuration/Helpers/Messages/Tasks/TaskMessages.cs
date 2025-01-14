using SiapBackups.Configuration.Directories;
using SiapBackups.Configuration.Helpers.Paths;

namespace SiapBackups.Configuration.Helpers.Messages.Tasks;

public readonly struct TaskMessages
{
    public const string START = "⏳ Iniciando Backup da base de dados...\n";
    public const string INITIAL_CHECK = "⏳ Verificando diretório inicial...\n";
    public const string FINAL_CHECK = "⏳ Verificando diretório final...\n";
    public const string CREATE_NEW_DIRECTORY = "⏳ O diretório final não existe! Iniciando a criação do diretório final para o backup...\n";
    public const string FILE_LIST = "[>] Arquivos localizados no diretório:\n";
    public const string DIRECTORY_ALREADY_EXIST = "[>] O diretório já existe! Prosseguindo para a transferência do arquivo...\n";
    public const string TANSFERRED_FILES = "✓ Arquivo copiado\n";
    public const string ZIP = "\n⏳ Iniciando compactação...\n";
    public const string UNZIP = "\n⏳ Iniciando a descompactação...\n";
    public const string SEND_MAIL_NOTIFICATION = "✉️ Enviando e-mail de notificação...\n";
    public const string END = "\nFIM...\n";

    public static readonly string COPY_FILE = $"⏳ Iniciando a cópia do arquivo {BaseDirectory.HomeDirectory}\\{ConstPaths.ZIP_NAME} ...\n";

    public static readonly string TOTAL_FILES = $"[>] Total de arquivos encontrados no diretório de origem: {ConstPaths.FILES.Length} itens\n";

    public static readonly string DELETING_FILE = $"\n⏳ Excluindo o arquivo {ConstPaths.ZIP_NAME} do diretório final {BaseDirectory.FinalDirectory}";
}