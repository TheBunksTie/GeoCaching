package at.wea5.geocaching.domainmodel;

public class GeoPosition {
    //------------------------------------ constructor ------------------------------------

    public GeoPosition(double latitude, double longitude) {
        this.latitude = latitude;
        this.longitude = longitude;
    }
  
    //-------------------------------------- public ---------------------------------------
        
    public double getLatitude() {
        return latitude;
    }
    public void setLatitude(double latitude) {
        this.latitude = latitude;
    }
    public double getLongitude() {
        return longitude;
    }
    public void setLongitude(double longitude) {
        this.longitude = longitude;
    }
    
    //------------------------------------- private ---------------------------------------
    
    //-------------------------------------- members --------------------------------------
    private double latitude;
    private double longitude;
}
