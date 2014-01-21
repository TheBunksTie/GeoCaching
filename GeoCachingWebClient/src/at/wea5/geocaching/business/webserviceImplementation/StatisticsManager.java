package at.wea5.geocaching.business.webserviceImplementation;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;

import at.wea5.geocaching.business.ManagerBase;
import at.wea5.geocaching.webserviceproxy.StatisticDataset;

@ManagedBean (name="StatisticsManager")
@SessionScoped
public class StatisticsManager extends ManagerBase {

//------------------------------------ constructor ------------------------------------
    public StatisticsManager() {
        // loads default filter
        super();
        
        // load most famous caches as default statistic
        currentDataset = geoCachingWsProxy.getBestRatedCache(activeFilter);
    }
//-------------------------------------- public ---------------------------------------
    
    public String getRequestedStatistics() {
        // TODO
        return "StatisticsEvent";
    }
    
    @Override
    public String resetFilter() {
        loadDefaultFilter();
        return "StatisticsEvent";
    }  
    
  
    public int getNrOfData() {
        return currentDataset.getData().getStatisticData().size();               
    }
    
    public boolean getIsCurrentDatasetEmpty() {
        return currentDataset.getData().getStatisticData().isEmpty();
    }
    
    public String getRequestStatistic() {
        return requestStatistic;
    }

    public void setRequestStatistic(String requestStatistic) {
        this.requestStatistic = requestStatistic;
    }   
    
    public boolean isPositionFiltered() {
        return isPositionFiltered;
    }

    public void setPositionFiltered(boolean isPositionFiltered) {
        this.isPositionFiltered = isPositionFiltered;
    }

    public boolean isDateFiltered() {
        return isDateFiltered;
    }

    public void setDateFiltered(boolean isDateFiltered) {
        this.isDateFiltered = isDateFiltered;
    }
    
    public StatisticDataset getStatisticDataset() {
        return currentDataset;
    }
    
//------------------------------------- private ---------------------------------------

//-------------------------------------- members --------------------------------------
    
    private StatisticDataset currentDataset;
    private String requestStatistic;
    private boolean isPositionFiltered;
    private boolean isDateFiltered;
   
}
