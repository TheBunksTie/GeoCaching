package at.wea5.geocaching.business;

import java.util.ArrayList;
import java.util.Collection;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.RequestScoped;

/**
 * stores any error message and provides
 * it for display
 * 
 */
@ManagedBean(name = "ErrorManager")
@RequestScoped
public class ErrorManager {

//------------------------------------ constructor ------------------------------------

//-------------------------------------- public ---------------------------------------
    
    public boolean getErrorOccured() {
        return errorOccured;
    }
    
    public void addErrorMessage(String message) {        
        errorMessage = message;
        errorLog.add(message);
        errorOccured = true;
    }

    public String getMessage() {
        errorOccured = false;
        return errorMessage;
    }

    
//------------------------------------- private ---------------------------------------
       
//-------------------------------------- members --------------------------------------
    
    private String errorMessage;
    private boolean errorOccured = false;
    
    // maybe useful when all occured errors are relvant
    private Collection<String> errorLog = new ArrayList<String>();
}