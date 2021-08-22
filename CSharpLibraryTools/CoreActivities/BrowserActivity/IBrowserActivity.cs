using System;
using System.Collections.Generic;
using System.Text;

namespace CoreActivities.BrowserActivity
{
    public interface IBrowserActivity
    {
        IList<string> EnlistAllOpenTabs(BrowserType browserType);
        string EnlistActiveTabUrl(BrowserType browserType);
    }
}
