namespace StorageBroker.Services;

public interface IStorageBrokerService
{
    Task CreateFolder(string folderPath);
    Task UploadFile(string? filePath, Stream stream);
    Task UploadFileWithChunks(string? filePath, Stream stream);
    Task DeleteFile(string filePath);
    Task DeleteFolder(string folderPath);
    Task<Stream> DownloadFile(string filePath);
    Task<Stream> DownloadFolder(string folderPath);
    Task<Stream> DownloadFolderAsZip(string folderPath);
    Task GetContentOfTxtFile();
    Task UploadGetContentOfTxtFile(string filePath, string newContent);
    Task GetAllInFolderPath(string? folderPath);    

}