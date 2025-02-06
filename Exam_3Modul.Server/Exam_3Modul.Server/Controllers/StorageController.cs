using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Exam_3Modul.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpPost("uploadFile")]
        public async Task UploadFile(IFormFile file, string? directoryPath)
        {
            directoryPath = directoryPath ?? string.Empty;
            directoryPath = Path.Combine(directoryPath, file.FileName);

            using (var stream = file.OpenReadStream())
            {
                await _storageService.UploadFile(directoryPath, stream);
            }
        }

        [HttpPost("uploadFiles")]
        public async Task UploadFiles(List<IFormFile> files, string? directoryPath)
        {
            directoryPath = directoryPath ?? string.Empty;
            var mainPath = directoryPath;
            if (files == null || files.Count == 0)
            {
                throw new Exception("files is empty or null");
            }

            foreach (var file in files)
            {
                directoryPath = Path.Combine(mainPath, file.FileName);

                using (var stream = file.OpenReadStream())
                {
                    await _storageService.UploadFile(directoryPath, stream);
                }
            }
        }

        [HttpPost("createFolder")]
        public async Task CreateFolder(string folderPath)
        {
            await _storageService.CreateDirectory(folderPath);
        }

        [HttpGet("getAll")]
        public async Task<List<string>> GetAllInFolderPath(string? directoryPath)
        {
            directoryPath = directoryPath ?? string.Empty;
            var res = await _storageService.GetAllInFolderPath(directoryPath);
            return res;
        }

        [HttpGet("downloadFile")]
        public async Task<FileStreamResult> DownloadFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("Error");
            }

            var fileName = Path.GetFileName(filePath);

            var stream = await _storageService.DownloadFile(filePath);


            var res = new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = fileName,
            };

            return res;
        }

        [HttpGet("downloadFolderAsZip")]
        public async Task<FileStreamResult> DownloadFolderAsZip(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new Exception("Error");
            }

            var directoryName = Path.GetFileName(directoryPath);

            var stream = await _storageService.DownloadFolderAsZip(directoryPath);

            try
            {
                var res = new FileStreamResult(stream, "application/octet-stream")
                {
                    FileDownloadName = directoryName + ".zip",
                };

                return res;
            }
            finally
            {
                //_storageService.DeleteFile(directoryPath + ".zip");
            }
        }

        [HttpGet("uploadGetContentOfTxtFile")]
        public async Task UploadGetContentOfTxtFile(string filePath, string newContent)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("Error");
            }

            var directoryName = Path.GetFileName(filePath);

            var stream = _storageService.UploadGetContentOfTxtFile(filePath, newContent);

            var res = new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = directoryName + ".txt",
            };
        }

        [HttpDelete("deleteFile")]
        public async Task DeleteFile(string filePath)
        {
            await _storageService.DeleteFile(filePath);
        }

        [HttpDelete("deleteFolder")]
        public async Task DeleteFolder(string directoryPath)
        {
            await _storageService.DeleteFolder(directoryPath);
        }

    }
}

