package at.wea5.geocaching.business.webserviceImplementation;


import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;

import at.wea5.geocaching.business.ManagerBase;

@ManagedBean(name="CacheManager")
@SessionScoped
public class CacheManager extends ManagerBase {

//------------------------------------ constructor ------------------------------------

//-------------------------------------- public ---------------------------------------
    
    public String findCachesByName() {
        
        return "FindCachesEvent";
    }
    
    public String findCachesByCacheDifficulty() {
        
        return "FindCachesEvent";
    }
    
    public String findCachesByTerrainDifficulty() {
        
        return "FindCachesEvent";
    }
    
    public String findCachesByRegion() {
        
        return "FindCachesEvent";
    }
    
    public String findAllCaches() {        
        
        return "FindCachesEvent";
    }
    
//------------------------------------- private ---------------------------------------

//-------------------------------------- members --------------------------------------
    
}
