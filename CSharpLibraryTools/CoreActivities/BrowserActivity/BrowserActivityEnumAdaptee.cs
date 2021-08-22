using System.ComponentModel;

namespace CoreActivities.BrowserActivity
{
    public class BrowserActivityEnumAdaptee
    {
        public string ToDescriptionString(BrowserType val)
        {
            var attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
