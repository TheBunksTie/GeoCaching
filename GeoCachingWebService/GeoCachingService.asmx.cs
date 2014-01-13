using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.AuthenticationManager;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.DomainModel;

namespace GeoCaching.Services {
    /// <summary>
    ///     Summary description for GeoCachingService
    /// </summary>
    [WebService(Namespace = "http://GeoCaching.Services/")]

    // conformity specification to BasicProfile 1
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GeoCachingService : WebService {
        private readonly IAuthenticationManager authenticationManager = GeoCachingBLFactory.GetAuthentificationManager();
        private readonly ICacheManager cacheManager = GeoCachingBLFactory.GetCacheManager();

        // -------------------------------- Authentication -------------------------------

        [WebMethod]
        public User AuthenticateUser(string username, string password) {
            return authenticationManager.AuthenticateUser(username, password, false);
        }

        // ------------------------------------- Caches ----------------------------------

        [WebMethod]
        public List<Cache> GetAllCaches() {
            return cacheManager.GetCacheList();
        }

        [WebMethod]
        public List<string> GetCacheSizeList() {
            return cacheManager.GetCacheSizeList();
        }

        [WebMethod]
        public List<Cache> FindCachesByCacheDifficulty(FilterOperation operation, string difficulty) {
            return cacheManager.GetFilteredCacheList(FilterCriterium.CacheDifficulty, operation, difficulty);
        }

        [WebMethod]
        public List<Cache> FindCachesByTerrainDifficulty(FilterOperation operation, string difficulty) {
            return cacheManager.GetFilteredCacheList(FilterCriterium.TerrainDifficulty, operation, difficulty);
        }

        [WebMethod]
        public List<Cache> FindCachesBySize(FilterOperation operation, string size) {
            return cacheManager.GetFilteredCacheList(FilterCriterium.Size, operation, size);
        }

        [WebMethod]
        public List<Image> GetAllImagesForCache(int cacheId) {
            return cacheManager.GetImagesForCache(cacheId);
        }

        // ----------------------------------- Logentries ---------------------------------

        [WebMethod]
        public List<LogEntry> GetLogEntriesForCache(int cacheId) {
            return cacheManager.GetLogEntriesforCache(cacheId);
        }

        [WebMethod]
        public bool AddLogEntryForCache(User user, LogEntry logEntry) {
            // TODO only if user is valid (name + pw hash) = reauthenticate
            return cacheManager.AddLogEntryForCache(logEntry);
        }

        [WebMethod]
        public bool AddRatingForCache(User user, Rating rating) {
            // TODO only if user is valid (name + pw hash) = reauthenticate
            return cacheManager.AddRatingForCache(rating);
        }

        // ------------------------------------ Ratings  ----------------------------------
        [WebMethod]
        public double GetRatingForCache(int cacheId) {
            return cacheManager.GetAverageRatingForCache(cacheId);
        }
    }
}