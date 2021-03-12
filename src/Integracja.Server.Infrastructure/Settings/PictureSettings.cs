using Integracja.Server.Infrastructure.Enums;

namespace Integracja.Server.Infrastructure.Settings
{
    public class PictureSettings
    {
        public int MaxSize { get; set; }
        public int PictureWidth { get; set; }
        public int PictureHeight { get; set; }
        public int ThumbnailWidth { get; set; }
        public int ThumbnailHeight { get; set; }
        public ImageFormat Format { get; set; }
    }
}
