package at.wea5.geocaching.business.webserviceImplementation;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.logging.Logger;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;
import javax.xml.datatype.DatatypeConfigurationException;

import org.primefaces.event.map.OverlaySelectEvent;
import org.primefaces.model.map.DefaultMapModel;
import org.primefaces.model.map.LatLng;
import org.primefaces.model.map.MapModel;
import org.primefaces.model.map.Marker;

import at.wea5.geocaching.Settings;
import at.wea5.geocaching.Util;
import at.wea5.geocaching.business.ManagerBase;
import at.wea5.geocaching.business.exception.NoCurrentUserException;
import at.wea5.geocaching.webserviceproxy.Cache;
import at.wea5.geocaching.webserviceproxy.CacheDetails;
import at.wea5.geocaching.webserviceproxy.GeoPosition;
import at.wea5.geocaching.webserviceproxy.LogEntry;
import at.wea5.geocaching.webserviceproxy.Rating;
import at.wea5.geocaching.webserviceproxy.User;

@ManagedBean(name="CacheManager", eager=true)
@SessionScoped
public class CacheManager extends ManagerBase {

//------------------------------------ constructor ------------------------------------
    
    public CacheManager() {
        // tries to connect to configured webservice and loads default filter
        super();
        
        
        // load whole cache list        
        loadCacheSizes();
        loadCaches();
        fillMapModel();     
    }

//-------------------------------------- public ---------------------------------------
          
    public String getMapCenter() {        
        try {
            User u = getCurrentUser(); 
            return u.getPosition().getLatitude() + "," + u.getPosition().getLongitude();
        }
        catch (Exception e) {
            // default coordinates are the ones if Hagenberg
            return "48.363889,14.519444";
        }       
    }
    
    public String showDetailsFromList() {
        try {
            // read id of requested cache and retrieve all details via webservice
            return showDetails(Integer.parseInt(getRequestParameterValue("cacheId")));
        }
        catch (NumberFormatException ne) {            
            setErrorMessage("The id of the request cache is not a number.");            
        }
        return Settings.CacheView;
    }
             
    public String getFilteredCacheList() {        
        
        // reset filter to default values and afterwards check 
        // for any user-provided filter restrictions
        
        activeFilter = getDefaultFilter();
        
        try {
            // process requested filters            
            if (positionFiltered) {
                // get passed parameters from session context
                GeoPosition from = new GeoPosition();                                                
                from.setLatitude(Double.parseDouble(getRequestParameterValue("latitudeFrom")));
                from.setLongitude(Double.parseDouble(getRequestParameterValue("longitudeFrom")));                
                activeFilter.setFromPosition(from);
                
                GeoPosition to = new GeoPosition();                                                
                to.setLatitude(Double.parseDouble(getRequestParameterValue("latitudeTo")));
                to.setLongitude(Double.parseDouble(getRequestParameterValue("longitudeTo")));
                activeFilter.setToPosition(to);
            }
            
            if (cacheDifficultyFiltered) {
                // get passed parameters from session context
                activeFilter.setFromCacheDifficulty(Double.parseDouble(getRequestParameterValue("cacheDifficultyFrom")));
                activeFilter.setToCacheDifficulty(Double.parseDouble(getRequestParameterValue("cacheDifficultyTo")));
            }
            
            if (terrainDifficultyFiltered) {
                // get passed parameters from session context
                activeFilter.setFromTerrainDifficulty(Double.parseDouble(getRequestParameterValue("terrainDifficultyFrom")));
                activeFilter.setToTerrainDifficulty(Double.parseDouble(getRequestParameterValue("terrainDifficultyTo")));
            }
            
            if (sizeFiltered) {
                // get passed parameters from session context
                //log.severe(getRequestParameterValue("sizeFrom"));
                activeFilter.setFromCacheSize(Integer.parseInt(getRequestParameterValue("sizeFrom")));
                activeFilter.setToCacheSize(Integer.parseInt(getRequestParameterValue("sizeTo")));
            }
            
            loadCaches();
            fillMapModel();
        }
        catch (NumberFormatException ne) {            
            setErrorMessage("One (or more) of the entered filter values is invalid.");            
        }
        catch (Exception e) {
            log.severe(e.getMessage());
            setErrorMessage("Unable to perform requested action.");
        }                
        return Settings.CacheView;
    }
        
    public String rateCurrentCache() {
        try {            
            // check entered rating value
            int grade = Integer.parseInt(getRequestParameterValue("cacheRating"));
            
            if (grade >= 1 && grade <= 10) {
                Rating rating = new Rating();
                
                // add time: today               
                rating.setCreationDate(Util.convertToXML(new Date()));
                               
                // add creator: currently logged in user
                User currentUser = getCurrentUser();                
                rating.setCreatorId(currentUser.getId());
                
                // add related cache
                rating.setCacheId(currentCache.getCache().getId());

                // add rating itself
                rating.setGrade(grade);
                
                // send back to data-backend via webservice
                geoCachingWsProxy.addRatingForCache(currentUser, rating);
                
                // refresh data of current cache
                currentCache = geoCachingWsProxy.getDetailedCache(currentCache.getCache().getId());
                
            }
            else {
                setErrorMessage("Provide rating ist invalid. Must be a number between 1 and 10.");
            }
        }
        catch (NoCurrentUserException ue) {
            setErrorMessage("You need to be logged in to rate a cache.");
        }
        catch (DatatypeConfigurationException de) {
            setErrorMessage(de.getMessage());
        }
        catch(NumberFormatException e) {
            setErrorMessage("Provide rating ist invalid. Must be a number between 1 and 10.");
        }
        return Settings.CacheDetailsView;
    }
    
