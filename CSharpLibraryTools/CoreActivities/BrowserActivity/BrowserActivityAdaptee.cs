using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Automation;

namespace CoreActivities.BrowserActivity
{
    public class BrowserActivityAdaptee
    {
        private readonly BrowserActivityEnumAdaptee _browserActivityEnumAdaptee;

        public BrowserActivityAdaptee(BrowserActivityEnumAdaptee browserActivityEnumAdaptee)
            => _browserActivityEnumAdaptee = browserActivityEnumAdaptee;

        public string GetActiveTabUrl(BrowserType browserType)
        {
            try
            {
                // There are always multiple chrome processes, so we have to loop through all of them to find the
                // process with a Window Handle and an automation element of name "Address and search bar"

                var browserName = _browserActivityEnumAdaptee.ToDescriptionString(browserType);
                var browserProcess = Process.GetProcessesByName(browserName);

                if (browserProcess.Length > 0)
                {
                    var url = string.Empty;
                    foreach (var chrome in browserProcess)
                    {
                        // The browser process must have a window
                        if (chrome.MainWindowHandle == IntPtr.Zero)
                            continue;

                        // Find the automation element
                        var elm = AutomationElement.FromHandle(chrome.MainWindowHandle);
                        var elmUrlBar = elm.FindFirst(TreeScope.Descendants,
                            new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));

                        // If it can be found, get the value from the URL bar
                        if (elmUrlBar != null)
                        {
                            var patterns = elmUrlBar.GetSupportedPatterns();
                            if (patterns.Length > 0)
                            {
                                var val = (ValuePattern)elmUrlBar.GetCurrentPattern(patterns[0]);
                                url = val.Current.Value;
                            }
                        }
                    }

                    return url;
                }
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<string> GetOpenTabsInfos(BrowserType browserType)
        {
            try
            {
                var tabInfos = new List<string>();

                var browserName = _browserActivityEnumAdaptee.ToDescriptionString(browserType);
                var processes = Process.GetProcessesByName(browserName);

                if (processes.Length <= 0)
                    throw new Exception($"No process found with this {browserName} name");
                else
                {
                    foreach (var proc in processes)
                    {
                        if (proc.MainWindowHandle != IntPtr.Zero)
                        {
                            var root = AutomationElement.FromHandle(proc.MainWindowHandle);
                            var condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
                            var tabs = root.FindAll(TreeScope.Descendants, condition);
                            var enumerator = tabs.GetEnumerator();

                            while (enumerator.MoveNext())
                            {
                                var info = (AutomationElement)enumerator.Current;
                                tabInfos.Add(info.Current.Name);
                            }
                        }
                    }
                }

                return tabInfos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
