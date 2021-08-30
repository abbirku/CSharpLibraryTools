using System;

namespace CoreActivities.ActiveProgram
{
    public interface IActiveProgram
    {
        string CaptureActiveProgramTitle();
    }

    public class ActiveProgramAdapter : IActiveProgram
    {
        private readonly ActiveProgramAdaptee _activeProgramAdaptee;

        public ActiveProgramAdapter(ActiveProgramAdaptee activeProgramAdaptee)
            => _activeProgramAdaptee = activeProgramAdaptee;

        public string CaptureActiveProgramTitle()
        {
            var windowTitle = _activeProgramAdaptee.GetActiveWindowTitle();
            if (string.IsNullOrWhiteSpace(windowTitle))
                throw new Exception("No valid active program title has found");

            if (windowTitle.Contains("\\"))
                return windowTitle.Split("\\")[^1];
            else
                return windowTitle;
        }
    }
}
