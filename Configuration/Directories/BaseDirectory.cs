using Microsoft.Extensions.Configuration;

namespace SiapBackups.Configuration.Directories;

public readonly struct BaseDirectory
{
    private static readonly IConfiguration Configuration;

    static BaseDirectory()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static string HomeDirectory => Configuration["Directories:HomeDirectory"]
        ?? throw new InvalidOperationException("O valor de 'Directories:HomeDirectory' não pode ser nulo ou vazio.");

    public static string FinalDirectory => Configuration["Directories:FinalDirectory"]
        ?? throw new InvalidOperationException("O valor de 'Directories:FinalDirectory' não pode ser nulo ou vazio.");
}