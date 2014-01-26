using System;
using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.Desktop.ViewModel.Filter {
    public class FilterVM : AbstractViewModelBase<FilterVM> {
        private const double TOLERANCE = 0.000001;
        private readonly DataFilter filter;

        public FilterVM(DataFilter filter) {
            // call (deep) copy constructor
            this.filter = new DataFilter(filter);

        }

        // to convert cache sizes from description to id and vice versa
        public List<string> CacheSizeList { get; set; }

        public DataFilter Current {
            get { return filter; }
        }

        public double FromLatitude {
            get { return filter.FromPosition.Latitude; }
            set {
                if (Math.Abs(filter.FromPosition.Latitude - value) > TOLERANCE) {
                    filter.FromPosition = new GeoPosition(value, filter.FromPosition.Longitude);
                    RaisePropertyChangedEvent(vm => vm.FromLatitude);
                }
            }
        }

        public double FromLongitude {
            get { return filter.FromPosition.Longitude; }
            set {
                if (Math.Abs(filter.FromPosition.Longitude - value) > TOLERANCE) {
                    filter.FromPosition = new GeoPosition(filter.FromPosition.Latitude, value);
                    RaisePropertyChangedEvent(vm => vm.FromLongitude);
                }
            }
        }

        public double ToLatitude {
            get { return filter.ToPosition.Latitude; }
            set {
                if (Math.Abs(filter.ToPosition.Latitude - value) > TOLERANCE) {
                    filter.ToPosition = new GeoPosition(value, filter.ToPosition.Longitude);
                    RaisePropertyChangedEvent(vm => vm.ToLatitude);
                }
            }
        }

        public double ToLongitude {
            get { return filter.ToPosition.Longitude; }
            set {
                if (Math.Abs(filter.ToPosition.Longitude - value) > TOLERANCE) {
                    filter.ToPosition = new GeoPosition(filter.ToPosition.Latitude, value);
                    RaisePropertyChangedEvent(vm => vm.ToLongitude);
                }
            }
        }

        public DateTime FromDate {
            get { return filter.FromDate; }
            set {
                if (filter.FromDate != value) {
                    filter.FromDate = value;
                    RaisePropertyChangedEvent(vm => vm.FromDate);
                }
            }
        }

        public DateTime ToDate {
            get { return filter.ToDate; }
            set {
                if (filter.ToDate != value) {
                    filter.ToDate = value;
                    RaisePropertyChangedEvent(vm => vm.ToDate);
                }
            }
        }

        public double FromCacheDifficulty {
            get { return filter.FromCacheDifficulty; }
            set {
                if (Math.Abs(filter.FromCacheDifficulty - value) > TOLERANCE) {
                    filter.FromCacheDifficulty = value;
                    RaisePropertyChangedEvent(vm => vm.FromCacheDifficulty);
                }
            }
        }

        public double ToCacheDifficulty {
            get { return filter.ToCacheDifficulty; }
            set {
                if ( Math.Abs(filter.ToCacheDifficulty - value) > TOLERANCE ) {
                    filter.ToCacheDifficulty = value;
                    RaisePropertyChangedEvent(vm => vm.ToCacheDifficulty);
                }
            }
        }

        public double FromTerrainDifficulty {
            get { return filter.FromTerrainDifficulty; }
            set {
                if ( Math.Abs(filter.FromTerrainDifficulty - value) > TOLERANCE ) {
                    filter.FromTerrainDifficulty = value;
                    RaisePropertyChangedEvent(vm => vm.FromTerrainDifficulty);
                }
            }
        }

        public double ToTerrainDifficulty {
            get { return filter.ToTerrainDifficulty; }
            set {
                if ( Math.Abs(filter.ToTerrainDifficulty - value) > TOLERANCE ) {
                    filter.ToTerrainDifficulty = value;
                    RaisePropertyChangedEvent(vm => vm.ToTerrainDifficulty);
                }
            }
        }

        public int FromCacheSize {
            get { return filter.FromCacheSize; }
            set {
                if ( filter.FromCacheSize != value ) {
                    filter.FromCacheSize = value;
                    RaisePropertyChangedEvent(vm => vm.FromCacheSize);
                }
            }
        }

        public int ToCacheSize {
            get { return filter.ToCacheSize; }
            set {
                if ( filter.ToCacheSize != value ) {
                    filter.ToCacheSize = value;
                    RaisePropertyChangedEvent(vm => vm.ToCacheSize);
                }
            }
        }

        public string FromCacheSizeString {
            get { return CacheSizeList[filter.FromCacheSize - 1]; }
            set {

                int sizeCode = CacheSizeList.IndexOf(value) + 1;
                
                if ( filter.FromCacheSize != sizeCode) {
                    filter.FromCacheSize = sizeCode;
                    RaisePropertyChangedEvent(vm => vm.FromCacheSizeString);
                }
            }
        }

        public string ToCacheSizeString {
            get { return CacheSizeList[filter.ToCacheSize - 1]; }
            set {

                int sizeCode = CacheSizeList.IndexOf(value) + 1;

                if ( filter.ToCacheSize != sizeCode ) {
                    filter.ToCacheSize = sizeCode;
                    RaisePropertyChangedEvent(vm => vm.ToCacheSizeString);
                }
            }
        }
    }
}