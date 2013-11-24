namespace Swk5.GeoCaching.DomainModel {
    public struct GeoPosition {
        private readonly double latitude;
        private readonly double longitude;

        public GeoPosition(double latitude, double longitude) {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public double Latitude {
            get { return latitude; }
        }

        public double Longitude {
            get { return longitude; }
        }

        public override int GetHashCode() {
            unchecked {
                return (Longitude.GetHashCode()*397) ^ Latitude.GetHashCode();
            }
        }

        public override string ToString() {
            return string.Format("[{0},{1}]", Latitude, Longitude);
        }
    }
}