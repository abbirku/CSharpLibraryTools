namespace CoreActivities.DirectoryManager
{
    public interface IDirectoryManager
    {
        string GetProgramDataDirectoryPath(string appFolder);
        bool ChecknCreateDirectory(string directoryPath);
        string CreateProgramDataFilePath(string folderName, string fileName);
    }
}
