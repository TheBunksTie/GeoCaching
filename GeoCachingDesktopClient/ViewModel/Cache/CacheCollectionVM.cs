using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Swk5.GeoCaching.BusinessLogic.CacheManager;

namespace Swk5.GeoCaching.Desktop.ViewModel.Cache {
    public class CacheCollectionVM : AbstractViewModelBase<CacheCollectionVM> {
        private readonly ICacheManager cacheManager;

        private RelayCommand createCommand;

        private CacheVM currentCache;
        private RelayCommand deleteCommand;

        public CacheCollectionVM(ICacheManager cacheManager) {
            this.cacheManager = cacheManager;

            Caches = new ObservableCollection<CacheVM>();
            LoadCaches();

            SizeList = new List<string>();
            LoadCacheSizeList();
        }

        public ObservableCollection<CacheVM> Caches { get; private set; }
        public List<string> SizeList { get; private set; }

        public CacheVM CurrentCache {
            get { return currentCache; }
            set {
                if (currentCache != value) {
                    currentCache = value;
                    RaisePropertyChangedEvent(vm => vm.CurrentCache);
                }
            }
        }

        private async void LoadCacheSizeList() {
            SizeList.Clear();

            IEnumerator<string> e = cacheManager.GetCacheSizeList().GetEnumerator();
            while (await Task.Factory.StartNew(() => e.MoveNext())) {
                SizeList.Add(e.Current);
            }
        }

        private async void LoadCaches() {
            Caches.Clear();

            IEnumerator<DomainModel.Cache> e = cacheManager.GetCacheList().GetEnumerator();
            while (await Task.Factory.StartNew(() => e.MoveNext())) {
                Caches.Add(new CacheVM(cacheManager, e.Current));
            }
        }
    }
}