using CoreActivities.BrowserActivity;
using System;

namespace CSharpLibraryTools
{
    public class BrowseActivityImp
    {
        private readonly IBrowserActivity _browserActivity;

        public BrowseActivityImp(IBrowserActivity browserActivity)
        {
            _browserActivity = browserActivity;
        }

        public void Run()
        {
            var activeUrl = _browserActivity.EnlistActiveTabUrl(BrowserType.Chrome);
            Console.WriteLine($"Active URL: {activeUrl}");

            var tabs = _browserActivity.EnlistAllOpenTabs(BrowserType.Chrome);
            foreach (var item in tabs)
                Console.WriteLine(item);
        }
    }
}
