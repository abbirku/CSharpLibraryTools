using CoreActivities.FileManager;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Upload;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreActivities.GoogleDriveApi
{
    public interface IIGoogleDriveApiManager
    {
        Task<string> UploadFileAsync(string filePath, Action<IUploadProgress> uploadProgress = null);
    }

    public class GoogleDriveApiManagerAdapter : IIGoogleDriveApiManager
    {
        private readonly string _authfilePath;
        private readonly string _directoryId;
        private readonly IFile _file;
        private readonly IFileInfo _fileInfo;
        private long _fileSize;

        public GoogleDriveApiManagerAdapter(string authfilePath,
            string directoryId,
            IFile file,
            IFileInfo fileInfo)
        {
            _authfilePath = authfilePath;
            _directoryId = directoryId;
            _file = file;
            _fileInfo = fileInfo;
            _fileSize = 0;
        }

        public async Task<string> UploadFileAsync(string filePath, Action<IUploadProgress> uploadProgress = null)
        {
            //Gathering file size at the very begining
            _fileSize = _fileInfo.FileSize(filePath);

            var credential = GoogleCredential.FromFile(_authfilePath)
                                    .CreateScoped(DriveService.ScopeConstants.Drive);

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });

            if (!string.IsNullOrWhiteSpace(filePath) && _file.DoesExists(filePath))
            {
                // Upload file Metadata
                var fileMetadata = new File()
                {
                    Name = _file.FileName(filePath),
                    Parents = new List<string>() { _directoryId }
                };

                // Create a new file on Google Drive
                await using var fsSource = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // Create a new file, with metadata and stream.
                var request = service.Files.Create(fileMetadata, fsSource, _file.GetMimeType(filePath));
                request.Fields = "*";
                request.ChunkSize = 262144;
                if (uploadProgress != null)
                    request.ProgressChanged += uploadProgress;
                else
                    request.ProgressChanged += UploadProgress;

                var results = await request.UploadAsync(CancellationToken.None);

                if (results.Status == UploadStatus.Failed)
                    throw new Exception($"Error uploading file: {results.Exception.Message}");

                return request.ResponseBody?.Id;
            }
            else
                throw new Exception("Provide valid file path");
        }

        private void UploadProgress(IUploadProgress progress)
        {
            DrawTextProgressBar(progress.BytesSent, _fileSize);
        }

        private void DrawTextProgressBar(long progress, long total)
        {
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;

            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + total.ToString() + "    "); //blanks at the end remove any excess
        }
    }
}
