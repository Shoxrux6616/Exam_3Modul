
using System.IO;
using System.IO.Compression;

namespace StorageBroker.Services;

public class StorageBrokerService : IStorageBrokerService
{
    private string _dataPath;

    public StorageBrokerService()
    {
        _dataPath = Path.Combine(Directory.GetCurrentDirectory(), "data");
        if (!Directory.Exists(_dataPath))
        {
            throw new Exception("Eror");
        }
        Directory.CreateDirectory(_dataPath);
    }


    public async Task CreateFolder(string folderPath)
    {
        _dataPath = Path.Combine(_dataPath, folderPath);
        Validate(folderPath);
        Directory.CreateDirectory(_dataPath);
    }

    public async Task DeleteFile(string filePath)
    {
        filePath = Path.Combine(_dataPath, filePath);

        if (!File.Exists(filePath))
        {
            throw new Exception("File not found delete");
        }

        Directory.Delete(filePath, true);
    }

    public async Task DeleteFolder(string folderPath)
    {
        folderPath = Path.Combine(_dataPath, folderPath);
        if (!File.Exists(folderPath))
        {
            throw new Exception("Foldar not found delete");
        }

        Directory.Delete(folderPath, true);
    }

    public async Task<Stream> DownloadFile(string filePath)
    {
        filePath = Path.Combine(_dataPath, filePath);

        if (!File.Exists(filePath))
        {
            throw new Exception();
        }

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return stream;
    }

    public async Task<Stream> DownloadFolder(string folderPath)
    {
        folderPath = Path.Combine(_dataPath, folderPath);

        if (!File.Exists(folderPath))
        {
            throw new Exception();
        }

        var stream = new FileStream(folderPath, FileMode.Open, FileAccess.Read);
        return stream;
    }

    public Task<Stream> DownloadFolderAsZip(string folderPath)
    {

        if (Path.GetExtension(folderPath) != string.Empty)
        {
            throw new Exception("DirectoryPath is not directory");
        }

        folderPath = Path.Combine(_dataPath, folderPath);
        if (!Directory.Exists(folderPath))
        {
            throw new Exception("Directory not found to download");
        }

        var zipFile = folderPath + ".zip";
        ZipFile.CreateFromDirectory(folderPath, zipFile);

        var stream = new FileStream(zipFile, FileMode.Open, FileAccess.Read);

        return stream;
    }

    public async Task GetAllInFolderPath(string? folderPath)
    {
        folderPath = Path.Combine(_dataPath, folderPath);

        var parentPath = Directory.GetParent(folderPath);

        if (!Directory.Exists(parentPath.FullName))
        {
            throw new Exception("Parent folder path not found");
        }

        var allFilesAndFolders = Directory.GetFileSystemEntries(folderPath).ToList();

        allFilesAndFolders = allFilesAndFolders.Select(p => p.Remove(0, folderPath.Length + 1)).ToList();
    }

    public async Task GetContentOfTxtFile()
    {
        
    }

    public async Task UploadFile(string filePath, Stream stream)
    {
        filePath = Path.Combine(_dataPath, filePath);
        var patentPath = Directory.GetParent(filePath);

        if (Directory.Exists(patentPath.FullName))
        {
            throw new Exception("Parent file path not found");
        }

        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            stream.CopyTo(fileStream);
        }
    }

    public async Task UploadFileWithChunks(string? filePath, Stream stream)
    {
        filePath = Path.Combine(_dataPath, filePath);
        var patentPath = Directory.GetParent(filePath);

        if (Directory.Exists(patentPath.FullName))
        {
            throw new Exception("Parent folder path not found");
        }

        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            stream.CopyTo(fileStream);
        }
    }

    public async Task UploadGetContentOfTxtFile(string filePath, string newContent)
    {
        filePath = Path.Combine(_dataPath, filePath);
        var patentPath = Directory.GetParent(filePath);

        if (Directory.Exists(patentPath.FullName))
        {
            throw new Exception("Parent folder path not found");
        }

        
    }

    private void Validate(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            throw new Exception("Folder has already created");
        }

        var parentPath = Directory.GetParent(directoryPath);

        if (!Directory.Exists(parentPath.FullName))
        {
            throw new Exception("Parent folder path not found");
        }
    }
}
