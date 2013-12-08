using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading.Tasks;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.DomainModel;
using Image = Swk5.GeoCaching.DomainModel.Image;

namespace Swk5.GeoCaching.Desktop.ViewModel.Cache {
    public class ImageCollectionVM : AbstractViewModelBase<ImageCollectionVM> {
        private readonly DomainModel.Cache cache;
        private readonly ICacheManager cacheManager;
        private ImageVM currentImage;

        private RelayCommand newImage;
        private RelayCommand deleteImage;

        public ImageCollectionVM(ICacheManager cacheManager, DomainModel.Cache cache) {
            this.cacheManager = cacheManager;
            this.cache = cache;

            Images = new ObservableCollection<ImageVM>();
            LoadImages();

            //CurrentImage = Images.First();
        }

        public ObservableCollection<ImageVM> Images { get; private set; }

        public ImageVM CurrentImage {
            get { return currentImage; }
            set {
                if (currentImage != value) {
                    currentImage = value;
                    RaisePropertyChangedEvent(vm => vm.CurrentImage);
                }
            }
        }

        public RelayCommand UploadImageCommand {
            get {
                if (newImage == null) {
                    newImage = new RelayCommand(param => UploadImage());
                }
                return newImage;
            }
        }

        public RelayCommand DeleteImageCommand {
            get {
                if (deleteImage == null) {
                    deleteImage = new RelayCommand(param => DeleteImage());
                }
                return deleteImage;
            }
        }

        private void UploadImage ( ) {
            // TODO upload images
        }

        private void DeleteImage() {
            // TODO delete Image
        }

        private async void LoadImages() {
            IEnumerator<Image> e = cacheManager.GetImagesForCache(cache.Id).GetEnumerator();

            while (await Task.Factory.StartNew(() => e.MoveNext())) {
                Images.Add(new ImageVM(cacheManager, e.Current));
            }
        }
    }
}