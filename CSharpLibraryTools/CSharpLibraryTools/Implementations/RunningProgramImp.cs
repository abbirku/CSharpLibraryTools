using CoreActivities.RunningPrograms;
using System;
using System.Threading.Tasks;

namespace CSharpLibraryTools
{
    public class RunningProgramImp
    {
        private readonly IRunningPrograms _runningPrograms;

        public RunningProgramImp(IRunningPrograms runningPrograms)
        {
            _runningPrograms = runningPrograms;
        }

        public async Task Run()
        {
            await Task.Run(() =>
            {
                var runningProcesses = _runningPrograms.GetRunningProcessList();
                var runningPrograms = _runningPrograms.GetRunningProgramsList();

                Console.WriteLine("Running Processes");
                foreach (var item in runningProcesses)
                    Console.WriteLine(item);
                Console.WriteLine();

                Console.WriteLine("Running Programs");
                foreach (var item in runningPrograms)
                    Console.WriteLine(item);
                Console.WriteLine();
            });
        }
    }
}
