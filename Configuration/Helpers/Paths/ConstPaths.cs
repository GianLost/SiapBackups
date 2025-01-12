using SiapBackups.Configuration.DateTime;
using SiapBackups.Configuration.Directories;

namespace SiapBackups.Configuration.Helpers.Paths;

public readonly struct ConstPaths
{
    public const string FILE_TYPE = "*.FDB";
    public const string DATE_TIME_FORMAT = "dd-MM-yyyy HH\\H mm\\m ss\\s";

    public static readonly string[] FILES = Directory.GetFiles(BaseDirectory.HomeDirectory, FILE_TYPE);

    public static readonly string FOLDER_NAME = $"BKP_SIAP {DateAndBaseHour.CurrentDateTime.ToString(DATE_TIME_FORMAT)}";

    public static readonly string FOLDER_PATH = Path.Combine(BaseDirectory.Finaldirectory, FOLDER_NAME);

    public static readonly string ZIP_NAME = $"{FOLDER_NAME} {Guid.NewGuid()}.zip";

    public static readonly string ZIP_PATH = Path.Combine(FOLDER_PATH, ZIP_NAME);
}