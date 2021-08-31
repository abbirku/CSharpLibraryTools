using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace CoreActivities.FileManager
{
    public interface IFileManager
    {
        void CreateFile(string filePath);
        byte[] ReadFileAsByte(string filePath);
        void SaveByteStream(string filePath, byte[] file);
        void SaveBitmapImage(string filePath, Bitmap bitmap);
    }

    public class FileManagerAdapter : IFileManager
    {
        private readonly IFile _file;

        public FileManagerAdapter(IFile file)
            => _file = file;

        public void CreateFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new Exception("Provide valid file path");

            if (!_file.DoesExists(path))
                _file.CreateFile(path);
        }

        public byte[] ReadFileAsByte(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new Exception("Provide valid file path read the file stream.");

            if (!_file.DoesExists(filePath))
                throw new Exception("File does not exists");

            return _file.ReadFileAsByte(filePath);
        }

        public void SaveByteStream(string filePath, byte[] file)
        {
            if (string.IsNullOrWhiteSpace(filePath) || file == null || file.Length == 0)
                throw new Exception("Provide valid file path and byte array to save the file stream.");

            _file.WriteBytesStream(filePath, file);
        }

        public void SaveBitmapImage(string filePath, Bitmap bitmap)
        {
            if (string.IsNullOrWhiteSpace(filePath) || bitmap == null)
                throw new Exception("Provide valid file path and bitmap to save the image.");

            bitmap.Save(filePath, ImageFormat.Jpeg);
        }
    }
}
