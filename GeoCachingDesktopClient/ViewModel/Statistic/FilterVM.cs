using System;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.Desktop.ViewModel.Statistic {
    public class FilterVM : AbstractViewModelBase<FilterVM> {
        private const double TOLERANCE = 0.000001;
        private readonly CacheFilter filter;

        public FilterVM(CacheFilter filter) {
            
            // kind of copy construction
            this.filter = new CacheFilter() {
                FromPosition = filter.FromPosition,
                ToPosition = filter.ToPosition,
                FromCreationDate =  filter.FromCreationDate,
                ToCreationDate =  filter.ToCreationDate
            };
        }

        public CacheFilter Current {
            get { return filter; }
        }

        public double FromLatitude {
            get { return filter.FromPosition.Latitude; }
            set {
                if ( Math.Abs(filter.FromPosition.Latitude - value) > TOLERANCE ) {
                    filter.FromPosition = new GeoPosition(value, filter.FromPosition.Longitude);
                    RaisePropertyChangedEvent(vm => vm.FromLatitude);
                }
            }
        }

        public double FromLongitude {
            get { return filter.FromPosition.Longitude; }
            set {
                if ( Math.Abs(filter.FromPosition.Longitude - value) > TOLERANCE ) {
                    filter.FromPosition = new GeoPosition(filter.FromPosition.Latitude, value);
                    RaisePropertyChangedEvent(vm => vm.FromLongitude);
                }
            }
        }
        public double ToLatitude {
            get { return filter.ToPosition.Latitude; }
            set {
                if ( Math.Abs(filter.ToPosition.Latitude - value) > TOLERANCE ) {
                    filter.ToPosition = new GeoPosition(value, filter.ToPosition.Longitude);
                    RaisePropertyChangedEvent(vm => vm.ToLatitude);
                }
            }
        }

        public double ToLongitude {
            get { return filter.ToPosition.Longitude; }
            set {
                if ( Math.Abs(filter.ToPosition.Longitude - value) > TOLERANCE ) {
                    filter.ToPosition = new GeoPosition(filter.ToPosition.Latitude, value);
                    RaisePropertyChangedEvent(vm => vm.ToLongitude);
                }
            }
        }

        public DateTime FromDate {
            get { return filter.FromCreationDate; }
            set {
                if ( filter.FromCreationDate != value ) {
                    filter.FromCreationDate = value;
                    RaisePropertyChangedEvent(vm => vm.FromDate);
                }
            }
        }

        public DateTime ToDate {
            get { return filter.ToCreationDate; }
            set {
                if ( filter.ToCreationDate != value ) {
                    filter.ToCreationDate = value;
                    RaisePropertyChangedEvent(vm => vm.ToDate);
                }
            }
        }

    }
}