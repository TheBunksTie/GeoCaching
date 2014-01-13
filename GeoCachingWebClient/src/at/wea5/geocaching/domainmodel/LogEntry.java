package at.wea5.geocaching.domainmodel;

import java.util.Date;

public class LogEntry {

//------------------------------------ constructor ------------------------------------
    public LogEntry(int id, int cacheId, int creatorId, Date creationDate, boolean isFound, String comment) {
        this.id = id;
        this.cacheId = cacheId;
        this.creatorId = creatorId;
        this.creationDate = creationDate;
        this.isFound = isFound;
        this.comment = comment;
    }
    
//-------------------------------------- public ---------------------------------------

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
    
    public int getCreatorId() {
        return creatorId;
    }
    
    public void setCreatorId(int creatorId) {
        this.creatorId = creatorId;
    }
    
    public Date getCreationDate() {
        return creationDate;
    }
    
    public void setCreationDate(Date creationDate) {
        this.creationDate = creationDate;
    }
    
    public boolean isFound() {
        return isFound;
    }
    
    public void setFound(boolean isFound) {
        this.isFound = isFound;
    }
    
    public String getComment() {
        return comment;
    }
    
    public void setComment(String comment) {
        this.comment = comment;
    }
    
//------------------------------------- private ---------------------------------------

    
//-------------------------------------- members --------------------------------------
    
    private int id;
    private int cacheId;
    private int creatorId;
    private Date creationDate;
    private boolean isFound;
    private String comment;
}
