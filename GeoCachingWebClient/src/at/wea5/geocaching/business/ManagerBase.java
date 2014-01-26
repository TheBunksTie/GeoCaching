package at.wea5.geocaching.business;

import java.net.MalformedURLException;
import java.net.URL;
import java.util.Enumeration;
import java.util.logging.Logger;

import javax.el.ELContext;
import javax.el.ExpressionFactory;
import javax.el.ValueExpression;
import javax.faces.application.Application;
import javax.faces.context.FacesContext;
import javax.servlet.http.HttpServletRequest;
import javax.xml.namespace.QName;
import javax.xml.ws.Service;

import at.wea5.geocaching.Settings;
import at.wea5.geocaching.webserviceproxy.DataFilter;
import at.wea5.geocaching.webserviceproxy.GeoCachingService;
import at.wea5.geocaching.webserviceproxy.GeoCachingServiceSoap;

public abstract class ManagerBase {
    
//------------------------------------ constructor ------------------------------------
    protected ManagerBase() {       
        
        try {            
            // dynamically conenct to configured ws and create proxy        
            Service factory = Service.create(new URL(Settings.getWsWsdlPath()), new QName(Settings.getWsNamespaceUri(), Settings.getWsServiceName()));            
            geoCachingWsProxy = factory.getPort(GeoCachingServiceSoap.class);
            
            // check if ws itself has connection to its databackend            
            if (geoCachingWsProxy.isServiceAvailable()) {

                // get default values for filter
                activeFilter = geoCachingWsProxy.computeDefaultFilter();
                isServiceAvailable = true;

            }
            else {
                setErrorMessage("WebService is unavailable.");
            }            
        } 
        catch (MalformedURLException e) {
            log.severe(e.getMessage());
            setErrorMessage("WebService is unavailable.");            
        }
        
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
    
    protected DataFilter getDefaultFilter() {
        return geoCachingWsProxy.computeDefaultFilter();               
    }
        
//-------------------------------------- members --------------------------------------

    protected GeoCachingServiceSoap geoCachingWsProxy = null;
    protected DataFilter activeFilter;
    protected boolean isServiceAvailable = false;
    
    private static final Logger log = Logger.getLogger(ManagerBase.class.getName());
}
