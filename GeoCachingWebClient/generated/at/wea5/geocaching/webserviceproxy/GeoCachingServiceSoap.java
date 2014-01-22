
package at.wea5.geocaching.webserviceproxy;

import javax.jws.WebMethod;
import javax.jws.WebParam;
import javax.jws.WebResult;
import javax.jws.WebService;
import javax.xml.bind.annotation.XmlSeeAlso;
import javax.xml.ws.RequestWrapper;
import javax.xml.ws.ResponseWrapper;


/**
 * This class was generated by the JAX-WS RI.
 * JAX-WS RI 2.2.8
 * Generated source version: 2.2
 * 
 */
@WebService(name = "GeoCachingServiceSoap", targetNamespace = "http://GeoCaching.Services/")
@XmlSeeAlso({
    ObjectFactory.class
})
public interface GeoCachingServiceSoap {


    /**
     * 
     * @param username
     * @param password
     * @return
     *     returns at.wea5.geocaching.webserviceproxy.User
     */
    @WebMethod(operationName = "AuthenticateUser", action = "http://GeoCaching.Services/AuthenticateUser")
    @WebResult(name = "AuthenticateUserResult", targetNamespace = "http://GeoCaching.Services/")
    @RequestWrapper(localName = "AuthenticateUser", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.AuthenticateUser")
    @ResponseWrapper(localName = "AuthenticateUserResponse", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.AuthenticateUserResponse")
    public User authenticateUser(
        @WebParam(name = "username", targetNamespace = "http://GeoCaching.Services/")
        String username,
        @WebParam(name = "password", targetNamespace = "http://GeoCaching.Services/")
        String password);

    /**
     * 
     * @param filter
     * @return
     *     returns at.wea5.geocaching.webserviceproxy.ArrayOfCache
     */
    @WebMethod(operationName = "GetFilteredCacheList", action = "http://GeoCaching.Services/GetFilteredCacheList")
    @WebResult(name = "GetFilteredCacheListResult", targetNamespace = "http://GeoCaching.Services/")
    @RequestWrapper(localName = "GetFilteredCacheList", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetFilteredCacheList")
    @ResponseWrapper(localName = "GetFilteredCacheListResponse", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetFilteredCacheListResponse")
    public ArrayOfCache getFilteredCacheList(
        @WebParam(name = "filter", targetNamespace = "http://GeoCaching.Services/")
        DataFilter filter);

    /**
     * 
     * @return
     *     returns at.wea5.geocaching.webserviceproxy.DataFilter
     */
    @WebMethod(operationName = "ComputeDefaultFilter", action = "http://GeoCaching.Services/ComputeDefaultFilter")
    @WebResult(name = "ComputeDefaultFilterResult", targetNamespace = "http://GeoCaching.Services/")
    @RequestWrapper(localName = "ComputeDefaultFilter", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.ComputeDefaultFilter")
    @ResponseWrapper(localName = "ComputeDefaultFilterResponse", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.ComputeDefaultFilterResponse")
    public DataFilter computeDefaultFilter();

    /**
     * 
     * @return
     *     returns at.wea5.geocaching.webserviceproxy.ArrayOfString
     */
    @WebMethod(operationName = "GetCacheSizeList", action = "http://GeoCaching.Services/GetCacheSizeList")
    @WebResult(name = "GetCacheSizeListResult", targetNamespace = "http://GeoCaching.Services/")
    @RequestWrapper(localName = "GetCacheSizeList", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetCacheSizeList")
    @ResponseWrapper(localName = "GetCacheSizeListResponse", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetCacheSizeListResponse")
    public ArrayOfString getCacheSizeList();

    /**
     * 
     * @param cacheId
     * @return
     *     returns at.wea5.geocaching.webserviceproxy.CacheDetails
     */
    @WebMethod(operationName = "GetDetailedCache", action = "http://GeoCaching.Services/GetDetailedCache")
    @WebResult(name = "GetDetailedCacheResult", targetNamespace = "http://GeoCaching.Services/")
    @RequestWrapper(localName = "GetDetailedCache", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetDetailedCache")
    @ResponseWrapper(localName = "GetDetailedCacheResponse", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetDetailedCacheResponse")
    public CacheDetails getDetailedCache(
        @WebParam(name = "cacheId", targetNamespace = "http://GeoCaching.Services/")
        int cacheId);

    /**
     * 
     * @param logEntry
     * @param requestingUser
     * @return
     *     returns boolean
     */
    @WebMethod(operationName = "AddLogEntryForCache", action = "http://GeoCaching.Services/AddLogEntryForCache")
    @WebResult(name = "AddLogEntryForCacheResult", targetNamespace = "http://GeoCaching.Services/")
    @RequestWrapper(localName = "AddLogEntryForCache", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.AddLogEntryForCache")
    @ResponseWrapper(localName = "AddLogEntryForCacheResponse", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.AddLogEntryForCacheResponse")
    public boolean addLogEntryForCache(
        @WebParam(name = "requestingUser", targetNamespace = "http://GeoCaching.Services/")
        User requestingUser,
        @WebParam(name = "logEntry", targetNamespace = "http://GeoCaching.Services/")
        LogEntry logEntry);

    /**
     * 
     * @param requestingUser
     * @param rating
     * @return
     *     returns boolean
     */
    @WebMethod(operationName = "AddRatingForCache", action = "http://GeoCaching.Services/AddRatingForCache")
    @WebResult(name = "AddRatingForCacheResult", targetNamespace = "http://GeoCaching.Services/")
    @RequestWrapper(localName = "AddRatingForCache", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.AddRatingForCache")
    @ResponseWrapper(localName = "AddRatingForCacheResponse", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.AddRatingForCacheResponse")
    public boolean addRatingForCache(
        @WebParam(name = "requestingUser", targetNamespace = "http://GeoCaching.Services/")
        User requestingUser,
        @WebParam(name = "rating", targetNamespace = "http://GeoCaching.Services/")
        Rating rating);

    /**
     * 
     * @param filter
     * @return
     *     returns at.wea5.geocaching.webserviceproxy.StatisticDataset
     */
    @WebMethod(operationName = "GetUserByFoundCaches", action = "http://GeoCaching.Services/GetUserByFoundCaches")
    @WebResult(name = "GetUserByFoundCachesResult", targetNamespace = "http://GeoCaching.Services/")
    @RequestWrapper(localName = "GetUserByFoundCaches", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetUserByFoundCaches")
    @ResponseWrapper(localName = "GetUserByFoundCachesResponse", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetUserByFoundCachesResponse")
    public StatisticDataset getUserByFoundCaches(
        @WebParam(name = "filter", targetNamespace = "http://GeoCaching.Services/")
        DataFilter filter);

    /**
     * 
     * @param filter
     * @return
     *     returns at.wea5.geocaching.webserviceproxy.StatisticDataset
     */
    @WebMethod(operationName = "GetUserByHiddenCaches", action = "http://GeoCaching.Services/GetUserByHiddenCaches")
    @WebResult(name = "GetUserByHiddenCachesResult", targetNamespace = "http://GeoCaching.Services/")
    @RequestWrapper(localName = "GetUserByHiddenCaches", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetUserByHiddenCaches")
    @ResponseWrapper(localName = "GetUserByHiddenCachesResponse", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetUserByHiddenCachesResponse")
    public StatisticDataset getUserByHiddenCaches(
        @WebParam(name = "filter", targetNamespace = "http://GeoCaching.Services/")
        DataFilter filter);

    /**
     * 
     * @param filter
     * @return
     *     returns at.wea5.geocaching.webserviceproxy.StatisticDataset
     */
    @WebMethod(operationName = "GetBestRatedCache", action = "http://GeoCaching.Services/GetBestRatedCache")
    @WebResult(name = "GetBestRatedCacheResult", targetNamespace = "http://GeoCaching.Services/")
    @RequestWrapper(localName = "GetBestRatedCache", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetBestRatedCache")
    @ResponseWrapper(localName = "GetBestRatedCacheResponse", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetBestRatedCacheResponse")
    public StatisticDataset getBestRatedCache(
        @WebParam(name = "filter", targetNamespace = "http://GeoCaching.Services/")
        DataFilter filter);

    /**
     * 
     * @param filter
     * @return
     *     returns at.wea5.geocaching.webserviceproxy.StatisticDataset
     */
    @WebMethod(operationName = "GetMostLoggedCaches", action = "http://GeoCaching.Services/GetMostLoggedCaches")
    @WebResult(name = "GetMostLoggedCachesResult", targetNamespace = "http://GeoCaching.Services/")
    @RequestWrapper(localName = "GetMostLoggedCaches", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetMostLoggedCaches")
    @ResponseWrapper(localName = "GetMostLoggedCachesResponse", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetMostLoggedCachesResponse")
    public StatisticDataset getMostLoggedCaches(
        @WebParam(name = "filter", targetNamespace = "http://GeoCaching.Services/")
        DataFilter filter);

    /**
     * 
     * @param filter
     * @return
     *     returns at.wea5.geocaching.webserviceproxy.StatisticDataset
     */
    @WebMethod(operationName = "GetCacheDistributionBySize", action = "http://GeoCaching.Services/GetCacheDistributionBySize")
    @WebResult(name = "GetCacheDistributionBySizeResult", targetNamespace = "http://GeoCaching.Services/")
    @RequestWrapper(localName = "GetCacheDistributionBySize", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetCacheDistributionBySize")
    @ResponseWrapper(localName = "GetCacheDistributionBySizeResponse", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetCacheDistributionBySizeResponse")
    public StatisticDataset getCacheDistributionBySize(
        @WebParam(name = "filter", targetNamespace = "http://GeoCaching.Services/")
        DataFilter filter);

    /**
     * 
     * @param filter
     * @return
     *     returns at.wea5.geocaching.webserviceproxy.StatisticDataset
     */
    @WebMethod(operationName = "GetCacheDistributionByCacheDifficulty", action = "http://GeoCaching.Services/GetCacheDistributionByCacheDifficulty")
    @WebResult(name = "GetCacheDistributionByCacheDifficultyResult", targetNamespace = "http://GeoCaching.Services/")
    @RequestWrapper(localName = "GetCacheDistributionByCacheDifficulty", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetCacheDistributionByCacheDifficulty")
    @ResponseWrapper(localName = "GetCacheDistributionByCacheDifficultyResponse", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetCacheDistributionByCacheDifficultyResponse")
    public StatisticDataset getCacheDistributionByCacheDifficulty(
        @WebParam(name = "filter", targetNamespace = "http://GeoCaching.Services/")
        DataFilter filter);

    /**
     * 
     * @param filter
     * @return
     *     returns at.wea5.geocaching.webserviceproxy.StatisticDataset
     */
    @WebMethod(operationName = "GetCacheDistributionByTerrainDifficulty", action = "http://GeoCaching.Services/GetCacheDistributionByTerrainDifficulty")
    @WebResult(name = "GetCacheDistributionByTerrainDifficultyResult", targetNamespace = "http://GeoCaching.Services/")
    @RequestWrapper(localName = "GetCacheDistributionByTerrainDifficulty", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetCacheDistributionByTerrainDifficulty")
    @ResponseWrapper(localName = "GetCacheDistributionByTerrainDifficultyResponse", targetNamespace = "http://GeoCaching.Services/", className = "at.wea5.geocaching.webserviceproxy.GetCacheDistributionByTerrainDifficultyResponse")
    public StatisticDataset getCacheDistributionByTerrainDifficulty(
        @WebParam(name = "filter", targetNamespace = "http://GeoCaching.Services/")
        DataFilter filter);

}
