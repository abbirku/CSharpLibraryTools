using System;
using System.IO;

namespace CoreActivities.DirectoryManager
{
    public class DirectoryManagerAdaptee
    {
        public string CommonApplicationPath => Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        public bool Exists(string directory) => Directory.Exists(directory);
        public void CreateDirectory(string directory) => Directory.CreateDirectory(directory);
    }
}
