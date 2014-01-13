package at.wea5.geocaching.domainmodel;

import java.util.Date;

public class Cache {

//------------------------------------ constructor ------------------------------------
    
    public Cache(double cacheDifficulty, double terrainDifficulty, int id, String name, Date creationDate, String size, int ownerId,
            GeoPosition position, String description) {
        
        this.cacheDifficulty = cacheDifficulty;
        this.terrainDifficulty = terrainDifficulty;
        this.id = id;
        this.name = name;
        this.creationDate = creationDate;
        Size = size;
        this.ownerId = ownerId;
        this.position = position;
        this.description = description;
    }

//-------------------------------------- public ---------------------------------------
    
    public double getCacheDifficulty() {
        return cacheDifficulty;
    }
    
    public void setCacheDifficulty(double cacheDifficulty) {
        this.cacheDifficulty = cacheDifficulty;
    }
    
    public double getTerrainDifficulty() {
        return terrainDifficulty;
    }
    
    public void setTerrainDifficulty(double terrainDifficulty) {
        this.terrainDifficulty = terrainDifficulty;
    }
    
    public int getId() {
        return id;
    }
    
    public void setId(int id) {
        this.id = id;
    }
    
    public String getName() {
        return name;
    }
    
    public void setName(String name) {
        this.name = name;
    }
    
    public Date getCreationDate() {
        return creationDate;
    }
    
    public void setCreationDate(Date creationDate) {
        this.creationDate = creationDate;
    }
    
    public String getSize() {
        return Size;
    }
    
    public void setSize(String size) {
        Size = size;
    }
    
    public int getOwnerId() {
        return ownerId;
    }
    
    public void setOwnerId(int ownerId) {
        this.ownerId = ownerId;
    }
    
    public GeoPosition getPosition() {
        return position;
    }
    
    public void setPosition(GeoPosition position) {
        this.position = position;
    }
    
    public String getDescription() {
        return description;
    }
    
    public void setDescription(String description) {
        this.description = description;
    }

//------------------------------------- private ---------------------------------------

//-------------------------------------- members --------------------------------------
    private double cacheDifficulty;
    private double terrainDifficulty;

    private int id;
    private String name;
    private Date creationDate;

    private String Size;
    private int ownerId;
    private GeoPosition position;
    private String description;

}
