

using StorageBroker.Services;

namespace Service.Services;

public class StorageService : IStorageService
{
    private readonly IStorageBrokerService _storageBrokerService;
    public async Task CreateFolder(string folderPath)
    {
        await _storageBrokerService.CreateFolder(folderPath);
    }

    public async Task DeleteFile(string filePath)
    {
        await _storageBrokerService.DeleteFile(filePath);
    }

    public async Task DeleteFolder(string folderPath)
    {
        await _storageBrokerService.DeleteFolder(folderPath);
    }

    public async Task<Stream> DownloadFile(string filePath)
    {
        var res = await _storageBrokerService.DownloadFile(filePath);
        return res;
    }

    public async Task<Stream> DownloadFolder(string folderPath)
    {
        var res = await _storageBrokerService.DownloadFolder(folderPath);
        return res;
    }

    public async Task<Stream> DownloadFolderAsZip(string folderPath)
    {
        var res = await _storageBrokerService.DownloadFolderAsZip(folderPath);
        return res;
    }

    public async Task GetAllInFolderPath(string? folderPath)
    {
        await _storageBrokerService.GetAllInFolderPath(folderPath);
    }

    public async Task GetContentOfTxtFile()
    {
        await _storageBrokerService.GetContentOfTxtFile();
    }

    public async Task UploadFile(string? filePath, Stream stream)
    {
        await _storageBrokerService.UploadFile(filePath, stream);
    }

    public async Task UploadFileWithChunks(string? filePath, Stream stream)
    {
        await _storageBrokerService.UploadFileWithChunks(filePath, stream);
    }

    public async Task UploadGetContentOfTxtFile(string filePath, string newContent)
    {
        await _storageBrokerService.UploadGetContentOfTxtFile(filePath, newContent);
    }
}
