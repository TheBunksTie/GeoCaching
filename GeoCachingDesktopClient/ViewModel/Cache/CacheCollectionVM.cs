using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.Desktop.ViewModel.Filter;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.Desktop.ViewModel.Cache {
    public class CacheCollectionVM : AbstractViewModelBase<CacheCollectionVM> {
        private readonly ICacheManager cacheManager;

        private readonly DataFilter defaultFilter;
        private readonly FilterVM filterVM;
        private ICommand actualizeCommand;        
        private ICommand deleteCommand;
        private CacheVM currentCache;
      
        private bool difficultyFilterRequested;
        private bool sizeFilterRequested;
        private bool terrainFilterRequested;

        public CacheCollectionVM(ICacheManager cacheManager) {
            this.cacheManager = cacheManager;

            // load list of cache sizes from db
            SizeList = cacheManager.GetCacheSizeList();
                       
            // load default filter
            defaultFilter = cacheManager.GetDefaultFilter();

            // initialise active filter with default values and pass just loaded list of cache size to filter
            filterVM = new FilterVM(defaultFilter) {
                CacheSizeList = SizeList
            };

            Caches = new ObservableCollection<CacheVM>();
            LoadCaches(Filter.Current);
        }

        // contains the list of all caches from db
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

        public string AuthenticatedUserPositionString {
            get { return cacheManager.GetAuthenticatedUser().Position.ToString(); }
        }


        public FilterVM Filter {
            get { return filterVM; } 
        }

        public bool DifficultyFilterRequested {
            get { return difficultyFilterRequested; }
            set {
                if (difficultyFilterRequested != value) {
                    difficultyFilterRequested = value;
                    RaisePropertyChangedEvent(vm => vm.DifficultyFilterRequested);
                }
            }
        }

        public bool TerrainFilterRequested {
            get { return terrainFilterRequested; }
            set {
                if (terrainFilterRequested != value) {
                    terrainFilterRequested = value;
                    RaisePropertyChangedEvent(vm => vm.TerrainFilterRequested);
                }
            }
        }

        public bool SizeFilterRequested {
            get { return sizeFilterRequested; }
            set {
                if (sizeFilterRequested != value) {
                    sizeFilterRequested = value;
                    RaisePropertyChangedEvent(vm => vm.SizeFilterRequested);
                }
            }
        }

        public ICommand DeleteCurrentCacheCommand {
            get { return deleteCommand ?? (deleteCommand = new RelayCommand(param => DeleteCache())); }
        }

        public ICommand ActualizeCommand {
            get { return actualizeCommand ?? (actualizeCommand = new RelayCommand(param => ActualizeVisibleCaches())); }
        }

        public void SetCurrentCacheById(int id) {
            CurrentCache = Caches.First(c => c.Id == id);
        }

        private void DeleteCache() {
            try {
                cacheManager.DeleteCache(CurrentCache.Id);
                Caches.Remove(CurrentCache);
                CurrentCache = null;
            }
            catch (Exception e) {
                MessageBox.Show(e.Message, "Cache manager error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadCaches(DataFilter filter) {
            Caches.Clear();

            IEnumerator<DomainModel.Cache> e = cacheManager.GetFilteredCacheList(filter).GetEnumerator();
            while (await Task.Factory.StartNew(() => e.MoveNext())) {
                Caches.Add(new CacheVM(cacheManager, e.Current));
            }
        }

        private void ActualizeVisibleCaches() {
            // prepare filter object depending on user-selected options
            PrepareFilter();
            LoadCaches(Filter.Current);
        }

        private void PrepareFilter() {
            if (!TerrainFilterRequested) {
                Filter.FromTerrainDifficulty = defaultFilter.FromTerrainDifficulty;
                Filter.ToTerrainDifficulty = defaultFilter.ToTerrainDifficulty;
            }

            if (!DifficultyFilterRequested) {
                Filter.FromCacheDifficulty = defaultFilter.FromCacheDifficulty;
                Filter.ToCacheDifficulty = defaultFilter.ToCacheDifficulty;
            }

            if (!SizeFilterRequested) {
                Filter.FromCacheSize = defaultFilter.FromCacheSize;
                Filter.ToCacheSize = defaultFilter.ToCacheSize;
            }
        }
    }
}