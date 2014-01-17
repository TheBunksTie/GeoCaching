package at.wea5.geocaching.business.webserviceImplementation;


import java.util.ArrayList;
import java.util.List;
import java.util.logging.Logger;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;

import at.wea5.geocaching.business.ManagerBase;
import at.wea5.geocaching.webserviceproxy.Cache;
import at.wea5.geocaching.webserviceproxy.CacheDetails;
import at.wea5.geocaching.webserviceproxy.CacheFilter;
import at.wea5.geocaching.webserviceproxy.GeoPosition;

@ManagedBean(name="CacheManager", eager=true)
@SessionScoped
public class CacheManager extends ManagerBase {

//------------------------------------ constructor ------------------------------------
    
    public CacheManager() {
        // retrieve default filter and load whole cache list        
        loadDefaultFilter();
        loadCaches();
    }

//-------------------------------------- public ---------------------------------------
          
    public String showDetails() {        
        try {
            // read id of requested cache and retrieve all details via webservice
            currentCache = geoCachingWsProxy.getDetailedCache(Integer.parseInt(getRequestParameterValue("cacheId")));
            
            if (currentCache == null) {
                setErrorMessage("The requested cache could not be found.");
            }
        }
        catch (NumberFormatException ne) {            
            setErrorMessage("The id of the request cache is not a number.");            
        }
        catch (Exception e) {
            log.severe(e.getMessage());
            setErrorMessage("Unable to perform requested action.");
        }            
        return "CacheDetailsEvent";
    }
    
          
    public String getFilteredCacheList() {        
        
        // reset filter to default values
        filter = defaultFilter;
        
        try {
            // process requested filters            
            if (positionFiltered) {
                // get passed paramters from session context
                GeoPosition from = new GeoPosition();                                                
                from.setLatitude(Double.parseDouble(getRequestParameterValue("latitudeFrom")));
                from.setLongitude(Double.parseDouble(getRequestParameterValue("longitudeFrom")));                
                filter.setFromPosition(from);
                
                GeoPosition to = new GeoPosition();                                                
                to.setLatitude(Double.parseDouble(getRequestParameterValue("latitudeTo")));
                to.setLongitude(Double.parseDouble(getRequestParameterValue("longitudeTo")));
                filter.setToPosition(to);
            }
            
            if (cacheDifficultyFiltered) {
                // get passed paramters from session context
                filter.setFromCacheDifficulty(Double.parseDouble(getRequestParameterValue("cacheDifficultyFrom")));
                filter.setToCacheDifficulty(Double.parseDouble(getRequestParameterValue("cacheDifficultyTo")));
            }
            
            if (terrainDifficultyFiltered) {
                // get passed paramters from session context
                filter.setFromTerrainDifficulty(Double.parseDouble(getRequestParameterValue("terrainDifficultyFrom")));
                filter.setToTerrainDifficulty(Double.parseDouble(getRequestParameterValue("terrainDifficultyTo")));
            }
            
            if (sizeFiltered) {
                // get passed paramters from session context
                filter.setFromCacheSize(Integer.parseInt(getRequestParameterValue("sizeFrom")));
                filter.setToCacheSize(Integer.parseInt(getRequestParameterValue("sizeTo")));
            }
            
            loadCaches();
        }
        catch (NumberFormatException ne) {            
            setErrorMessage("One of the entered filter values is invalid.");            
        }
        catch (Exception e) {
            log.severe(e.getMessage());
            setErrorMessage("Unable to perform requested action.");
        }                
        return "FindCachesEvent";        
    }
    
    public String resetFilter() {        
        loadDefaultFilter();        
        loadCaches();
        
        return "FindCachesEvent";
    }
    
    public boolean getCurrentCacheAvailable() {
        return (currentCache != null);
    }
    
    public CacheDetails getCurrentCache() {
        return currentCache;
    }
    
    public CacheFilter getCacheFilter() {
        return filter;
    }
      
    public List<Cache> getCacheList() {
        // TODO check if running
        return caches;
    }
    
    public int getNrOfSearchResults() {
       return caches.size();
    }
       
    public boolean getResultListEmpty() {
        return caches.isEmpty();
    }
    
    public boolean isPositionFiltered() {
        return positionFiltered;
    }

    public boolean isSizeFiltered() {
        return sizeFiltered;
    }

    public boolean isCacheDifficultyFiltered() {
        return cacheDifficultyFiltered;
    }

    public boolean isTerrainDifficultyFiltered() {
        return terrainDifficultyFiltered;
    }
    
    public void setPositionFiltered(boolean positionFiltered) {
        this.positionFiltered = positionFiltered;
    }

    public void setSizeFiltered(boolean sizeFiltered) {
        this.sizeFiltered = sizeFiltered;
    }

    public void setCacheDifficultyFiltered(boolean cacheDifficultyFiltered) {
        this.cacheDifficultyFiltered = cacheDifficultyFiltered;
    }

    public void setTerrainDifficultyFiltered(boolean terrainDifficultyFiltered) {
        this.terrainDifficultyFiltered = terrainDifficultyFiltered;
    } 
        
//------------------------------------- private ---------------------------------------
    
    private void loadCaches() {        
        caches.clear();        
        caches = geoCachingWsProxy.getFilteredCacheList(filter).getCache();
    }
    
    private void loadDefaultFilter() {
        defaultFilter = geoCachingWsProxy.computeDefaultFilter();
        filter = defaultFilter;
    }

//-------------------------------------- members --------------------------------------
    
    private CacheDetails currentCache;
    private List<Cache> caches = new ArrayList<Cache>();
    private CacheFilter filter;
    private CacheFilter defaultFilter;
        
    private boolean positionFiltered = false;
    private boolean sizeFiltered = false;
    private boolean cacheDifficultyFiltered = false;
    private boolean terrainDifficultyFiltered = false;
    
    private static final Logger log = Logger.getLogger(CacheManager.class.getName());
    
}
