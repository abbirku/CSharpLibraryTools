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
Follow _ActiveProgramImp_

### **Browser Activity**
_**Dependencies**_
- System.Diagnostics.Process

_**Interface & Its members**_
- IBrowserActivity

| Member | Details |
| ------ | ------ |
| EnlistAllOpenTabs(BrowserType browserType) | Returns list of open tabs title of a browser. |
| EnlistActiveTabUrl(BrowserType browserType) | Returns url string of present visting website in a browser. |

_**Notes**_
**BrowserType** is an Enum which contains **Chrome**, **FireFox**, **Edge**, **Opera**, **Safari**

_**Usage**_
Follow _BrowseActivityImp_