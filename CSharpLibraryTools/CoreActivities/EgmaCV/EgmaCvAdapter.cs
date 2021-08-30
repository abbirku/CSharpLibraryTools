using Emgu.CV;
using System;
using System.Threading.Tasks;

namespace CoreActivities.EgmaCV
{
    public interface IEgmaCv
    {
        Task CaptureImageAsync(int camIndex, string filePath);
    }

    public class EgmaCvAdapter : IEgmaCv
    {
        public async Task CaptureImageAsync(int camIndex, string filePath)
        {
            await Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(filePath))
                    throw new Exception("Provide a valid path of file");

                using var capture = new VideoCapture(camIndex, VideoCapture.API.DShow);
                var image = capture.QueryFrame(); //take a picture
                image.Save(filePath);
            });
        }
    }
}
