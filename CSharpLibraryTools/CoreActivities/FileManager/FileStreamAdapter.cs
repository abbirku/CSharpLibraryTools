using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreActivities.FileManager
{
    public class FileStreamAdapter : IFileStream
    {
        public void WriteFileBytes(string filePath, byte[] file)
        {
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(file, 0, file.Length);
        }
        public byte[] ReadFileAsByte(string filePath) => File.ReadAllBytes(filePath);
        public string ConvertByteToBase64String(byte[] file) => Convert.ToBase64String(file);
    }
}
