package at.wea5.geocaching.presentation;

import javax.servlet.ServletContext;
import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

import at.wea5.geocaching.Settings;


/**
 * This class gets initialized when the web application is started.
 * The class provides those configuration data, which has been defined
 * in the web.xml file.
 */
public class ContextListenerInitializer implements ServletContextListener {
    
public void contextInitialized(ServletContextEvent contextEvent) {
      ServletContext sc = contextEvent.getServletContext();
      
      // retrieve required parameters from web.xml (mainly WS-Configuration)
      String serviceHost = sc.getInitParameter("WS_HOST");
      String servicePort = sc.getInitParameter("WS_PORT");
      String serviceFile = sc.getInitParameter("WS_SERVICE_FILE");
      String nameSpaceURI = sc.getInitParameter("WS_NAMESPACE_URI");
      String serviceName = sc.getInitParameter("WS_SERVICE_NAME");
      
      String wsdlUrl = "http://" + serviceHost + ":" + servicePort + "/" + serviceFile + "?wsdl";
      
      // pass on option to settings class
      Settings.setWsDetails(wsdlUrl, nameSpaceURI, serviceName);
}

    
    public void contextDestroyed(ServletContextEvent arg0) {
        // intentionally left blank
    }
}
