using System.IO.Compression;
using SiapBackups.Configuration.Helpers.Paths;
using SiapBackups.Configuration.Helpers.Messages.Error;
using SiapBackups.Configuration.Helpers.Messages.Tasks;
using SiapBackups.Configuration.Helpers.Messages.Success;

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
        if (ConstPaths.FILES.Length == 0)
            throw new ArgumentNullException(nameof(ConstPaths.FILES), ErrorMessages.FDB_NOT_FOUND);

        Console.WriteLine(TaskMessages.TOTAL_FILES);

        try
        {
            Console.WriteLine(TaskMessages.FILE_LIST);

            foreach (var file in ConstPaths.FILES)
            {
                string fileName = Path.GetFileName(file);
                Console.WriteLine($"- {fileName}");
            }

        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"{ErrorMessages.FDB_NOT_FOUND} - EXCEPTION_MESSAGE: {ex.Message} {ex.InnerException}");
        }

        return await Task.FromResult(ConstPaths.FILES);
    }

    public static void ZipFiles(string sourceDirectory, string zipFilePath)
    {
        try
        {
            if (!Directory.Exists(sourceDirectory))
                throw new DirectoryNotFoundException(ErrorMessages.DESTINATION_DIRECTORY_NOT_FOUD);

            string[] files = Directory.GetFiles(sourceDirectory, ConstPaths.FILE_TYPE, SearchOption.AllDirectories);

            if (files.Length == 0)
                throw new InvalidOperationException(ErrorMessages.EMPTY_DIRECTORY);

            using (FileStream zipToOpen = new(zipFilePath, FileMode.Create))
            using (ZipArchive archive = new(zipToOpen, ZipArchiveMode.Create))
            {
                Console.WriteLine(TaskMessages.ZIP);

                int totalFiles = files.Length;
                int processedFiles = 0;

                foreach (var file in files)
                {
                    string entryName = Path.GetRelativePath(sourceDirectory, file);
                    archive.CreateEntryFromFile(file, entryName, CompressionLevel.Optimal);

                    processedFiles++;
                    DrawProgressBar(processedFiles, totalFiles);
                }
            }

            Console.WriteLine();
            Console.WriteLine(SuccessMessages.ZIP_SUCCESS);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ErrorMessages.ZIP_FAIL} - EXCEPTION_MESSAGE: {ex.Message} {ex.InnerException}");
            throw;
        }
    }

    public static async Task CreateFinalDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Console.WriteLine(TaskMessages.CREATE_NEW_DIRECTORY);
            Directory.CreateDirectory(path);
            Console.WriteLine(SuccessMessages.CREATED_NEW_DIRECTORY);
        }
        else
        {
            Console.WriteLine(TaskMessages.DIRECTORY_ALREADY_EXIST);
        }

        await Task.CompletedTask;
    }

    public static async Task CopyZipFile(string sourcePath, string destinationPath)
    {
        try
        {
            FileInfo zipFile = new(sourcePath);
            long fileSize = zipFile.Length;
            long copiedSize = 0;

            using (FileStream sourceStream = new(sourcePath, FileMode.Open, FileAccess.Read))
            using (FileStream destStream = new(destinationPath, FileMode.Create, FileAccess.Write))
            {
                Console.WriteLine(TaskMessages.COPY_FILE);

                byte[] buffer = new byte[8192];
                int bytesRead;
                int progress = 0;

                while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    destStream.Write(buffer, 0, bytesRead);
                    copiedSize += bytesRead;

                    int newProgress = (int)((double)copiedSize / fileSize * 50); // Barra de 50 caracteres
                    if (newProgress > progress)
                    {
                        DrawProgressBar(newProgress, 50);
                        progress = newProgress;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine(SuccessMessages.COPY_ZIP_SUCCESS);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ErrorMessages.COPY_ZIP_FAIL} - EXCEPTION_MESSAGE: {ex.Message} {ex.InnerException}");
            throw;
        }

        await Task.CompletedTask;
    }

    public static async Task ExtractZipFile(string zipPath, string extractPath)
    {
        try
        {
            Console.WriteLine(TaskMessages.UNZIP);

            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                int totalEntries = archive.Entries.Count;
                for (int i = 0; i < totalEntries; i++)
                {
                    ZipArchiveEntry entry = archive.Entries[i];
                    string destinationPath = Path.Combine(extractPath, entry.FullName);

                    // Valida o destino antes de criar diretórios
                    string directoryPath = Path.GetDirectoryName(destinationPath) ?? throw new DirectoryNotFoundException();

                    if (!string.IsNullOrEmpty(entry.Name)) // Ignora diretórios
                    {
                        if (!string.IsNullOrEmpty(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        entry.ExtractToFile(destinationPath, overwrite: true);
                    }

                    DrawProgressBar(i + 1, totalEntries);
                }
            }

            Console.WriteLine();
            Console.WriteLine(SuccessMessages.UNZIP_SUCCESS);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ErrorMessages.UNZIP_FAIL} - EXCEPTION_MESSAGE: {ex.Message} {ex.InnerException}");
            throw;
        }

        await Task.CompletedTask;
    }

    public static async Task DeleteFile(string filePath)
    {
        try
        {
            await Task.Delay(500);
            if (File.Exists(filePath))
            {
                Console.WriteLine(TaskMessages.DELETING_FILE);
                File.Delete(filePath);
                Console.WriteLine(SuccessMessages.DELETED_FILE_SUCCESS);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ErrorMessages.DELETED_FILE_FAIL} - EXCEPTION_MESSAGE: {ex.Message} {ex.InnerException}");
            throw;
        }

        await Task.CompletedTask;
    }

    private static void DrawProgressBar(int current, int total)
    {
        int progressBarWidth = 50;
        double percentage = (double)current / total;
        int progress = (int)(percentage * progressBarWidth);

        Console.Write($"\r[{new string('#', progress)}{new string('-', progressBarWidth - progress)}] {percentage:P0}");
    }

}