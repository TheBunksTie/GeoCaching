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
        private readonly IAuthenticationManager authenticationManager = GeoCachingBLFactory.GetAuthentificationManager();
        private readonly ICacheManager cacheManager = GeoCachingBLFactory.GetCacheManager();
        private readonly IStatisticsManager statisticsManager = GeoCachingBLFactory.GetStatisticsManager();

        // -------------------------------- Authentication -------------------------------

        [WebMethod]
        public User AuthenticateUser(string username, string password) {
            return authenticationManager.AuthenticateUser(username, password, false);
        }

        // ------------------------------------- Caches ----------------------------------

        [WebMethod]
        public List<Cache> GetFilteredCacheList(DataFilter filter) {
            return cacheManager.GetFilteredCacheList(filter);
        }

        [WebMethod]
        public DataFilter ComputeDefaultFilter() {
            return cacheManager.GetDefaultFilter();
        }

        [WebMethod]
        public List<string> GetCacheSizeList() {
            return cacheManager.GetCacheSizeList();
        }

        [WebMethod]
        public CacheDetails GetDetailedCache(int cacheId) {
            return new CacheDetails {
                Cache = cacheManager.GetCacheById(cacheId),
                Images = cacheManager.GetImagesForCache(cacheId),
                LogEntries = cacheManager.GetLogEntriesforCache(cacheId),
                Rating = cacheManager.GetAverageRatingForCache(cacheId)
            };
        }

        // ----------------------------------- Logentries ---------------------------------

        [WebMethod]
        public bool AddLogEntryForCache(User requestingUser, LogEntry logEntry) {
            return authenticationManager.ReauthenticateUser(requestingUser) &&
                   cacheManager.AddLogEntryForCache(logEntry);
        }

        // ------------------------------------ Ratings  ----------------------------------

        [WebMethod]
        public bool AddRatingForCache(User requestingUser, Rating rating) {
            return authenticationManager.ReauthenticateUser(requestingUser) && cacheManager.AddRatingForCache(rating);
        }

        // ----------------------------------- Statistics ---------------------------------

        [WebMethod]
        public StatisticDataset GetCachesFoundByUser(DataFilter filter) {
            return statisticsManager.GetFoundCachesByUser(filter);
        }

        [WebMethod]
        public StatisticDataset GetCachesHiddenByUser(DataFilter filter) {
            return statisticsManager.GetHiddenCachesByUser(filter);
        }

        [WebMethod]
        public StatisticDataset GetCacheDistributionBySize(DataFilter filter) {
            return statisticsManager.GetCacheDistributionBySize(filter);
        }

        [WebMethod]
        public StatisticDataset GetCacheDistributionByCacheDifficulty(DataFilter filter) {
            return statisticsManager.GetCacheDistributionByCacheDifficulty(filter);
        }

        [WebMethod]
        public StatisticDataset GetCacheDistributionByTerrainDifficulty(DataFilter filter) {
            return statisticsManager.GetCacheDistributionByTerrainDifficulty(filter);
        }

        [WebMethod]
        public StatisticDataset GetBestRatedCache(DataFilter filter) {
            return statisticsManager.GetBestRatedCaches(filter);
        }

        [WebMethod]
        public StatisticDataset GetMostLoggedCaches(DataFilter filter) {
            return statisticsManager.GetMostLoggedCaches(filter);
        }
    }
}