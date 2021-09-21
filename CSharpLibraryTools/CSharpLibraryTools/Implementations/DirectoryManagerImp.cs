using CoreActivities.DirectoryManager;
using System;
using System.Threading.Tasks;

namespace CSharpLibraryTools
{
    public class DirectoryManagerImp
    {
        private readonly IDirectoryManager _directoryManager;

        public DirectoryManagerImp(IDirectoryManager directoryManager)
        {
            _directoryManager = directoryManager;
        }

        public async Task Run()
        {
            await Task.Run(() =>
            {
                //The following prints the windows program data directorty path
                Console.WriteLine($"ProgramData Directory: {_directoryManager.GetProgramDataDirectoryPath("Foobar")}");

                //The following creates a directory under a drive and returns bool as created or not
                var isCreated = _directoryManager.ChecknCreateDirectory("Foobar");
                if (isCreated)
                    Console.WriteLine("Has created Foobar under C:\\ProgramData");
                else
                    Console.WriteLine("The directory already exists. No need to create it.");

                //The following prints the file path of program data directory
                var filePath = _directoryManager.CreateProgramDataFilePath("Foobar", "file.txt");
                Console.WriteLine($"File path: {filePath}\n");
            });
        }
    }
}
