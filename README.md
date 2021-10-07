# *CSharpLibraryTools*

## *Aim*

Aim of this project is to separate daily code in a SOLID manner so that we could re-use the code again without reinventing the wheel.

## *Library Architecture & Details*

- Each library item is a package. Just need to register the package with Autofac and use the interfaces. There is no overhead of registering library dependencies. Package registration is enough. 
- Each library try to follow Adapter Pattern so that user could do unit testing.
- Also there are implementations for each library item for easer understanding.
- And last we have used console app to simulate library results.

## *Library List*

- Active Program
- Browser Activity
- Directory Manager
- EgmaCV
- File Manager
- Google Drive Api
- Running Programs
- Screen Capture

### **Active Program**
_**Dependencies**_
- user32.dll

_**Interface & Its members**_
- IActiveProgram

| Member | Details |
| ------ | ------ |
| CaptureActiveProgramTitle() | Returns string of current active foreground program on windows. Null otherwise. |

_**Usage**_
- Follow _ActiveProgramImp_

### **Browser Activity**
_**Dependencies**_
- System.Diagnostics.Process

_**Interface & Its members**_
- IBrowserActivity

| Member | Details |
| ------ | ------ |
| EnlistAllOpenTabs(BrowserType browserType) | Returns list of open tabs title of a browser. |
| EnlistActiveTabUrl(BrowserType browserType) | Returns url string of present visting website in a browser. |
| EnlistActiveTabTitle(BrowserType browserType) | Returns tab title of present visting website in a browser. |
| IsBrowserOpen(BrowserType browserType) | Returns bool. |

_**Notes**_
- **BrowserType** is an Enum which contains **Chrome**, **FireFox**, **Edge**, **Opera**, **Safari**

_**Usage**_
- Follow _DirectoryManagerImp_

### **Directory Manager**
_**Dependencies**_
- System.Environment
- System.IO.Directory

_**Interface & Its members**_
- IDirectoryManager

| Member | Details |
| ------ | ------ |
| GetProgramDataDirectoryPath(string appFolder) | Returns directory path of C:\ProgramData with given {appFolder}. |
| ChecknCreateDirectory(string directoryPath) | Creates a folder and returns bool. **Note:** {directoryPath} must contain a folder name. |
| CreateProgramDataFilePath(string folderName, string fileName) | Creates a folder under C:\ProgramData for {folderName} and return file path for given {fileName}. i.e. C:\ProgramData\\{folderName}\\{FileN} |

_**Notes**_
- **BrowserType** is an Enum which contains **Chrome**, **FireFox**, **Edge**, **Opera**, **Safari**

_**Usage**_
- Follow _BrowseActivityImp_

### **EgmaCV**
_**Dependencies**_
- nuget package Egma.CV (4.5.3.4721)

_**Interface & Its members**_
- IEgmaCv

| Member | Details |
| ------ | ------ |
| CaptureImageAsync(int camIndex, string filePath) | Capture a picture from webcam for given {camIndex} and valid .jpg file path. |

_**Usage**_
- Follow _EgmaCvImp_

### **File Manager**
_**Dependencies**_
- System.IO
- System.Drawing

_**Interface & Its members**_
- IFile

| Member | Details |
| ------ | ------ |
| FileName(string filePath) | Returns file name for a given filePath. |
| DoesExists(string filePath) | Check if a file exists or not for a given filePath. Returns bool |
| CreateFile(string filePath) | Creates a file for a given file path. |
| GetMimeType(string filePath) | Returns mime type for a given file path. |
| ReadFileAsByteAsync(string filePath) | For a given file path return bytes of a file. |
| ConvertByteToBase64String(byte[] file) | For given byte array return base 64 string. |
| WriteBytesStreamAsync(string filePath, byte[] file) | Write bytes in a file for a given {filePath}. |
| WriteAllLineAsync(List<string> lines, string filePath) | Write list of string to a file. |
| WriteAllTextAsync(string text, string filePath) | Write string to a file. |
| AppendAllLineAsync(List<string> lines, string filePath) | Append list of string to a file. |
| AppendAllTextAsync(string text, string filePath) | Append a string to a file. |
| ReadAllLineAsync(string filePath) | Read all line of text file and retuns string array. |
| ReadAllTextAsync(string filePath) | Read all text of text file and retuns string. |

- IFileInfo

| Member | Details |
| ------ | ------ |
| FileSize(string filePath) | Returns file size for a given filePath. |
| IsReadOnly(string filePath) | Check if a file is readonly or not. Returns bool |
| CreatedOn(string filePath) | Returns file created date time for a given filePath. |
| LastAccessOn(string filePath) | Returns file last access date time for a given filePath. |
| LastUpdateOn(string filePath) | Returns file last update date time for a given filePath. |

- IFileManager

| Member | Details |
| ------ | ------ |
| CreateFile(string filePath) | Creates a file for a given file path. |
| ReadFileAsByteAsync(string filePath) | Read file for a given filePath and return byte array |
| SaveByteStreamAsync(string filePath, byte[] file) | Save bytes to a file for a given filePath. |
| SaveBitmapImage(string filePath, Bitmap bitmap) | Save Bitmap to a file for a given filePath. |

_**Notes**_
- **IFileManager** and **IFile** both have **CreateFile** and **ReadFileAsByteAsync**. It is preferred to use **IFileManager** members. 

_**Usage**_
- Follow _FileManagerImp_

### **Google Drive Api Manager**
_**Dependencies**_
- Google.Apis.Drive.v3 (1.54.0.2397)

_**Interface & Its members**_
- IGoogleDriveApiManager

| Member | Details |
| ------ | ------ |
| GetFilesAndFolders(string nextPageToken = null, FilesListOptionalParms optional = null) | Returns file and folder informations for an authenticated google drive folder. |
| UploadFileAsync(UploadFileInfo uploadFileInfo, Action<IUploadProgress> uploadProgress = null) | Upload a file in a google drive autneticated folder. Can check upload progress by providing IUploadProgress as delegate. |
| DeleteAsync(string fileId, FilesDeleteOptionalParms optional = null) | Delete a file from google drive by fileId. |
| DownloadAsync(File file, string filePath, Action<IDownloadProgress> downloadProgress = null) | Download a file by providing google drive file type. Can check download progress by providing IDownloadProgress delegate. |

_**Models**_
- FilesListOptionalParms
- FilesDeleteOptionalParms
- UploadFileInfo

_**Notes**_
- Need google drive .json authentication file. Like webcamsync.json.
- Follow appsettings.json

_**Usage**_
- Follow _GoogleDriveApiImp_

### **Running Programs**
_**Dependencies**_
- System.Process

_**Interface & Its members**_
- IRunningPrograms

| Member | Details |
| ------ | ------ |
| GetRunningProgramsList() | Returns all running process. |
| GetRunningProcessList() | Returns all running foreground program names. |


_**Usage**_
- Follow _RunningProgramImp_

### **Screen Capture**
_**Dependencies**_
- System.Drawing

_**Interface & Its members**_
- IScreenCapture

| Member | Details |
| ------ | ------ |
| CaptureUserScreen(int width, int height) | Capture desktop screen for given width and height and return Bitmap |

_**Usage**_
- Follow _ScreenCaptureImp_

_**Observation Notes**_
- Follow .csproj if fall into dependency issue.
- Follow Program.cs for Package registration.