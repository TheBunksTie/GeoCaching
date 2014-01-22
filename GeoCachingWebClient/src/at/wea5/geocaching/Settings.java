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
    
    public static final String HomeView = "HomeEvent";
    public static final String LoginView = "LoginEvent";
    public static final String StatisticsView = "StatisticsEvent";
    public static final String CacheView = "FindCachesEvent";
    public static final String CacheDetailsView = "CacheDetailsEvent";
    
//------------------------------------- private ---------------------------------------

//-------------------------------------- members --------------------------------------

}
