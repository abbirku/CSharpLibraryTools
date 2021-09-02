using CoreActivities.DirectoryManager;
using CoreActivities.EgmaCV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibraryTools
{
    public class EgmaCvImp
    {
        private readonly IDirectoryManager _directoryManager;
        private readonly IEgmaCv _egmaCv;

        public EgmaCvImp(IDirectoryManager directoryManager,
            IEgmaCv egmaCv)
        {
            _directoryManager = directoryManager;
            _egmaCv = egmaCv;
        }

        public async Task Run()
        {
            //The following capture image from webcam and store it on a file
            var fileName = $"{Guid.NewGuid()}.jpg";
            var filePath = _directoryManager.CreateProgramDataFilePath("CSharpLib", fileName);
            await _egmaCv.CaptureImageAsync(0, filePath);
            Console.WriteLine($"Image Captured. FilePath: {filePath}");
        }
    }
}
