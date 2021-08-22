using System;
using System.Collections.Generic;
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
        Task WriteAllLineAsync(List<string> lines, string filePath);
        Task WriteAllTextAsync(string text, string filePath);
        Task AppendAllLineAsync(List<string> lines, string filePath);
        Task AppendAllTextAsync(string text, string filePath);
        Task<string[]> ReadAllLineAsync(string filePath);
        Task<string> ReadAllTextAsync(string filePath);
    }
}
