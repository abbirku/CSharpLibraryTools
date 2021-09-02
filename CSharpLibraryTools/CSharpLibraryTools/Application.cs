using System;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpLibraryTools
{
    public class Application
    {
        private readonly ActiveProgramImp _activeProgramImp;
        private readonly BrowseActivityImp _browseActivityImp;
        private readonly DirectoryManagerImp _directoryManagerImp;
        private readonly EgmaCvImp _egmaCv;
        private readonly FileManagerImp _fileManagerImp;
        private readonly RunningProgramImp _runningProgramImp;
        private readonly ScreenCaptureImp _screenCaptureImp;
        private readonly GoogleDriveApiImp _googleDriveApiImp;

        public Application(ActiveProgramImp activeProgramImp,
            BrowseActivityImp browseActivityImp,
            DirectoryManagerImp directoryManagerImp,
            EgmaCvImp egmaCv,
            FileManagerImp fileManagerImp,
            RunningProgramImp runningProgramImp,
            ScreenCaptureImp screenCaptureImp,
            GoogleDriveApiImp googleDriveApiImp)
        {
            _activeProgramImp = activeProgramImp;
            _browseActivityImp = browseActivityImp;
            _directoryManagerImp = directoryManagerImp;
            _egmaCv = egmaCv;
            _fileManagerImp = fileManagerImp;
            _runningProgramImp = runningProgramImp;
            _screenCaptureImp = screenCaptureImp;
            _googleDriveApiImp = googleDriveApiImp;
        }

        public async Task Run()
        {
            try
            {
                //TitlePrinter("Active Program Information");
                //await _activeProgramImp.Run();
                //PrintSeperator(20);

                //TitlePrinter("Browser Activity");
                //await _browseActivityImp.Run();
                //PrintSeperator(20);

                //TitlePrinter("Directory Manager");
                //await _directoryManagerImp.Run();
                //PrintSeperator(20);

                //TitlePrinter("Webcam");
                //await _egmaCv.Run();
                //PrintSeperator(20);

                //TitlePrinter("File Manager");
                //await _fileManagerImp.Run();
                //PrintSeperator(20);

                //TitlePrinter("Running Programs");
                //await _runningProgramImp.Run();
                //PrintSeperator(20);

                TitlePrinter("Screen Capture");
                await _screenCaptureImp.Run();
                PrintSeperator(20);

                //TitlePrinter("Google Drive Api");
                //await _googleDriveApiImp.Run();
                //PrintSeperator(20);
            }
            catch (Exception ex)
            {

            }
        }

        public void TitlePrinter(string title)
            => Console.WriteLine($"{title}\n{new string('-', title.Count())}");

        public void PrintSeperator(int length)
        {
            var seperator = new string('=', length);
            Console.WriteLine($"{seperator}\n{seperator}\n");
        }
    }
}
