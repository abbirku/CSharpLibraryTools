using CoreActivities.ActiveProgram;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpLibraryTools
{
    public class Application
    {
        private readonly IActiveProgram _activeProgram;

        public Application(IActiveProgram activeProgram)
        {
            _activeProgram = activeProgram;
        }

        public void Run()
        {
            var title = _activeProgram.CaptureActiveProgramTitle();
            Console.WriteLine($"Present active program title is {title}");
        }
    }
}