    public String addLogEntry() {
        try {            
            // get entered comment
            String comment = getRequestParameterValue("comment");
            
            LogEntry logEntry = new LogEntry();
            
            // add time: today            
            logEntry.setCreationDate(Util.convertToXML(new Date()));
                           
            // add creator: currently logged in user
            User currentUser = getCurrentUser();                
            logEntry.setCreatorId(currentUser.getId());
            
            // add related cache
            logEntry.setCacheId(currentCache.getCache().getId());

            // add found flag
            logEntry.setIsFound(cacheFound);

            // add optional comment
            logEntry.setComment(comment);
            
            // send back to data-backend via webservice
            geoCachingWsProxy.addLogEntryForCache(currentUser, logEntry);
            
            // refresh data of current cache
            currentCache = geoCachingWsProxy.getDetailedCache(currentCache.getCache().getId());
                
        }
        catch (NoCurrentUserException ue) {
            setErrorMessage("You need to be logged in to rate a cache.");
        }
        catch (DatatypeConfigurationException de) {
            setErrorMessage(de.getMessage());
        }
        return Settings.CacheDetailsView;    
    }
    
    public void onMarkerSelect(OverlaySelectEvent event) {  
        currentMarker = (Marker) event.getOverlay();
        
        // also set currentCache for display of information
        int cacheId = ((Cache)currentMarker.getData()).getId();
        currentCache = geoCachingWsProxy.getDetailedCache(cacheId);
    }  
        
    public MapModel getMapModel() {
        return mapModel;        
    }
    
    public Marker getCurrentMarker() {  
        return currentMarker;  
    }  
    
    public boolean getCurrentCacheAvailable() {
        return (currentCache != null);
    }
    
    public CacheDetails getCurrentCache() {
        return currentCache;
    }
         
    public List<CacheSizeItem> getCacheSizeList() {
        return cacheSizes;
    }
    
    public List<Cache> getCacheList() {
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
    
    public void setCacheFound(boolean value) {
        this.cacheFound = value;
    }
    
    public boolean getCacheFound() {
        return cacheFound;
    }
    
    public String getFromSizeFilter() {
        return Integer.toString(activeFilter.getFromCacheSize());
    }
    
    public void setFromSizeFilter(String id) {
        activeFilter.setFromCacheSize(Integer.parseInt(id));
    }
    
    public String getToSizeFilter() {
        return Integer.toString(activeFilter.getToCacheSize());
    }
     
    public void setToSizeFilter(String id) {
        activeFilter.setToCacheSize(Integer.parseInt(id));
    }
    
    public boolean getIsImageListEmpty() {
        return (currentCache.getImages().getImage().size() == 0);
    }
    
//------------------------------------- private ---------------------------------------
    
    private String showDetails(int id) {        
        try {
            // read id of requested cache and retrieve all details via webservice
            currentCache = geoCachingWsProxy.getDetailedCache(id);
            
            if (currentCache == null) {
                setErrorMessage("The requested cache could not be found.");
            }
        }       
        catch (Exception e) {
            log.severe(e.getMessage());
            setErrorMessage("Unable to show requested details for cache.");
        }            
        return Settings.CacheDetailsView;
    }
    
    private User getCurrentUser() throws NoCurrentUserException {
        AuthenticationManager authenticationManager = (AuthenticationManager) getSessionVariable("AuthenticationManager");
        
        User user = authenticationManager.getCurrentUser();
        
        if (user != null) {
            return user;
        }
        else {
            throw new NoCurrentUserException();
        }
    }
    
    private void loadCacheSizes() {
        cacheSizes.clear();
        
        int i = 1;
        for (String size : geoCachingWsProxy.getCacheSizeList().getString()) {
            cacheSizes.add(new CacheSizeItem(size, Integer.toString(i)));
            i++;
        }       
    }
    
    private void loadCaches() {    
        caches = geoCachingWsProxy.getFilteredCacheList(activeFilter).getCache();
    }
       
    private void fillMapModel() {
        mapModel = new DefaultMapModel();
        
        for (Cache c : caches) {
            mapModel.addOverlay(new Marker(new LatLng(c.getPosition().getLatitude(), c.getPosition().getLongitude()), 
                                           c.getName(), c));
        }
        
    }
        
//-------------------------------------- members --------------------------------------
        
    private CacheDetails currentCache;
    private List<Cache> caches;
    private List<CacheSizeItem> cacheSizes = new ArrayList<CacheSizeItem>(); 
    
    private Marker currentMarker;
    private MapModel mapModel;
    
    private boolean positionFiltered = false;
    private boolean sizeFiltered = false;
    private boolean cacheDifficultyFiltered = false;
    private boolean terrainDifficultyFiltered = false;
    
    private boolean cacheFound;
    
    private static final Logger log = Logger.getLogger(CacheManager.class.getName());    
}
