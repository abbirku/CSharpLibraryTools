using System.Collections.Generic;

namespace CoreActivities.BrowserActivity
{
    public class BrowserActivityAdapter : IBrowserActivity
    {
        private readonly BrowserActivityAdaptee _browserActivityAdaptee;

        public BrowserActivityAdapter(BrowserActivityAdaptee browserActivityAdaptee)
            => _browserActivityAdaptee = browserActivityAdaptee;

        public string EnlistActiveTabUrl(BrowserType browserType)
        {
            var tabUrl = _browserActivityAdaptee.GetActiveTabUrl(browserType);
            return tabUrl;
        }

        public IList<string> EnlistAllOpenTabs(BrowserType browserType)
        {
            var tabs = _browserActivityAdaptee.GetOpenTabsInfos(browserType);
            return tabs;
        }
    }
}
