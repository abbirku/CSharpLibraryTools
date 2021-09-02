using CoreActivities.BrowserActivity;
using System;
using System.Threading.Tasks;

namespace CSharpLibraryTools
{
    public class BrowseActivityImp
    {
        private readonly IBrowserActivity _browserActivity;

        public BrowseActivityImp(IBrowserActivity browserActivity)
        {
            _browserActivity = browserActivity;
        }

        public async Task Run()
        {
            await Task.Run(() =>
            {
                var activeUrl = _browserActivity.EnlistActiveTabUrl(BrowserType.Chrome);
                Console.WriteLine($"Active URL: {activeUrl}\n");

                var tabs = _browserActivity.EnlistAllOpenTabs(BrowserType.Chrome);
                foreach (var item in tabs)
                    Console.WriteLine($"Tab: {item}");
            });
        }
    }
}
