using CoreActivities.RunningPrograms;
using System;

namespace CSharpLibraryTools.Implementations
{
    public class RunningProgramImp
    {
        private readonly IRunningPrograms _runningPrograms;

        public RunningProgramImp(IRunningPrograms runningPrograms)
        {
            _runningPrograms = runningPrograms;
        }

        public void Run()
        {
            var runningProcesses = _runningPrograms.GetRunningProcessList();
            var runningPrograms = _runningPrograms.GetRunningProgramsList();

            Console.WriteLine("Running Processes");
            foreach (var item in runningProcesses)
                Console.WriteLine(item);

            Console.WriteLine("Running Programs");
            foreach (var item in runningPrograms)
                Console.WriteLine(item);
        }
    }
}
