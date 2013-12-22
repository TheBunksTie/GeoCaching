using System;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.Desktop.ViewModel.Statistic {
    public class FilterVM : AbstractViewModelBase<FilterVM> {
        private const double TOLERANCE = 0.000001;
        private readonly Filter filter;

        public FilterVM(Filter filter) {
            
            // kind of copy construction
            this.filter = new Filter() {
                FromPosition = filter.FromPosition,
                ToPosition = filter.ToPosition,
                FromDate =  filter.FromDate,
                ToDate =  filter.ToDate
            };
        }

        public Filter Current {
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
            get { return filter.FromDate; }
            set {
                if ( filter.FromDate != value ) {
                    filter.FromDate = value;
                    RaisePropertyChangedEvent(vm => vm.FromDate);
                }
            }
        }

        public DateTime ToDate {
            get { return filter.ToDate; }
            set {
                if ( filter.ToDate != value ) {
                    filter.ToDate = value;
                    RaisePropertyChangedEvent(vm => vm.ToDate);
                }
            }
        }

    }
}