using System;
using System.Collections.Generic;
using System.Text;

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
}
