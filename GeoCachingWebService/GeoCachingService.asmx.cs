using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.AuthenticationManager;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.BusinessLogic.StatisticsManager;
using Swk5.GeoCaching.DomainModel;

namespace GeoCaching.Services {
    [WebService(Namespace = "http://GeoCaching.Services/")]

    // conformity specification to BasicProfile 1
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class GeoCachingService : WebService {
        private readonly IAuthenticationManager authenticationManager;
        private readonly bool backendRunning;
        private readonly ICacheManager cacheManager;
        private readonly IStatisticsManager statisticsManager;

        public GeoCachingService() {
            try {
                authenticationManager = GeoCachingBLFactory.GetAuthenticationManager();
                cacheManager = GeoCachingBLFactory.GetCacheManager();
                statisticsManager = GeoCachingBLFactory.GetStatisticsManager();
                backendRunning = true;
            }
            catch {
                // in case of any exception when creating "connection" to backend, set flag 
                // and do not provide access from outside
                backendRunning = false;
            }
        }

        // -------------------------------- Service Available ----------------------------
        [WebMethod]
        public bool IsServiceAvailable() {
            return backendRunning;
        }

        // -------------------------------- Authentication -------------------------------

        [WebMethod]
        public User AuthenticateUser(string username, string password) {
            try {
                return backendRunning ? authenticationManager.AuthenticateUser(username, password, LoginMode.GeneralAccessible): null;
            }
            catch {                
                return null;
            }             
        }

        // ------------------------------------- Caches ----------------------------------

        [WebMethod]
        public List<Cache> GetFilteredCacheList(DataFilter filter) {
            try {
                return backendRunning ? cacheManager.GetFilteredCacheList(filter) : null;
            }
            catch {
                return null;
            }
        }

        [WebMethod]
        public DataFilter ComputeDefaultFilter() {
            try {
                return backendRunning ? cacheManager.GetDefaultFilter(): null;
            }
            catch {
                return null;
            }            
        }

        [WebMethod]
        public List<string> GetCacheSizeList() {
            try {
                return backendRunning ? cacheManager.GetCacheSizeList() : null;
            }
            catch {
                return null;
            }            
        }

        [WebMethod]
        public CacheDetails GetDetailedCache(int cacheId) {
            try {
                return backendRunning
                    ? new CacheDetails {
                        Cache = cacheManager.GetCacheById(cacheId),
                        Images = cacheManager.GetImagesForCache(cacheId),
                        LogEntries = cacheManager.GetLogEntriesforCache(cacheId),
                        Rating = cacheManager.GetAverageRatingForCache(cacheId)
                    }
                    : null;
            }
            catch {
                return null;
            }
        }

        // ----------------------------------- Logentries ---------------------------------

        [WebMethod]
        public bool AddLogEntryForCache(User requestingUser, LogEntry logEntry) {
            try {
                return backendRunning && authenticationManager.ReauthenticateUser(requestingUser) &&
                       cacheManager.AddLogEntryForCache(logEntry);
            }
            catch {
                return false;
            }
        }

        // ------------------------------------ Ratings  ----------------------------------

        [WebMethod]
        public bool AddRatingForCache(User requestingUser, Rating rating) {
            try {
                return backendRunning && authenticationManager.ReauthenticateUser(requestingUser) &&
                       cacheManager.AddRatingForCache(rating);
            }
            catch {
                return false;
            }
        }

        // ------------------------------ Statistics: Top Lists ----------------------------

        [WebMethod]
        public StatisticDataset GetUserByFoundCaches(DataFilter filter) {
            try {
                return backendRunning ? statisticsManager.GetUsersByFoundCaches(filter) : null;
            }
            catch  {                
                return null;
            }            
        }

        [WebMethod]
        public StatisticDataset GetUserByHiddenCaches(DataFilter filter) {
            try {
                return backendRunning ? statisticsManager.GetUsersByHiddenCaches(filter) : null;
            }
            catch {
                return null;
            }
            
        }

        [WebMethod]
        public StatisticDataset GetBestRatedCache(DataFilter filter) {
            try {
                return backendRunning ? statisticsManager.GetCachesByRating(filter) : null;
            }
            catch {
                return null;
            }
            
        }

        [WebMethod]
        public StatisticDataset GetMostLoggedCaches(DataFilter filter) {
            try {
                return backendRunning ? statisticsManager.GetCachesByLogCount(filter) : null;
            }
            catch {
                return null;
            }
            
        }

        // ---------------------------- Statistics: Distributions --------------------------

        [WebMethod]
        public StatisticDataset GetCacheDistributionBySize(DataFilter filter) {
            try {
                return backendRunning ? statisticsManager.GetCacheDistributionBySize(filter) : null;
            }
            catch {
                return null;
            }            
        }

        [WebMethod]
        public StatisticDataset GetCacheDistributionByCacheDifficulty(DataFilter filter) {
            try {
                return backendRunning ? statisticsManager.GetCacheDistributionByCacheDifficulty(filter) : null;
            }
            catch {
                return null;
            }
            
        }

        [WebMethod]
        public StatisticDataset GetCacheDistributionByTerrainDifficulty(DataFilter filter) {
            try {
                return backendRunning ? statisticsManager.GetCacheDistributionByTerrainDifficulty(filter) : null;
            }
            catch {
                return null;
            }            
        }
    }
}