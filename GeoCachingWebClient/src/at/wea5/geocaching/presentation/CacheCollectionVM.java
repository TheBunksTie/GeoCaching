package at.wea5.geocaching.presentation;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;




import at.wea5.geocaching.domainmodel.Cache;
import at.wea5.geocaching.domainmodel.GeoPosition;

@ManagedBean (name="CacheCollection")
@SessionScoped
public class CacheCollectionVM {
    
//------------------------------------ constructor ------------------------------------
    
    public CacheCollectionVM() {
        // dummy creation only for testing purposes
        Cache dummy = new Cache(1, 1, 12, "Die Nockn", new Date(), "Normal", 213, new GeoPosition(47.132, 14.156), "a very special an nice cache");
        caches.add(dummy);
        currentCache = dummy;
        
        // TODO only testing
        searchResultsReady = true;
    }

//-------------------------------------- public ---------------------------------------
    public Cache getCurrentCache() {
        return currentCache;
    }
    
    public List<Cache> getCacheList() {
        // TODO check if running
        // reset indicator flag, because results are fetched and displayed
        searchResultsReady = false;
        return caches;
    }
        
    public void setCacheList(List<Cache> list) {
        searchResultsReady = true;
        caches = list;
    }
    
    public boolean getResultListEmpty() {
        return caches.isEmpty();
    }
    
    public boolean getSearchResultsReady() {
        return searchResultsReady;
    }
    
    public void addCacheToList(Cache c) {
        // TODO maybe check against null (c and caches)
        caches.add(c);
    }
    
    
    public String showDetails() {
        // TODO set current cache by passed id from form
        
        return "CacheDetailsEvent";
    }
    
//------------------------------------- private ---------------------------------------

//-------------------------------------- members --------------------------------------
    private Cache currentCache;
    private List<Cache> caches = new ArrayList<Cache>();
    private boolean searchResultsReady = false;
}
