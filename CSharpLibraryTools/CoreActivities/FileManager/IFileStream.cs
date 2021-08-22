using System;
using System.Collections.Generic;
using System.Text;

namespace CoreActivities.FileManager
{
    public interface IFileStream
    {
        void WriteFileBytes(string filePath, byte[] file);
        byte[] ReadFileAsByte(string filePath);
        string ConvertByteToBase64String(byte[] file);
    }
}
