using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CoreActivities.FileManager
{
    public interface IFileManager
    {
        void CreateFile(string filePath);
        byte[] ReadFileAsByte(string filePath);
        void SaveByteStream(string filePath, byte[] file);
        void SaveBitmapImage(string filePath, Bitmap bitmap);
    }
}
