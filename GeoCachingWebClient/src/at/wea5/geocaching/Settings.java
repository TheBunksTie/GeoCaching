package at.wea5.geocaching;

import javax.faces.bean.ApplicationScoped;
import javax.faces.bean.ManagedBean;

@ManagedBean(name="Settings", eager=true)
@ApplicationScoped
public class Settings {

//------------------------------------ constructor ------------------------------------

//-------------------------------------- public ---------------------------------------
    
    public String getHomeView() {
        return HomeView;
    }
    
    public String getLoginView() {
        return LoginView;
    }
    
    public String getStatisticsView() {
        return StatisticsView;
    }
    
    public String getCacheView() {
        return CacheView;
    }
    
    public String getCacheDetailsView() {
        return CacheDetailsView;
    }
    
    public static String getWsWsdlPath() {
        return wsWsdlUrl;
    }

    public static String getWsNamespaceUri() {
        return wsNamespaceUri;
    }

    public static String getWsServiceName() {
        return wsServiceName;
    }

    public static void setWsDetails(String wsWsdlUrl, String wsNamespaceUri, String wsServiceName) {
        Settings.wsWsdlUrl = wsWsdlUrl;
        Settings.wsNamespaceUri = wsNamespaceUri;
        Settings.wsServiceName = wsServiceName;
    }
    
    // navigation strings
    public static final String HomeView = "HomeEvent";
    public static final String LoginView = "LoginEvent";
    public static final String StatisticsView = "StatisticsEvent";
    public static final String CacheView = "FindCachesEvent";
    public static final String CacheDetailsView = "CacheDetailsEvent";
    
//------------------------------------- private ---------------------------------------
    
    
    
//-------------------------------------- members --------------------------------------
          
    // web service parameters
    private static String wsWsdlUrl;
    private static String wsNamespaceUri;
    private static String wsServiceName;
}
