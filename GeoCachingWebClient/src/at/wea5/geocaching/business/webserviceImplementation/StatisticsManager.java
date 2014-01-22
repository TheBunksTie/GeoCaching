package at.wea5.geocaching.business.webserviceImplementation;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.logging.Logger;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;
import javax.xml.datatype.DatatypeConfigurationException;

import at.wea5.geocaching.Settings;
import at.wea5.geocaching.business.ManagerBase;
import at.wea5.geocaching.business.exception.LimitInvalidException;
import at.wea5.geocaching.webserviceproxy.GeoPosition;
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
        try {                       
            // process filters first            
            if (isDateFilterRequested) {
                String fromDate = getRequestParameterValue("dateFrom");
                String toDate = getRequestParameterValue("dateTo");
                
                activeFilter.setFromJavaDate(dateFormat.parse(fromDate));
                activeFilter.setToJavaDate(dateFormat.parse(toDate));
            }
            
            if (isPositionFilterRequested) {
                GeoPosition from = new GeoPosition();                                                
                from.setLatitude(Double.parseDouble(getRequestParameterValue("latitudeFrom")));
                from.setLongitude(Double.parseDouble(getRequestParameterValue("longitudeFrom")));                
                activeFilter.setFromPosition(from);
                
                GeoPosition to = new GeoPosition();                                                
                to.setLatitude(Double.parseDouble(getRequestParameterValue("latitudeTo")));
                to.setLongitude(Double.parseDouble(getRequestParameterValue("longitudeTo")));
                activeFilter.setToPosition(to);
            }
            
            // retrieve dataset depending on user selection
            // (if because string switching only from java 1.7 on, BUT jsf needs 1.6 compliance)
            
            switch (requestedStatistic) {
            case byFound: {
                // check and set topXField limit to filter
                setFilterLimit();                
                currentDataset = geoCachingWsProxy.getUserByFoundCaches(activeFilter);                
                break;
            }
            case byHidden: {
                setFilterLimit();                
                currentDataset = geoCachingWsProxy.getUserByHiddenCaches(activeFilter);
                break;
            }
            case byLogging: {
                setFilterLimit();                
                currentDataset = geoCachingWsProxy.getMostLoggedCaches(activeFilter);
                break;
            }
            case byRating: {
                setFilterLimit();                
                currentDataset = geoCachingWsProxy.getBestRatedCache(activeFilter);
                break;
            }
            case cachesByCacheDifficulty: {
                currentDataset = geoCachingWsProxy.getCacheDistributionByCacheDifficulty(activeFilter);
                break;
            }
            case cachesBySize: {
                currentDataset = geoCachingWsProxy.getCacheDistributionBySize(activeFilter);
                break;
            }
            case cachesByTerrainDifficulty: {
                currentDataset = geoCachingWsProxy.getCacheDistributionByTerrainDifficulty(activeFilter);
                break;
            }
            default:
                break;
            
            }
        }
        catch (ParseException e) {
            setErrorMessage("One (or more) of the entered filter dates is invalid.");
        }
        catch (DatatypeConfigurationException e) {            
            setErrorMessage("One (or more) of the entered filter dates is invalid.");
        }
        catch(NumberFormatException e) {
            setErrorMessage("One (or more) of the entered position values is invalid."); 
        }
        catch(LimitInvalidException l) {
            setErrorMessage("The entered top X value is invalid.");
        }
        catch(Exception e) {
            log.severe(e.getMessage());
            setErrorMessage("Unable to perform requested action.");
        }
        
        return Settings.StatisticsView;
    }
    
    public String resetFilter() {
        loadDefaultFilter();
        return Settings.StatisticsView;
    }  
    
    public int getNrOfData() {
        return currentDataset.getData().getStatisticData().size();               
    }
    
    public boolean getIsCurrentDatasetEmpty() {
        return currentDataset.getData().getStatisticData().isEmpty();
    }
    
    public String getRequestedStatistic() {
        return requestedStatistic.toStringCode();
    }

    public void setRequestedStatistic(String requestStatistic) {
        this.requestedStatistic = AvailableDatasets.fromCode(Integer.parseInt(requestStatistic));
    }   
    
    public boolean isPositionFiltered() {
        return isPositionFilterRequested;
    }

    public void setPositionFiltered(boolean isPositionFiltered) {
        this.isPositionFilterRequested = isPositionFiltered;
    }

    public boolean isDateFiltered() {
        return isDateFilterRequested;
    }

    public void setDateFiltered(boolean isDateFiltered) {
        this.isDateFilterRequested = isDateFiltered;
    }
    
    public StatisticDataset getStatisticDataset() {
        return currentDataset;
    }
    
    public boolean isTopXFiltered() {
        return isTopXFiltered;
    }

    public void setTopXFiltered(boolean isTopXFiltered) {
        this.isTopXFiltered = isTopXFiltered;
    }
    
//------------------------------------- private ---------------------------------------
    
    private void setFilterLimit() throws LimitInvalidException {
        try {
            String topX = getRequestParameterValue("topXEdit");
            activeFilter.setLimit(Integer.parseInt(topX));
        }
        catch (NumberFormatException e) {
            throw new LimitInvalidException();
        }        
    }
    
//-------------------------------------- members --------------------------------------   

    private StatisticDataset currentDataset;
    private AvailableDatasets requestedStatistic = AvailableDatasets.byRating;
    private boolean isPositionFilterRequested;
    private boolean isDateFilterRequested;
    private boolean isTopXFiltered = true;
    
    private static final SimpleDateFormat dateFormat = new SimpleDateFormat("dd.MM.yy");
    
    private static final Logger log = Logger.getLogger(StatisticsManager.class.getName());

    private enum AvailableDatasets {
        byRating(1),
        byLogging(2),
        byFound(3),
        byHidden(4),
        cachesBySize(5),
        cachesByCacheDifficulty(6),
        cachesByTerrainDifficulty (7);
        
        private int code;
        
        private AvailableDatasets(int code) {
            this.code = code;
        }
              
        public int toCode() {
            return code;
        }
        
        public String toStringCode() {
            return Integer.toString(code);
        }
        
        public static AvailableDatasets fromCode(int code) {
            switch (code) {
                case 1: return AvailableDatasets.byRating;
                case 2: return AvailableDatasets.byLogging;
                case 3: return AvailableDatasets.byFound;
                case 4: return AvailableDatasets.byHidden;
                case 5: return AvailableDatasets.cachesBySize;
                case 6: return AvailableDatasets.cachesByCacheDifficulty;
                case 7: return AvailableDatasets.cachesByTerrainDifficulty;
            }
            return null;
        }
    }
    
}
