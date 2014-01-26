package at.wea5.geocaching.business.webserviceImplementation;

public class CacheSizeItem {
    
//------------------------------------ constructor ------------------------------------
    public CacheSizeItem(String size, String id) {
        this.id = id;
        this.size = size;
    }
//-------------------------------------- public ---------------------------------------

    public String getSize() {
        return size;
    }
    
//    public void setSize(String size) {
//        this.size = size;
//    }
    
    public String getId() {
        return id;
    }
    
//    public void setId(int id) {
//        this.id = id;
//    }
    
//------------------------------------- private ---------------------------------------

//-------------------------------------- members --------------------------------------
    private String size;
    private String id;
}
