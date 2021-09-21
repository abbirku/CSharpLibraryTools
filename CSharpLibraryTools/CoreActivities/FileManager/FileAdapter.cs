using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CoreActivities.FileManager
{
    public interface IFile
    {
        string FileName(string filePath);
        bool DoesExists(string filePath);
        void CreateFile(string filePath);
        string GetMimeType(string filePath);
        Task<byte[]> ReadFileAsByteAsync(string filePath);
        string ConvertByteToBase64String(byte[] file);
        Task WriteBytesStreamAsync(string filePath, byte[] file);
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

        public void CreateFile(string filePath)
        {
            var stream = File.Create(filePath);
            stream.Close();
        }

        public bool DoesExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public string GetMimeType(string filePath)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out string contentType))
                contentType = "application/octet-stream";

            return contentType;
        }

        public async Task<byte[]> ReadFileAsByteAsync(string filePath)
        {
            byte[] result;
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                result = new byte[stream.Length];
                await stream.ReadAsync(result, 0, (int)stream.Length);
            }

            return result;
        }

        public string ConvertByteToBase64String(byte[] file) => Convert.ToBase64String(file);

        public async Task WriteBytesStreamAsync(string filePath, byte[] file)
        {
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            await fileStream.WriteAsync(file, 0, file.Length);
        }

        public async Task WriteAllLineAsync(List<string> lines, string filePath)
        {
            await File.WriteAllLinesAsync(filePath, lines);
        }

        public async Task WriteAllTextAsync(string text, string filePath)
        {
            await File.WriteAllTextAsync(filePath, text);
        }

        public async Task AppendAllLineAsync(List<string> lines, string filePath)
        {
            await File.AppendAllLinesAsync(filePath, lines);
        }

        public async Task AppendAllTextAsync(string text, string filePath)
        {
            await File.AppendAllTextAsync(filePath, text);
        }

        public async Task<string[]> ReadAllLineAsync(string filePath)
        {
            return await File.ReadAllLinesAsync(filePath);
        }

        public async Task<string> ReadAllTextAsync(string filePath)
        {
            return await File.ReadAllTextAsync(filePath);
        }
    }
}
