using CoreActivities.ActiveProgram;
using System;

namespace CSharpLibraryTools
{
    public class ActiveProgramImp
    {
        private readonly IActiveProgram _activeProgram;

        public ActiveProgramImp(IActiveProgram activeProgram)
            => _activeProgram = activeProgram;

        public void Run()
        {
            Console.WriteLine($"Active Program: {_activeProgram.CaptureActiveProgramTitle()}");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
