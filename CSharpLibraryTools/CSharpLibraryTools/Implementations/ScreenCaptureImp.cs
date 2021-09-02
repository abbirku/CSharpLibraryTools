using CoreActivities.DirectoryManager;
using CoreActivities.FileManager;
using CoreActivities.ScreenCapture;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibraryTools
{
    public class ScreenCaptureImp
    {
        private readonly IScreenCapture _screenCapture;
        private readonly IDirectoryManager _directoryManager;
        private readonly IFileManager _fileManager;
        private readonly IFile _file;

        public ScreenCaptureImp(IScreenCapture screenCapture,
            IDirectoryManager directoryManager,
            IFileManager fileManager,
            IFile file)
        {
            _screenCapture = screenCapture;
            _directoryManager = directoryManager;
            _fileManager = fileManager;
            _file = file;
        }

        public async Task Run()
        {
            await Task.Run(() =>
            {
                var folder = "CSharpLib";

                Console.WriteLine("Capturing and storing image");

                var filePath = _directoryManager.CreateProgramDataFilePath(folder, $"{Guid.NewGuid()}.jpg");
                _fileManager.CreateFile(filePath);

                var capturedScreen = _screenCapture.CaptureUserScreen(1920, 1080);
                _fileManager.SaveBitmapImage(filePath, capturedScreen);

                Console.WriteLine($"Image Captured. FilePath: {filePath}");
            });
        }
    }
}
