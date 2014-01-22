package at.wea5.geocaching.business.webserviceImplementation;

import java.util.logging.Logger;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;
import javax.faces.context.FacesContext;
import javax.servlet.http.HttpSession;

import at.wea5.geocaching.Settings;
import at.wea5.geocaching.business.ManagerBase;
import at.wea5.geocaching.webserviceproxy.User;

/**
 * represents the business logic part of de/authenticating a user
 * who wants to login
 *
 */
@ManagedBean(name="AuthenticationManager")
@SessionScoped
public class AuthenticationManager extends ManagerBase {

//------------------------------------ constructor ------------------------------------

//-------------------------------------- public ---------------------------------------
    
    public String getUsername() {
        return userdata.getName();
    }
    
    public boolean getLoggedIn() {
        return (userdata != null);
    }
    
    public String loginUser() {
        try {
            // get passed parameters from session context
            String username = getRequestParameterValue("username");
            String password = getRequestParameterValue("password");        
                                  
            // use webservice-method  to get user data                      
            userdata = geoCachingWsProxy.authenticateUser(username, password);
            
            if (userdata != null) {
                FacesContext.getCurrentInstance().getExternalContext().getSession(true);
                                
                return "HomeEvent";
            }
            else {
                // back to login page and display error message
                setErrorMessage("Provided credentials are invalid.");
                return "LoginEvent";
            }

        }
        catch (Exception e) {
            log.severe(e.getMessage());
            setErrorMessage("Unable to perform requested action.");
            
            return Settings.LoginView;            
        }
    }
    
    public User getCurrentUser() {
        return userdata;
    }

    public String logoutUser() {
        userdata = null;
        ((HttpSession) FacesContext.getCurrentInstance().getExternalContext().getSession(false)).invalidate();
        return Settings.HomeView;
    }

    
//------------------------------------- private ---------------------------------------
        
//-------------------------------------- members --------------------------------------
    private static final Logger log = Logger.getLogger(AuthenticationManager.class.getName());
    
    private User userdata;

}
