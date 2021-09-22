using CoreActivities.DirectoryManager;
using CoreActivities.FileManager;
using CoreActivities.GoogleDriveApi;
using CoreActivities.GoogleDriveApi.Models;
using CoreActivities.RunningPrograms;
using System;
using System.Threading.Tasks;

namespace CSharpLibraryTools
{
    public class GoogleDriveApiImp
    {
        private readonly IRunningPrograms _runningPrograms;
        private readonly IDirectoryManager _directoryManager;
        private readonly IFile _file;
        private readonly IFileInfo _fileInfo;
        private readonly IFileManager _fileManager;
        private readonly IGoogleDriveApiManager _googleDriveApiManager;

        public GoogleDriveApiImp(IRunningPrograms runningPrograms,
            IDirectoryManager directoryManager,
            IFile file,
            IFileInfo fileInfo,
            IFileManager fileManager,
            IGoogleDriveApiManager googleDriveApiManager)
        {
            _runningPrograms = runningPrograms;
            _directoryManager = directoryManager;
            _file = file;
            _fileInfo = fileInfo;
            _fileManager = fileManager;
            _googleDriveApiManager = googleDriveApiManager;
        }

        public async Task Run()
        {
            try
            {
                //Creating upload file path
                var folder = "CSharpLib";
                var filePath = _directoryManager.CreateProgramDataFilePath(folder, $"google-drive-api-{Guid.NewGuid()}.txt");
                _fileManager.CreateFile(filePath);

                //Creating download file path
                var downloadDir = $"{folder}Downlaod";
                var downloadFilePath = _directoryManager.CreateProgramDataFilePath(downloadDir, $"google-drive-api-download-{Guid.NewGuid()}.txt");
                _fileManager.CreateFile(downloadFilePath);

                //Gathering running program informations and writing info to upload file
                var runningPrograms = _runningPrograms.GetRunningProgramsList();
                await _file.WriteAllLineAsync(runningPrograms, filePath);

                //Uploading the written file
                Console.WriteLine("Uploading...");
                var file = await _googleDriveApiManager.UploadFileAsync(new UploadFileInfo
                {
                    FileName = _file.FileName(filePath),
                    FilePath = filePath,
                    FileSize = _fileInfo.FileSize(filePath),
                    MimeType = _file.GetMimeType(filePath)
                });

                //Print present file information
                Console.WriteLine("Printing...");
                await PrintFilesInAGoogleDirectory();
                Console.WriteLine();

                //Downloading the uploaded file in download directory
                Console.WriteLine("Downloading...");
                await _googleDriveApiManager.DownloadAsync(file, downloadFilePath);

                //Deleting the uploaded file
                Console.WriteLine("Deleting...");
                await _googleDriveApiManager.DeleteAsync(file.Id);

                //Print present file information
                Console.WriteLine("Printing...");
                await PrintFilesInAGoogleDirectory();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PrintFilesInAGoogleDirectory()
        {
            var counter = 0;
            var files = await _googleDriveApiManager.GetAllFilesAndFolders();

            foreach (var item in files)
            {
                Console.WriteLine($"SL: {counter} Name: {item.Name}");
                counter++;
            }
        }
    }
}
