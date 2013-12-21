using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Swk5.GeoCaching.BusinessLogic.CacheManager;

namespace Swk5.GeoCaching.Desktop.ViewModel.Cache {
    public class CacheCollectionVM : AbstractViewModelBase<CacheCollectionVM> {
        private readonly ICacheManager cacheManager;

        private ICommand createCommand;

        private CacheVM currentCache;
        private ICommand deleteCommand;

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

        public void SetCurrentCacheById(int id) {
            CurrentCache = Caches.First(c => c.Id == id);
        }

        public ICommand CreateCacheCommand {
            get { return createCommand ?? (createCommand = new RelayCommand(param => NewCache())); }
        }

        public ICommand DeleteCurrentCacheCommand {
            get { return deleteCommand ?? ( deleteCommand = new RelayCommand(param => DeleteCache()) ); }
        }

        private void NewCache() {
            throw new NotImplementedException();
        }

        private void DeleteCache ( ) {
            try {
                cacheManager.DeleteCache(CurrentCache.Id);
                Caches.Remove(CurrentCache);
                CurrentCache = null;
            }
            catch ( Exception e ) {
                MessageBox.Show(e.Message, "Cache manager error", MessageBoxButton.OK, MessageBoxImage.Error);
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