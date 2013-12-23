using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Swk5.GeoCaching.BusinessLogic.CacheManager;

namespace Swk5.GeoCaching.Desktop.ViewModel.Cache.Image {
    public class ImageVM {
        private readonly ICacheManager cacheManager;
        private readonly DomainModel.Image image;
        private readonly BitmapImage source = new BitmapImage();

        public ImageVM(ICacheManager cacheManager, DomainModel.Image image) {
            this.cacheManager = cacheManager;
            this.image = image;

            LoadImage();
        }

        public DomainModel.Image Image {
            get { return image; }
        }

        public ImageSource WpfImageSource {
            get { return source; }
        }

        private void LoadImage() {
            using (var mem = new MemoryStream(image.ImageData)) {
                mem.Position = 0;
                source.BeginInit();
                source.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                source.CacheOption = BitmapCacheOption.OnLoad;
                source.StreamSource = mem;
                source.EndInit();
            }
            source.Freeze();            
        }
    }
}