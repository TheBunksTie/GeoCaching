package at.wea5.geocaching.business;

import java.util.Enumeration;

import javax.el.ELContext;
import javax.el.ExpressionFactory;
import javax.el.ValueExpression;
import javax.faces.application.Application;
import javax.faces.context.FacesContext;
import javax.servlet.http.HttpServletRequest;

import at.wea5.geocaching.webserviceproxy.DataFilter;
import at.wea5.geocaching.webserviceproxy.GeoCachingService;
import at.wea5.geocaching.webserviceproxy.GeoCachingServiceSoap;
import at.wea5.geocaching.webserviceproxy.GeoPosition;

public abstract class ManagerBase {
//------------------------------------ constructor ------------------------------------
    protected ManagerBase() {
        GeoCachingService factory = new GeoCachingService();
        geoCachingWsProxy = factory.getGeoCachingServiceSoap();
        
        defaultFilter = geoCachingWsProxy.computeDefaultFilter();
        
        // copy values from defaultFilter to active filter
        activeFilter = defaultFilter;        
    }

//-------------------------------------- public ---------------------------------------
    
    public DataFilter getFilter() {
        return activeFilter;
    }
    
//------------------------------------ protected --------------------------------------
    
    protected GeoCachingServiceSoap getWSProxy() {
        
        if (geoCachingWsProxy == null) {
            GeoCachingService factory = new GeoCachingService();
            geoCachingWsProxy = factory.getGeoCachingServiceSoap();
        }
        return geoCachingWsProxy;        
    }
    
    
    /**
     * Since parameters are qualified with their full name 
     * within their view, e.g. :items:0:detailForm:articleId
     * it is necessary to provide some mechanism to find the
     * needed parameter and the corresponding value
     *     
     */
    protected String getRequestParameterValue(String name) {
        HttpServletRequest request = (HttpServletRequest)FacesContext.getCurrentInstance().getExternalContext().getRequest();
        String paramValue = null;
        
        Enumeration<String> e = request.getParameterNames();
        boolean parameterFound = false;
        
        while (!parameterFound && e.hasMoreElements()) {
            String paramName = e.nextElement();
           
            if (paramName.indexOf(name) > 0) {
                paramValue = request.getParameter(paramName);
                parameterFound = true;
            }
        }
        return paramValue;
    }
    
    
    protected Object getSessionVariable(String name) {
        FacesContext facesContext = FacesContext.getCurrentInstance();
        Application application = facesContext.getApplication();
        
        ExpressionFactory expressionFactory = application.getExpressionFactory();
        ELContext elContext = facesContext.getELContext();
        
        ValueExpression valueExpression = expressionFactory.createValueExpression(elContext,
                "#{" + name + "}", 
                Object.class);
        
        return valueExpression.getValue(elContext);
    }
    
    protected void setErrorMessage(String message) {
        ErrorManager error = (ErrorManager) getSessionVariable("ErrorManager");
        error.addErrorMessage(message);
    }
    
    protected void loadDefaultFilter() {
        defaultFilter = geoCachingWsProxy.computeDefaultFilter();        
        
        activeFilter = defaultFilter;
        //setFilterToDefault();
    }
    
    protected void setFilterToDefault() {
        activeFilter.setFromCacheDifficulty(defaultFilter.getFromCacheDifficulty());
        activeFilter.setToCacheDifficulty(defaultFilter.getToCacheDifficulty());
        
        activeFilter.setFromTerrainDifficulty(defaultFilter.getFromTerrainDifficulty());
        activeFilter.setToTerrainDifficulty(defaultFilter.getToTerrainDifficulty());
        
        activeFilter.setFromCacheSize(defaultFilter.getFromCacheSize());
        activeFilter.setToCacheSize(defaultFilter.getToCacheSize());
        
        GeoPosition from = new GeoPosition();
        from.setLatitude(defaultFilter.getFromPosition().getLatitude());
        from.setLongitude(defaultFilter.getFromPosition().getLongitude());
        
        activeFilter.setFromPosition(from);
        
        GeoPosition to = new GeoPosition();
        to.setLatitude(defaultFilter.getToPosition().getLatitude());
        to.setLongitude(defaultFilter.getToPosition().getLongitude());
        
        activeFilter.setToPosition(to);
    }
    
//-------------------------------------- members --------------------------------------
    //private static final Logger log = Logger.getLogger(ManagerBase.class.getName());
    
    // TODO make configurable via context initializer and static 
    private static final String serviceAddr  = "http://localhost:2037/GeoCachingService.asmx";
    private static final String wsdl         = serviceAddr + "?wsdl";
    private static final String namespaceURI = "http://GeoCaching.Services//";
    private static final String serviceName  = "GeoCachingWebService";
    
    protected GeoCachingServiceSoap geoCachingWsProxy = null;
    protected DataFilter activeFilter = new DataFilter();
    protected DataFilter defaultFilter;

}
