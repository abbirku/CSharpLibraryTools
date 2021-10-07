using CoreActivities.BrowserActivity;
using System;
using System.Threading.Tasks;

namespace CSharpLibraryTools
{
    public class BrowseActivityImp
    {
        private readonly IBrowserActivity _browserActivity;
        private readonly BrowserActivityEnumAdaptee _browserActivityEnumAdaptee;

        public BrowseActivityImp(IBrowserActivity browserActivity,
            BrowserActivityEnumAdaptee browserActivityEnumAdaptee)
        {
            _browserActivity = browserActivity;
            _browserActivityEnumAdaptee = browserActivityEnumAdaptee;
        }

        public async Task Run()
        {
            await Task.Run(() =>
            {
                //Check browser open or not
                Console.WriteLine($"The browser {_browserActivityEnumAdaptee.ToDescriptionString(BrowserType.Chrome)} " +
                    $"{(_browserActivity.IsBrowserOpen(BrowserType.Chrome)?"is open": "is not open")}");

                Console.WriteLine($"The browser {_browserActivityEnumAdaptee.ToDescriptionString(BrowserType.Edge)} " +
                    $"{(_browserActivity.IsBrowserOpen(BrowserType.Edge) ? "is open" : "is not open")}");

                Console.WriteLine($"The browser {_browserActivityEnumAdaptee.ToDescriptionString(BrowserType.FireFox)} " +
                    $"{(_browserActivity.IsBrowserOpen(BrowserType.FireFox) ? "is open" : "is not open")}");

                Console.WriteLine($"The browser {_browserActivityEnumAdaptee.ToDescriptionString(BrowserType.Opera)} " +
                    $"{(_browserActivity.IsBrowserOpen(BrowserType.Opera) ? "is open" : "is not open")}");

                Console.WriteLine($"The browser {_browserActivityEnumAdaptee.ToDescriptionString(BrowserType.Safari)} " +
                    $"{(_browserActivity.IsBrowserOpen(BrowserType.Safari) ? "is open" : "is not open")}");

                //Check open browser active url and tabs
                if (_browserActivity.IsBrowserOpen(BrowserType.Chrome))
                {
                    var activeUrl = _browserActivity.EnlistActiveTabUrl(BrowserType.Chrome);
                    Console.WriteLine($"Active URL: {activeUrl}\n");

                    var activeTitle = _browserActivity.EnlistActiveTabUrl(BrowserType.Chrome);
                    Console.WriteLine($"Active Tab Title: {activeTitle}\n");

                    var tabs = _browserActivity.EnlistAllOpenTabs(BrowserType.Chrome);
                    foreach (var item in tabs)
                        Console.WriteLine($"Tab: {item}");
                }

                if (_browserActivity.IsBrowserOpen(BrowserType.Edge))
                {
                    var activeUrl = _browserActivity.EnlistActiveTabUrl(BrowserType.Edge);
                    Console.WriteLine($"Active URL: {activeUrl}\n");

                    var activeTitle = _browserActivity.EnlistActiveTabUrl(BrowserType.Chrome);
                    Console.WriteLine($"Active Tab Title: {activeTitle}\n");

                    var tabs = _browserActivity.EnlistAllOpenTabs(BrowserType.Edge);
                    foreach (var item in tabs)
                        Console.WriteLine($"Tab: {item}");
                }
            });
        }
    }
}
