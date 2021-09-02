using CoreActivities.ActiveProgram;
using System;
using System.Threading.Tasks;

namespace CSharpLibraryTools
{
    public class ActiveProgramImp
    {
        private readonly IActiveProgram _activeProgram;

        public ActiveProgramImp(IActiveProgram activeProgram)
            => _activeProgram = activeProgram;

        public async Task Run()
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"Active Program: {_activeProgram.CaptureActiveProgramTitle()}");
            });
        }
    }
}
