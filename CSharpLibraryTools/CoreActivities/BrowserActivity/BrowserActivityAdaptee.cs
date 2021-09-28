﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                var browserProcess = GetBrowserProcessByBrowserType(browserType);

                if (browserProcess.Count > 0)
                {
                    var url = string.Empty;
                    foreach (var chrome in browserProcess)
                    {
                        // The browser process must have a window
                        if (chrome.Process.MainWindowHandle == IntPtr.Zero)
                            continue;

                        // Find the automation element
                        var elm = AutomationElement.FromHandle(chrome.Process.MainWindowHandle);
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
                var processes = GetBrowserProcessByBrowserType(browserType);

                if (processes.Count <= 0)
                    throw new Exception($"No process found with this {browserName} name");
                else
                {
                    foreach (var proc in processes)
                    {
                        if (proc.Process.MainWindowHandle != IntPtr.Zero)
                        {
                            var root = AutomationElement.FromHandle(proc.Process.MainWindowHandle);
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

        public IList<ProcessAndTitle> GetBrowserProcessByBrowserType(BrowserType browserType)
        {
            var browser = _browserActivityEnumAdaptee.ToDescriptionString(browserType).ToLower();
            var processes = Process.GetProcesses();

            return processes.Select(x => new ProcessAndTitle
            {
                MainWindowTitle = x.MainWindowTitle,
                Process = x
            }).Where(x => x.MainWindowTitle.ToLower().Contains(browser))
              .GroupBy(x => x.MainWindowTitle)
              .Select(x => x.First())
              .ToList();
        }
    }

    public class ProcessAndTitle
    {
        public string MainWindowTitle { get; set; }
        public Process Process { get; set; }
    }
}
