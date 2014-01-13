package at.wea5.geocaching.domainmodel;

import java.util.Date;

public class Rating {

//------------------------------------ constructor ------------------------------------
    
    public Rating(int grade, int id, int cacheId, int creatorId, Date creationDate) {
        this.grade = grade;
        this.id = id;
        this.cacheId = cacheId;
        this.creatorId = creatorId;
        this.creationDate = creationDate;
    }
       
//-------------------------------------- public ---------------------------------------

    public int getGrade() {
        return grade;
    }
    
    public void setGrade(int grade) {
        this.grade = grade;
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
    
//------------------------------------- private ---------------------------------------

//-------------------------------------- members --------------------------------------
 
    private int grade;
    private int id; 
    private int cacheId;
    private int creatorId;
    private Date creationDate; 
}
