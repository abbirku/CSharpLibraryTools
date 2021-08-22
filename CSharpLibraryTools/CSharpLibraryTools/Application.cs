using CoreActivities.ActiveProgram;
using CoreActivities.BrowserActivity;
using CoreActivities.DirectoryManager;
using CoreActivities.EgmaCV;
using System;
using System.Threading.Tasks;

namespace CSharpLibraryTools
{
    public class Application
    {
        private readonly IActiveProgram _activeProgram;
        private readonly IBrowserActivity _browserActivity;
        private readonly IDirectoryManager _directoryManager;
        private readonly IEgmaCv _egmaCv;

        public Application(IActiveProgram activeProgram,
            IBrowserActivity browserActivity,
            IDirectoryManager directoryManager,
            IEgmaCv egmaCv)
        {
            _activeProgram = activeProgram;
            _browserActivity = browserActivity;
            _directoryManager = directoryManager;
            _egmaCv = egmaCv;
        }

        public async Task Run()
        {
            var title = _activeProgram.CaptureActiveProgramTitle();
            Console.WriteLine($"Present active program title is {title}");

            Console.WriteLine("-------------------------------------------");

            var tabs = _browserActivity.EnlistAllOpenTabs(BrowserType.Chrome);
            foreach (var item in tabs)
                Console.WriteLine(item);

            var activeUrl = _browserActivity.EnlistActiveTabUrl(BrowserType.Chrome);
            Console.WriteLine($"Active URL: {activeUrl}");

            Console.WriteLine("-------------------------------------------");

            Console.WriteLine(_directoryManager.GetProgramDataDirectoryPath("WebCam"));
            _directoryManager.ChecknCreateDirectory("WebCam");
            Console.WriteLine(_directoryManager.CreateProgramDataFilePath("WebCam", "file.txt"));
            
            Console.WriteLine("-------------------------------------------");
            var rand = new Random();
            await _egmaCv.CaptureImageAsync(0, _directoryManager.CreateProgramDataFilePath("WebCam", $"{rand.Next(10, 9999)}.jpg"));
        }
    }
}
