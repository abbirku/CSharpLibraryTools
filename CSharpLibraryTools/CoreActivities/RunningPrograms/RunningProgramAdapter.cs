using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CoreActivities.RunningPrograms
{
    public interface IRunningPrograms
    {
        List<string> GetRunningProgramsList();
        List<string> GetRunningProcessList();
    }

    public class RunningProgramAdapter : IRunningPrograms
    {
        public List<string> GetRunningProcessList()
        {
            var processes = Process.GetProcesses();
            var processNames = new List<string>();

            foreach (var process in processes)
            {
                if (!string.IsNullOrWhiteSpace(process.ProcessName))
                    processNames.Add(process.ProcessName);
            }

            processNames = processNames.GroupBy(x => x).Select(x => x.Key).ToList();

            return processNames;
        }

        public List<string> GetRunningProgramsList()
        {
            var processes = Process.GetProcesses();
            var titles = new List<string>();

            foreach (var process in processes)
            {
                if (!string.IsNullOrWhiteSpace(process.MainWindowTitle))
                    titles.Add(process.MainWindowTitle);
            }

            titles = titles.GroupBy(x => x).Select(x => x.Key).ToList();

            return titles;
        }
    }
}
