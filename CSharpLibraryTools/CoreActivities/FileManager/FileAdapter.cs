using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CoreActivities.FileManager
{
    public interface IFile
    {
        string FileName(string filePath);
        bool DoesExists(string filePath);
        void CreateFile(string filePath);
        string GetMimeType(string fileName);
        byte[] ReadFileAsByte(string filePath); //
        string ConvertByteToBase64String(byte[] file); //
        void WriteBytesStream(string filePath, byte[] file); //
        Task WriteAllLineAsync(List<string> lines, string filePath);
        Task WriteAllTextAsync(string text, string filePath);
        Task AppendAllLineAsync(List<string> lines, string filePath);
        Task AppendAllTextAsync(string text, string filePath);
        Task<string[]> ReadAllLineAsync(string filePath);
        Task<string> ReadAllTextAsync(string filePath);
    }

    public class FileAdapter : IFile
    {
        public string FileName(string filePath) => Path.GetFileName(filePath);

        public void CreateFile(string filePath) => File.Create(filePath);

        public bool DoesExists(string filePath) => File.Exists(filePath);

        public string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out string contentType))
                contentType = "application/octet-stream";

            return contentType;
        }

        public byte[] ReadFileAsByte(string filePath) => File.ReadAllBytes(filePath);

        public string ConvertByteToBase64String(byte[] file) => Convert.ToBase64String(file);

        public void WriteBytesStream(string filePath, byte[] file)
        {
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(file, 0, file.Length);
        }

        public async Task WriteAllLineAsync(List<string> lines, string filePath) => await File.WriteAllLinesAsync(filePath, lines);

        public async Task WriteAllTextAsync(string text, string filePath) => await File.WriteAllTextAsync(filePath, text);

        public async Task AppendAllLineAsync(List<string> lines, string filePath) => await File.AppendAllLinesAsync(filePath, lines);

        public async Task AppendAllTextAsync(string text, string filePath) => await File.AppendAllTextAsync(filePath, text);

        public async Task<string[]> ReadAllLineAsync(string filePath) => await File.ReadAllLinesAsync(filePath);

        public async Task<string> ReadAllTextAsync(string filePath) => await File.ReadAllTextAsync(filePath);
    }
}
