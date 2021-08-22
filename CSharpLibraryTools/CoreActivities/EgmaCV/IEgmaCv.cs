using System.Threading.Tasks;

namespace CoreActivities.EgmaCV
{
    public interface IEgmaCv
    {
        Task CaptureImageAsync(int camIndex, string filePath);
    }
}
