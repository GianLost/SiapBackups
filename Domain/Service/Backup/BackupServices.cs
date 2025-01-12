using SiapBackups.Configuration.Helpers.Messages.Success;
using SiapBackups.Configuration.Helpers.Messages.Error;
using SiapBackups.Configuration.Helpers.Messages.Tasks;
using SiapBackups.Configuration.Helpers.Paths;
using System.IO.Compression;

namespace SiapBackups.Domain.Service.Backup;

public static class BackupServices
{
    public static async Task<bool> CheckDirectories(string path)
    {
        bool existDirectory = string.IsNullOrEmpty(path.Trim()) && !Directory.Exists(path.Trim());

        if (existDirectory)
        {
            Console.WriteLine(ErrorMessages.HOME_DIRECTORY_NOT_FOUND);
            return await Task.FromResult(existDirectory);
        }

        Console.WriteLine(SuccessMessages.DIRECTORY_VERIFIED_SUCCESSFULLY);
        return await Task.FromResult(existDirectory);
    }

    public static async Task<string[]> ListFoundFiles()
    {
        try
        {
            Console.WriteLine(TaskMessages.TOTAL_FILES);

            if (ConstPaths.FILES.Length == 0)
                throw new ArgumentNullException(nameof(ConstPaths.FILES), ErrorMessages.FDB_NOT_FOUND);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"{ErrorMessages.FDB_NOT_FOUND} - EXCEPTION_MESSAGE: {ex.Message} {ex.InnerException}");
        }

        Console.WriteLine(TaskMessages.FILE_LIST);

        foreach (var file in ConstPaths.FILES)
        {
            string fileName = Path.GetFileName(file);
            Console.WriteLine($"- {fileName}");
        }

        return await Task.FromResult(ConstPaths.FILES);
    }

    public static async Task<DirectoryInfo> CreateNewDirectory()
    {
        var directoryInfo = Directory.CreateDirectory(ConstPaths.FOLDER_PATH);

        Console.WriteLine(TaskMessages.CREATED_NEW_DIRECTORY);

        return await Task.FromResult(directoryInfo);
    }

    public static void CopyFiles()
    {
        Console.WriteLine(TaskMessages.TANSFERRED_FILES);
        foreach (string file in ConstPaths.FILES)
        {
            string fileName = Path.GetFileName(file);
            string destFilePath = Path.Combine(ConstPaths.FOLDER_PATH, fileName);

            File.Copy(file, destFilePath);

            Console.WriteLine($"- {fileName}");
        }
    }

    public static void ZipFiles()
    {
        // Cria o arquivo .zip no diretório de destino com um nome único
        string finalZipFilePath = ConstPaths.ZIP_PATH;

        try
        {
            // Verifica se o diretório do backup existe antes de criar o arquivo ZIP
            if (!Directory.Exists(ConstPaths.FOLDER_PATH))
                throw new DirectoryNotFoundException(ErrorMessages.DESTINATION_DIRECTORY_NOT_FOUD);

            // Obtém todos os arquivos do diretório
            string[] files = Directory.GetFiles(ConstPaths.FOLDER_PATH, "*", SearchOption.AllDirectories);

            if (files.Length == 0)
                throw new InvalidOperationException(ErrorMessages.EMPTY_DIRECTORY);

            using (FileStream zipToOpen = new(finalZipFilePath, FileMode.Create))

            using (ZipArchive archive = new(zipToOpen, ZipArchiveMode.Create))
            {
                Console.WriteLine(TaskMessages.ZIP);
                int fileCount = files.Length;

                for (int i = 0; i < fileCount; i++)
                {
                    string file = files[i];
                    string entryName = Path.GetRelativePath(ConstPaths.FOLDER_PATH, file);
                    archive.CreateEntryFromFile(file, entryName, CompressionLevel.Optimal);

                    // Atualiza a barra de progresso
                    DrawProgressBar(i + 1, fileCount);
                }
            }

            Console.WriteLine();
            Console.WriteLine(SuccessMessages.ZIP_SUCCESS);
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine($"{ErrorMessages.DIRECTORY_SEARCH_FAIL} - EXCEPTION_MESSAGE: {ex.Message} {ex.InnerException}");
            throw;
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"{ErrorMessages.ZIP_FAIL} - EXCEPTION_MESSAGE: {ex.Message} {ex.InnerException}");
            throw;
        }
    }

    private static void DrawProgressBar(int current, int total)
    {
        int progressBarWidth = 50;
        double percentage = (double)current / total;
        int progress = (int)(percentage * progressBarWidth);

        Console.Write($"\r[{new string('#', progress)}{new string('-', progressBarWidth - progress)}] {percentage:P0}");
    }

}