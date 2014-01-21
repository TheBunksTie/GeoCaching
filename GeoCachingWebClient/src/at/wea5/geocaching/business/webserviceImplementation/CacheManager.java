package at.wea5.geocaching.business.webserviceImplementation;


import java.util.ArrayList;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.List;
import java.util.logging.Logger;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;
import javax.xml.datatype.DatatypeConfigurationException;
import javax.xml.datatype.DatatypeFactory;
import javax.xml.datatype.XMLGregorianCalendar;

import org.primefaces.event.map.OverlaySelectEvent;
import org.primefaces.model.map.DefaultMapModel;
import org.primefaces.model.map.LatLng;
import org.primefaces.model.map.MapModel;
import org.primefaces.model.map.Marker;

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
        // loads default filter
        super();
        
        // load whole cache list        
        loadCaches();
        fillMapModel();
    }

//-------------------------------------- public ---------------------------------------
          
    public String showDetailsFromMap() {
        try {
            // read id of requested cache and retrieve all details via webservice
            int cacheId = ((Cache)currentMarker.getData()).getId();
            return showDetails(cacheId);
        }
        catch (Exception e) {            
            log.severe(e.getMessage());
            setErrorMessage("Unable to show requested details for cache.");            
        }
        return "FindCachesEvent";
    }    
    
    public String showDetailsFromList() {
        try {
            // read id of requested cache and retrieve all details via webservice
            return showDetails(Integer.parseInt(getRequestParameterValue("cacheId")));
        }
        catch (NumberFormatException ne) {            
            setErrorMessage("The id of the request cache is not a number.");            
        }
        return "FindCachesEvent";
    }
             
    public String getFilteredCacheList() {        
        
        // reset filter to default values
        //setFilterToDefault();
        
        activeFilter = defaultFilter;
        
        try {
            // process requested filters            
            if (positionFiltered) {
                // get passed paramters from session context
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
                // get passed paramters from session context
                activeFilter.setFromCacheDifficulty(Double.parseDouble(getRequestParameterValue("cacheDifficultyFrom")));
                activeFilter.setToCacheDifficulty(Double.parseDouble(getRequestParameterValue("cacheDifficultyTo")));
            }
            
            if (terrainDifficultyFiltered) {
                // get passed paramters from session context
                activeFilter.setFromTerrainDifficulty(Double.parseDouble(getRequestParameterValue("terrainDifficultyFrom")));
                activeFilter.setToTerrainDifficulty(Double.parseDouble(getRequestParameterValue("terrainDifficultyTo")));
            }
            
            if (sizeFiltered) {
                // get passed paramters from session context
                activeFilter.setFromCacheSize(Integer.parseInt(getRequestParameterValue("sizeFrom")));
                activeFilter.setToCacheSize(Integer.parseInt(getRequestParameterValue("sizeTo")));
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
    
    @Override
    public String resetFilter() {        
        loadDefaultFilter();        
        loadCaches();
        
        return "FindCachesEvent";
    }
    
    public String rateCurrentCache() {
        try {            
            // check entered rating value
            int grade = Integer.parseInt(getRequestParameterValue("cacheRating"));
            
            if (grade >= 1 && grade <= 10) {
                Rating rating = new Rating();
                
                // add time: today
                calendar.setTime(new Date());                     
                XMLGregorianCalendar xml = DatatypeFactory.newInstance().newXMLGregorianCalendar(calendar);
                rating.setCreationDate(xml);
                               
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
        return "CacheDetailsEvent";
    }
    
    public String addLogEntry() {
        try {            
            // get entered comment
            String comment = getRequestParameterValue("comment");
            
            LogEntry logEntry = new LogEntry();
            
            // add time: today
            calendar.setTime(new Date());                     
            XMLGregorianCalendar xml = DatatypeFactory.newInstance().newXMLGregorianCalendar(calendar);
            logEntry.setCreationDate(xml);
                           
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
        return "CacheDetailsEvent";        
    }
    
    public void onMarkerSelect(OverlaySelectEvent event) {  
        currentMarker = (Marker) event.getOverlay();  
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
    
    public void setCacheFound(boolean value) {
        this.cacheFound = value;
    }
    
    public boolean getCacheFound() {
        return cacheFound;
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
        return "CacheDetailsEvent";
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
    
    private void loadCaches() {        
        caches.clear();        
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
    private List<Cache> caches = new ArrayList<Cache>();
    
    private Marker currentMarker;
    private MapModel mapModel;
    
    private boolean positionFiltered = false;
    private boolean sizeFiltered = false;
    private boolean cacheDifficultyFiltered = false;
    private boolean terrainDifficultyFiltered = false;
    
    private boolean cacheFound;
    
    private GregorianCalendar calendar = new GregorianCalendar();            

    
    private static final Logger log = Logger.getLogger(CacheManager.class.getName());
    
}
