package at.wea5.geocaching.presentation;

import javax.servlet.ServletContext;
import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;


/**
 * This class gets initialized when the web application is started.
 * The class provides those configuration data, which has been defined
 * in the web.xml file.
 */
public class ContextListenerInitializer implements ServletContextListener {
    
    public void contextInitialized(ServletContextEvent contextEvent) {
          ServletContext sc = contextEvent.getServletContext();
          
          // TODO define required parameters
          String dsn = sc.getInitParameter("DB_DSN");
          String user = sc.getInitParameter("DB_USER");
          String password = sc.getInitParameter("DB_PASSWORD");
          String delegateClass = sc.getInitParameter("Shop_DELEGATE");

          // TODO implement service locator
          //ServiceLocator.getInstance().init(dsn, user, password, delegateClass);
    }

    
    public void contextDestroyed(ServletContextEvent arg0) {
        // intentionally left blank
    }
}
