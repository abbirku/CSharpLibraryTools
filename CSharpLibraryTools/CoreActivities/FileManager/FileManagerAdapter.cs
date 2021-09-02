using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace CoreActivities.FileManager
{
    public interface IFileManager
    {
        void CreateFile(string filePath);
        Task<byte[]> ReadFileAsByteAsync(string filePath);
        Task SaveByteStreamAsync(string filePath, byte[] file);
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

        public async Task<byte[]> ReadFileAsByteAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new Exception("Provide valid file path read the file stream.");

            if (!_file.DoesExists(filePath))
                throw new Exception("File does not exists");

            return await _file.ReadFileAsByteAsync(filePath);
        }

        public async Task SaveByteStreamAsync(string filePath, byte[] file)
        {
            if (string.IsNullOrWhiteSpace(filePath) || file == null || file.Length == 0)
                throw new Exception("Provide valid file path and byte array to save the file stream.");

            await _file.WriteBytesStreamAsync(filePath, file);
        }

        public void SaveBitmapImage(string filePath, Bitmap bitmap)
        {
            if (string.IsNullOrWhiteSpace(filePath) || bitmap == null)
                throw new Exception("Provide valid file path and bitmap to save the image.");

            using var memory = new MemoryStream();
            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            bitmap.Save(memory, ImageFormat.Jpeg);
            var bytes = memory.ToArray();
            fs.Write(bytes, 0, bytes.Length);
        }
    }
}
