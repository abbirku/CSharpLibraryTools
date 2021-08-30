using System;

namespace CoreActivities.DirectoryManager
{
    public interface IDirectoryManager
    {
        string GetProgramDataDirectoryPath(string appFolder);
        bool ChecknCreateDirectory(string directoryPath);
        string CreateProgramDataFilePath(string folderName, string fileName);
    }

    public class DirectoryManagerAdapter : IDirectoryManager
    {
        private readonly DirectoryManagerAdaptee _directoryManagerAdaptee;

        public DirectoryManagerAdapter(DirectoryManagerAdaptee directoryManagerAdaptee)
            => _directoryManagerAdaptee = directoryManagerAdaptee;

        /// <summary>
        /// Given a directory path of a folder it creates the folder under the directory if not exists
        /// </summary>
        public bool ChecknCreateDirectory(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                return false;

            if (!_directoryManagerAdaptee.Exists(directoryPath))
            {
                _directoryManagerAdaptee.CreateDirectory(directoryPath);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Create a file path of given folder name which is under C:\ProgramData
        /// </summary>
        public string GetProgramDataDirectoryPath(string appFolder)
        {
            if (string.IsNullOrWhiteSpace(appFolder))
                throw new Exception("App folder string is empty.");

            return $"{_directoryManagerAdaptee.CommonApplicationPath}\\{appFolder}";
        }

        /// <summary>
        /// Create a file path (If not exists) under given folderName in C:\ProgramData
        /// </summary>
        public string CreateProgramDataFilePath(string folderName, string fileName)
        {
            if (string.IsNullOrWhiteSpace(folderName) || string.IsNullOrWhiteSpace(fileName))
                throw new Exception("Given folder or File name is empty.");

            var directory = GetProgramDataDirectoryPath(folderName);
            ChecknCreateDirectory(directory);

            return $"{directory}\\{fileName}";
        }
    }
}
