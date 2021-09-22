using CoreActivities.GoogleDriveApi.Models;
using CoreActivities.GoogleDriveApi.Parms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
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
    public interface IGoogleDriveApiManager
    {
        Task<GoogleDriveFiles> GetFilesAndFolders(string nextPageToken = null, FilesListOptionalParms optional = null);
        Task<IList<File>> GetAllFilesAndFolders(FilesListOptionalParms optional = null);
        Task<File> UploadFileAsync(UploadFileInfo uploadFileInfo, Action<IUploadProgress> uploadProgress = null);
        Task DeleteAsync(string fileId, FilesDeleteOptionalParms optional = null);
        Task DownloadAsync(File file, string filePath, Action<IDownloadProgress> downloadProgress = null);
    }

    public class GoogleDriveApiManagerAdapter : IGoogleDriveApiManager
    {
        private readonly string _authfilePath;
        private readonly string _directoryId;
        private readonly DriveService _driveService;
        private long _fileSize = 0;
        private long _uploaded = 0;
        private long _downloaded = 0;

        public GoogleDriveApiManagerAdapter(string authfilePath,
            string directoryId)
        {
            _authfilePath = authfilePath;
            _directoryId = directoryId;

            var credential = GoogleCredential.FromFile(_authfilePath)
                                    .CreateScoped(DriveService.ScopeConstants.Drive);

            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
        }

        public async Task<GoogleDriveFiles> GetFilesAndFolders(string nextPageToken = null, FilesListOptionalParms optional = null)
        {

            return await Task.Run(() =>
            {
                try
                {
                    var files = new List<File>();

                    // Initial validation.
                    if (_driveService == null)
                        throw new ArgumentNullException("service");

                    //Providing default query parameter 'Q' to retrive only specific folder or files
                    var defaultQueryPatter = $"'{_directoryId}' in parents";

                    if (!string.IsNullOrWhiteSpace(_directoryId))
                    {
                        if (optional == null)
                            optional = new FilesListOptionalParms
                            {
                                Q = defaultQueryPatter
                            };
                        else if (optional != null && string.IsNullOrWhiteSpace(optional.Q))
                            optional.Q = defaultQueryPatter;
                    }

                    // Building the initial request.
                    var request = _driveService.Files.List();

                    // Applying optional parameters to the request.                
                    request = (FilesResource.ListRequest)ApplyOptionalParams(request, optional);

                    // Requesting data.
                    if (!string.IsNullOrWhiteSpace(nextPageToken))
                        request.PageToken = nextPageToken;

                    var fileFeed = request.Execute();

                    foreach (var item in fileFeed.Files)
                        files.Add(item);

                    var data = new GoogleDriveFiles
                    {
                        NextPageToken = fileFeed.NextPageToken,
                        Files = files
                    };

                    return data;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            });

        }

        public async Task<IList<File>> GetAllFilesAndFolders(FilesListOptionalParms optional = null)
        {
            GoogleDriveFiles results = null;
            FilesListOptionalParms optionals = null;
            var files = new List<File>();

            if(optional == null)
            {
                optionals = new FilesListOptionalParms
                {
                    PageSize = 5, //Provide positive integer for pagination
                    Fields = "nextPageToken, files(id, name, mimeType, kind, trashed)" //Follow this pattern to retrive only specified object fields
                };
            }

            do
            {
                if (results == null)
                    results = await GetFilesAndFolders(null, optionals);
                else
                    results = await GetFilesAndFolders(results.NextPageToken, optionals);

                files.AddRange(results.Files);

            } while (results != null && !string.IsNullOrEmpty(results.NextPageToken));

            return files;
        }

        public async Task<File> UploadFileAsync(UploadFileInfo uploadFileInfo, Action<IUploadProgress> uploadProgress = null)
        {
            //Initialization
            _uploaded = 0;

            // Initial validation.
            if (_driveService == null)
                throw new ArgumentNullException("service");

            if (uploadFileInfo != null && !string.IsNullOrWhiteSpace(uploadFileInfo.FilePath) &&
                !string.IsNullOrWhiteSpace(uploadFileInfo.FileName) && 
                !string.IsNullOrWhiteSpace(uploadFileInfo.MimeType) &&
                uploadFileInfo.FileSize != 0)
            {
                _fileSize = uploadFileInfo.FileSize;

                // Upload file Metadata
                var fileMetadata = new File()
                {
                    Name = uploadFileInfo.FileName,
                    Parents = new List<string>() { _directoryId }
                };

                // Create a new file on Google Drive
                await using var fsSource = new System.IO.FileStream(uploadFileInfo.FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // Create a new file, with metadata and stream.
                var request = _driveService.Files.Create(fileMetadata, fsSource, uploadFileInfo.MimeType);
                request.Fields = "*";
                request.ChunkSize = 262144;
                if (uploadProgress != null)
                    request.ProgressChanged += uploadProgress;
                else
                    request.ProgressChanged += UploadProgress;

                var results = await request.UploadAsync(CancellationToken.None);

                if (results.Status == UploadStatus.Failed)
                    throw new Exception($"Error uploading file: {results.Exception.Message}");

                return request.ResponseBody;
            }
            else
                throw new Exception("Provide valid upload file information");
        }

        public async Task DeleteAsync(string fileId, FilesDeleteOptionalParms optional = null)
        {
            await Task.Run(() =>
            {
                try
                {
                    // Initial validation.
                    if (_driveService == null)
                        throw new ArgumentNullException("service");

                    if (string.IsNullOrWhiteSpace(fileId))
                        throw new ArgumentNullException("fileId");

                    // Building the initial request.
                    var request = _driveService.Files.Delete(fileId);

                    // Applying optional parameters to the request.                
                    request = (FilesResource.DeleteRequest)ApplyOptionalParams(request, optional);

                    // Requesting data.
                    request.Execute();
                }
                catch (Exception ex)
                {
                    throw new Exception("Request Files.Delete failed.", ex);
                }
            });
        }

        public async Task DownloadAsync(File file, string filePath, Action<IDownloadProgress> downloadProgress = null)
        {
            if (file == null || string.IsNullOrWhiteSpace(filePath))
                throw new Exception("Provide valid file object and file path");

            try
            {
                var request = _driveService.Files.Get(file.Id);
                if (downloadProgress != null)
                    request.MediaDownloader.ProgressChanged += downloadProgress;
                else
                    request.MediaDownloader.ProgressChanged += DownloadProgress;

                using var output = System.IO.File.Create(filePath);
                await request.DownloadAsync(output);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }

        #region Private section
        public void ClearCurrentConsoleLine()
        {
            if (Console.CursorTop > 0)
                Console.SetCursorPosition(0, Console.CursorTop - 1);

            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        private void UploadProgress(IUploadProgress progress)
        {
            PrintUploadProgressByPercentage(progress.BytesSent, _fileSize);
        }

        private void DownloadProgress(IDownloadProgress progress)
        {
            _downloaded = 0;
            PrintDownloadProgress(progress.BytesDownloaded);
        }

        private void PrintUploadProgressByPercentage(long progress, long total)
        {
            ClearCurrentConsoleLine();

            _uploaded += progress;
            Console.WriteLine($"Uploaded: {100 * _uploaded / total}%");
        }

        private void PrintDownloadProgress(long progress)
        {
            ClearCurrentConsoleLine();

            _downloaded += progress;
            Console.WriteLine($"Downloaded: {(decimal)_downloaded / 1024} Kilo Bytes");
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

        private object ApplyOptionalParams(object request, object optional)
        {
            if (optional == null)
                return request;

            System.Reflection.PropertyInfo[] optionalProperties = (optional.GetType()).GetProperties();

            foreach (System.Reflection.PropertyInfo property in optionalProperties)
            {
                // Copy value from optional parms to the request.  They should have the same names and datatypes.
                System.Reflection.PropertyInfo piShared = (request.GetType()).GetProperty(property.Name);
                if (property.GetValue(optional, null) != null) // TODO Test that we do not add values for items that are null
                    piShared.SetValue(request, property.GetValue(optional, null), null);
            }

            return request;
        }
        #endregion
    }
}
