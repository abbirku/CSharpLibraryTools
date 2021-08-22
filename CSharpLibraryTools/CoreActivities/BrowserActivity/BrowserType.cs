using System.ComponentModel;

namespace CoreActivities.BrowserActivity
{
    public enum BrowserType
    {
        [Description("chrome")]
        Chrome=1,
        [Description("firefox")]
        FireFox =2,
        [Description("edge")]
        Edge =3,
        [Description("opera")]
        Opera =4,
        [Description("safari")]
        Safari = 5
    }
}
