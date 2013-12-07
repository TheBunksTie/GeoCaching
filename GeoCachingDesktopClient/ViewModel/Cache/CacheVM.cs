using System;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.Desktop.ViewModel.Cache {
    public class CacheVM : AbstractViewModelBase<CacheVM> {
        private readonly DomainModel.Cache cache;
        private readonly ICacheManager cacheManager;
        private const double TOLERANCE = 0.000001;

        public CacheVM(ICacheManager cacheManager, DomainModel.Cache cache) {
            this.cacheManager = cacheManager;
            this.cache = cache;
        }

        public int Id {
            get { return cache.Id; }
        }

        public string Name {
            get { return cache.Name; }
            set {
                if ( cache.Name != value ) {
                    cache.Name = value;
                    RaisePropertyChangedEvent(vm => vm.Name);
                }
            }
        }

        public DateTime CreationDate {
            get { return cache.CreationDate; }
        }

        public double CacheDifficulty {
            get { return cache.CacheDifficulty; }
            set {
                if ( Math.Abs(cache.CacheDifficulty - value) > TOLERANCE ) {
                    cache.CacheDifficulty = value;
                    RaisePropertyChangedEvent(vm => vm.CacheDifficulty);
                }
            }
        }

        public double TerrainDifficulty {
            get { return cache.TerrainDifficulty; }
            set {
                if ( Math.Abs(cache.TerrainDifficulty - value) > TOLERANCE ) {
                    cache.TerrainDifficulty = value;
                    RaisePropertyChangedEvent(vm => vm.TerrainDifficulty);
                }
            }
        }

        public string Size {
            get { return cache.Size; }
            set {
                if ( cache.Size != value ) {
                    cache.Size = value;
                    RaisePropertyChangedEvent(vm => vm.Size);
                }
            }
        }

        public int OwnerId {
            get { return cache.OwnerId; }
            set {
                if ( cache.OwnerId != value ) {
                    cache.OwnerId = value;
                    RaisePropertyChangedEvent(vm => vm.OwnerId);
                }
            }
        }

        public double Latitude {
            get { return cache.Position.Latitude; }
            set {
                if ( Math.Abs(cache.Position.Latitude - value) > TOLERANCE ) {
                    GeoPosition currentPosition = cache.Position;
                    currentPosition.Latitude = value;
                    cache.Position = currentPosition;
                    RaisePropertyChangedEvent(vm => vm.Latitude);
                }
            }
        }

        public double Longitude {
            get { return cache.Position.Longitude; }
            set {
                if ( Math.Abs(cache.Position.Longitude - value) > TOLERANCE ) {
                    GeoPosition currentPosition = cache.Position;
                    currentPosition.Longitude = value;
                    cache.Position = currentPosition;
                    RaisePropertyChangedEvent(vm => vm.Longitude);
                }
            }
        }

        public string Description {
            get { return cache.Description; }
            set {
                if ( cache.Description != value ) {
                    cache.Description = value;
                    RaisePropertyChangedEvent(vm => vm.Description);
                }
            }
        }

    }
}