using System.Drawing;

namespace CoreActivities.ScreenCapture
{
    public interface IScreenCapture
    {
        Bitmap CaptureUserScreen(int width, int height);
    }

    public class ScreenCaptureAdapter : IScreenCapture
    {
        public Bitmap CaptureUserScreen(int width, int height)
        {
            if (width == 0 || height == 0)
                return null;

            using var bitmap = new Bitmap(width, height);
            using var g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(0, 0, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);

            var cloneImage = (Bitmap)bitmap.Clone();

            return cloneImage;
        }
    }
}
