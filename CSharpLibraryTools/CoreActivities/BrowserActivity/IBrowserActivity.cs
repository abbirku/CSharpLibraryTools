using System.Collections.Generic;

namespace CoreActivities.BrowserActivity
{
    public interface IBrowserActivity
    {
        IList<string> EnlistAllOpenTabs(BrowserType browserType);
        string EnlistActiveTabUrl(BrowserType browserType);
    }
}
