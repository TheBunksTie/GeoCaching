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
                authenticationManager = GeoCachingBLFactory.GetAuthentificationManager();
                cacheManager = GeoCachingBLFactory.GetCacheManager();
                statisticsManager = GeoCachingBLFactory.GetStatisticsManager();
            }
            catch {
                // in case of any exception when creating "connection" to backend, set flag 
                // and do not provide access from outside
                backendRunning = false;
            }
        }

        // -------------------------------- Authentication -------------------------------

        [WebMethod]
        public User AuthenticateUser(string username, string password) {
            return backendRunning ? authenticationManager.AuthenticateUser(username, password, false) : null;
        }

        // ------------------------------------- Caches ----------------------------------

        [WebMethod]
        public List<Cache> GetFilteredCacheList(DataFilter filter) {
            return backendRunning ? cacheManager.GetFilteredCacheList(filter) : null;
        }

        [WebMethod]
        public DataFilter ComputeDefaultFilter() {
            return backendRunning ? cacheManager.GetDefaultFilter() : null;
        }

        [WebMethod]
        public List<string> GetCacheSizeList() {
            return backendRunning ? cacheManager.GetCacheSizeList() : null;
        }

        [WebMethod]
        public CacheDetails GetDetailedCache(int cacheId) {
            return backendRunning
                ? new CacheDetails {
                    Cache = cacheManager.GetCacheById(cacheId),
                    Images = cacheManager.GetImagesForCache(cacheId),
                    LogEntries = cacheManager.GetLogEntriesforCache(cacheId),
                    Rating = cacheManager.GetAverageRatingForCache(cacheId)
                }
                : null;
        }

        // ----------------------------------- Logentries ---------------------------------

        [WebMethod]
        public bool AddLogEntryForCache(User requestingUser, LogEntry logEntry) {
            return backendRunning && authenticationManager.ReauthenticateUser(requestingUser) &&
                   cacheManager.AddLogEntryForCache(logEntry);
        }

        // ------------------------------------ Ratings  ----------------------------------

        [WebMethod]
        public bool AddRatingForCache(User requestingUser, Rating rating) {
            return backendRunning && authenticationManager.ReauthenticateUser(requestingUser) &&
                   cacheManager.AddRatingForCache(rating);
        }
        
        // ------------------------------ Statistics: Top Lists ----------------------------
        
        [WebMethod]
        public StatisticDataset GetUserByFoundCaches(DataFilter filter) {
            return backendRunning ? statisticsManager.GetFoundCachesByUser(filter) : null;
        }

        [WebMethod]
        public StatisticDataset GetUserByHiddenCaches(DataFilter filter) {
            return backendRunning ? statisticsManager.GetHiddenCachesByUser(filter) : null;
        }

        [WebMethod]
        public StatisticDataset GetBestRatedCache ( DataFilter filter ) {
            return backendRunning ? statisticsManager.GetBestRatedCaches(filter) : null;
        }

        [WebMethod]
        public StatisticDataset GetMostLoggedCaches ( DataFilter filter ) {
            return backendRunning ? statisticsManager.GetMostLoggedCaches(filter) : null;
        }

        // ---------------------------- Statistics: Distributions --------------------------

        [WebMethod]
        public StatisticDataset GetCacheDistributionBySize(DataFilter filter) {
            return backendRunning ? statisticsManager.GetCacheDistributionBySize(filter) : null;
        }

        [WebMethod]
        public StatisticDataset GetCacheDistributionByCacheDifficulty(DataFilter filter) {
            return backendRunning ? statisticsManager.GetCacheDistributionByCacheDifficulty(filter) : null;
        }

        [WebMethod]
        public StatisticDataset GetCacheDistributionByTerrainDifficulty(DataFilter filter) {
            return backendRunning ? statisticsManager.GetCacheDistributionByTerrainDifficulty(filter) : null;
        }       
    }
}