using CoreActivities.DirectoryManager;
using CoreActivities.EgmaCV;
using CoreActivities.FileManager;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpLibraryTools
{
    public class FileManagerImp
    {
        private readonly IDirectoryManager _directoryManager;
        private readonly IFile _file;
        private readonly IFileInfo _fileInfo;
        private readonly IFileManager _fileManager;
        private readonly IEgmaCv _egmaCv;

        public FileManagerImp(IDirectoryManager directoryManager,
            IFile file,
            IFileInfo fileInfo,
            IFileManager fileManager,
            IEgmaCv egmaCv)
        {
            _directoryManager = directoryManager;
            _file = file;
            _fileInfo = fileInfo;
            _fileManager = fileManager;
            _egmaCv = egmaCv;
        }

        public async Task Run()
        {
            #region IFile implementation
            var textList = new List<string>
                {
                    "This is me",
                    "I am the Dojo"
                };

            var singleText = "Hmm gots yoo";
            var folder = "CSharpLib";

            //The following creates CSharpLib folder under C:\ProgramData and returns full path of specified file
            var filePath = _directoryManager.CreateProgramDataFilePath(folder, "file.txt");
            var fileName = _file.FileName(filePath);

            Console.WriteLine($"FilePath: {filePath},\n FileName: {fileName}");

            //Checks the file does exists. If not then create
            if (!_file.DoesExists(filePath))
                _file.CreateFile(filePath);

            //Prints created file mime type
            Console.WriteLine($"MimeType: {_file.GetMimeType(filePath)}\n");

            //WriteAllTextAsync is same as WriteAllLineAsync but just write a string as text
            //Note: Using WriteAllLineAsync and WriteAllTextAsync will override text of each other
            await _file.WriteAllLineAsync(textList, filePath);

            //The following just add text on new line on a file
            await _file.AppendAllLineAsync(textList, filePath);
            await _file.AppendAllTextAsync(singleText, filePath);

            var allLines = await _file.ReadAllLineAsync(filePath);
            var singleLine = await _file.ReadAllTextAsync(filePath);

            Console.WriteLine("ReadAllLineAsync");
            Console.WriteLine("----------------");
            foreach (var item in allLines)
                Console.WriteLine(item);
            Console.WriteLine();

            Console.WriteLine("ReadAllTextAsync");
            Console.WriteLine("----------------");
            Console.WriteLine(singleLine);
            Console.WriteLine();

            Console.WriteLine("Printing base 64 string");
            Console.WriteLine("-----------------------");
            
            var bytes = await _file.ReadFileAsByteAsync(filePath);
            Console.WriteLine(_file.ConvertByteToBase64String(bytes));
            
            var streanFilePath = _directoryManager.CreateProgramDataFilePath(folder, "streanFile.txt");
            await _file.WriteBytesStreamAsync(streanFilePath, bytes);
            Console.WriteLine();
            
            #endregion

            #region IFileInfo Implementation
            Console.WriteLine($"Filesize: {_fileInfo.FileSize(filePath)} Bytes");
            Console.WriteLine($"IsReadonly: {_fileInfo.IsReadOnly(filePath)}");
            Console.WriteLine($"CreatedOn: {_fileInfo.CreatedOn(filePath)}");
            Console.WriteLine($"LastUpdateOn: {_fileInfo.LastUpdateOn(filePath)}");
            Console.WriteLine($"LastAccessOn: {_fileInfo.LastAccessOn(filePath)}");
            #endregion

            #region IFileManager Implementation
            var writtenTextFilePath = _directoryManager.CreateProgramDataFilePath(folder, "written_text.txt");
            _fileManager.CreateFile(writtenTextFilePath);
            
            var readBytes = await _fileManager.ReadFileAsByteAsync(filePath);
            await _fileManager.SaveByteStreamAsync(writtenTextFilePath, readBytes);
            #endregion
        }
    }
}
