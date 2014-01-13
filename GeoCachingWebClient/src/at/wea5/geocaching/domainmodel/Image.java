package at.wea5.geocaching.domainmodel;

public class Image {

//------------------------------------ constructor ------------------------------------
    
    public Image(int id, int cacheId, String fileName, byte[] imageData) {
        this.imageData = imageData;
        this.id = id;
        this.cacheId = cacheId;
        this.fileName = fileName;
    }
        
//-------------------------------------- public ---------------------------------------

    public byte[] getImageData() {
        return imageData;
    }
    
    public void setImageData(byte[] imageData) {
        this.imageData = imageData;
    }
    
    public int getId() {
        return id;
    }
    
    public void setId(int id) {
        this.id = id;
    }
    
    public int getCacheId() {
        return cacheId;
    }
    
    public void setCacheId(int cacheId) {
        this.cacheId = cacheId;
    }
    
    public String getFileName() {
        return fileName;
    }
    
    public void setFileName(String fileName) {
        this.fileName = fileName;
    }
    
//------------------------------------- private ---------------------------------------

//-------------------------------------- members --------------------------------------
    
    private byte[] imageData;
    private int id; 
    private int cacheId; 
    private String fileName;
}
