using System;
using System.IO;

namespace CoreActivities.FileManager
{
    public interface IFileInfo
    {
        long FileSize(string filePath);
        bool IsReadOnly(string filePath);
        DateTime CreatedOn(string filePath);
        DateTime LastAccessOn(string filePath);
        DateTime LastUpdateOn(string filePath);
    }

    public class FileInfoAdapter : IFileInfo
    {
        private FileInfo ObjectCreationAndValidation(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new Exception("Provide valid file path");

            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
                throw new Exception("File does not exists");

            return fileInfo;
        }

        public DateTime CreatedOn(string filePath) => ObjectCreationAndValidation(filePath).CreationTime;

        public long FileSize(string filePath) => ObjectCreationAndValidation(filePath).Length;

        public bool IsReadOnly(string filePath) => ObjectCreationAndValidation(filePath).IsReadOnly;

        public DateTime LastAccessOn(string filePath) => ObjectCreationAndValidation(filePath).LastAccessTime;

        public DateTime LastUpdateOn(string filePath) => ObjectCreationAndValidation(filePath).LastWriteTime;
    }
}
