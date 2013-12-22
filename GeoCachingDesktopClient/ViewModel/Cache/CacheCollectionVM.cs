using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.Desktop.ViewModel.Cache.Filter;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.Desktop.ViewModel.Cache {
    public class CacheCollectionVM : AbstractViewModelBase<CacheCollectionVM> {
        private readonly ICacheManager cacheManager;

        private CacheVM currentCache;
        private ICommand deleteCommand;
        private ICommand actualizeCommand;

        private FilterCriteriumVM criterium;
        private FilterOperationVM operation;
        private string filterValue;

        public CacheCollectionVM(ICacheManager cacheManager) {
            this.cacheManager = cacheManager;

            Caches = new ObservableCollection<CacheVM>();
            LoadCaches();

            SizeList = new List<string>();
            LoadCacheSizeList();

            LoadFilterCriteria();
            LoadFilterOperations();            
            LoadFilterValues();
        }

        // contains the list of all caches from db
        public ObservableCollection<CacheVM> Caches { get; private set; }

        public ObservableCollection<FilterCriteriumVM> FilterCriteria { get; private set; }
        public ObservableCollection<FilterOperationVM> FilterOperations { get; private set; }
        public ObservableCollection<string> FilterValues { get; private set; }

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

        public FilterCriteriumVM CurrentFilterCriterium {
            get { return criterium; }
            set {
                if (criterium != value) {
                    criterium = value;
                    RaisePropertyChangedEvent(vm => vm.CurrentFilterCriterium);
                }
            }
        }
       
        public FilterOperationVM CurrentFilterOperation {
            get { return operation; }
            set {
                if (operation != value) {
                    operation= value;
                    RaisePropertyChangedEvent(vm => vm.CurrentFilterOperation);
                }
            }
        }

        public string CurrentFilterValue {
            get { return filterValue; }
            set {
                if (filterValue != value) {
                    filterValue = value;
                    RaisePropertyChangedEvent(vm => vm.CurrentFilterValue);
                }
            }
        }

        public ICommand DeleteCurrentCacheCommand {
            get { return deleteCommand ?? (deleteCommand = new RelayCommand(param => DeleteCache())); }
        }

        public ICommand ActualizeCommand {
            get { return actualizeCommand ?? ( actualizeCommand = new RelayCommand(param => ActualizeVisibleCaches()) ); }
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

        private void LoadFilterValues() {
            FilterValues = new ObservableCollection<string>();
            int maxI = 5;

            if (CurrentFilterCriterium.Criterium == FilterCriterium.Size) {
                maxI = 6;
                
            }
            for ( int i = 1; i <= maxI; i++ ) {
                FilterValues.Add(String.Format("{0}",i));
            }
        }

        private void LoadFilterCriteria() {
            FilterCriteria = new ObservableCollection<FilterCriteriumVM> {
                new FilterCriteriumVM(FilterCriterium.Size),
                new FilterCriteriumVM(FilterCriterium.CacheDifficulty),
                new FilterCriteriumVM(FilterCriterium.TerrainDifficulty)
            };

            CurrentFilterCriterium = FilterCriteria.First();
        }

        private void LoadFilterOperations() {
            FilterOperations = new ObservableCollection<FilterOperationVM> {
                new FilterOperationVM(FilterOperation.AboveEquals),
                new FilterOperationVM(FilterOperation.Above),                                
                new FilterOperationVM(FilterOperation.Exact),
                new FilterOperationVM(FilterOperation.Below),
                new FilterOperationVM(FilterOperation.BelowEquals)
            };

            CurrentFilterOperation = FilterOperations.First();            
        }
        
        private async void ActualizeVisibleCaches ( ) {
            try {
                Caches.Clear();

                IEnumerator<DomainModel.Cache> e = cacheManager.GetFilteredCacheList(CurrentFilterCriterium.Criterium, CurrentFilterOperation.Operation, CurrentFilterValue).GetEnumerator();
                while ( await Task.Factory.StartNew(( ) => e.MoveNext()) ) {
                    Caches.Add(new CacheVM(cacheManager, e.Current));
                }            
            }
            catch (Exception e) {
                // in case of error load whole list again
                LoadCaches();
                MessageBox.Show(e.Message, "Cache manager error", MessageBoxButton.OK, MessageBoxImage.Error);
            }           
        }
    }
}