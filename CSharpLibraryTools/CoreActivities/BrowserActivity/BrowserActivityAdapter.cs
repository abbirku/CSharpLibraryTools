using System;
using System.Collections.Generic;

namespace CoreActivities.BrowserActivity
{
    public interface IBrowserActivity
    {
        IList<string> EnlistAllOpenTabs(BrowserType browserType);
        string EnlistActiveTabUrl(BrowserType browserType);
    }

    public class BrowserActivityAdapter : IBrowserActivity
    {
        private readonly BrowserActivityAdaptee _browserActivityAdaptee;
        private readonly BrowserActivityEnumAdaptee _browserActivityEnumAdaptee;

        public BrowserActivityAdapter(BrowserActivityAdaptee browserActivityAdaptee,
            BrowserActivityEnumAdaptee browserActivityEnumAdaptee)
        {
            _browserActivityAdaptee = browserActivityAdaptee;
            _browserActivityEnumAdaptee = browserActivityEnumAdaptee;
        }

        public string EnlistActiveTabUrl(BrowserType browserType)
        {
            var tabUrl = _browserActivityAdaptee.GetActiveTabUrl(browserType);

            if (string.IsNullOrWhiteSpace(tabUrl))
                throw new Exception($"No URL found in {_browserActivityEnumAdaptee.ToDescriptionString(browserType)} browser");

            return tabUrl;
        }

        public IList<string> EnlistAllOpenTabs(BrowserType browserType)
        {
            var tabs = _browserActivityAdaptee.GetOpenTabsInfos(browserType);
            if (tabs == null || tabs.Count == 0)
                throw new Exception($"No tabs found in {_browserActivityEnumAdaptee.ToDescriptionString(browserType)} browser");

            return tabs;
        }
    }
}
