using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Maps.MapControl.WPF;

namespace Swk5.GeoCaching.Desktop.ViewModel.Cache {
    public class PushpinVM : AbstractViewModelBase<PushpinVM> {

        private readonly CacheVM cache;
        private readonly Pushpin pin;

        public PushpinVM(CacheVM cache) {
            this.cache = cache;
            pin = new Pushpin();
            pin.Location = new Location(cache.Latitude, cache.Longitude);
        }

        public CacheVM AssignedCacheVm {
            get {
                return cache;
            }
        }

        public Pushpin Pin {
            get {
                pin.Location.Latitude = cache.Latitude;
                pin.Location.Longitude = cache.Longitude;
                return pin;
            }
        }
    }
}
