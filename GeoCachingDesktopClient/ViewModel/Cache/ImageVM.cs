using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.Desktop.ViewModel.Cache {
    public class ImageVM {
        private readonly ICacheManager cacheManager;
        private readonly Image image;

        public ImageVM(ICacheManager cacheManager, Image image) {
            this.cacheManager = cacheManager;
            this.image = image;
        }

        public byte[] Image {
            get { return image.ImageData; }
        }
    }
}