using Swk5.GeoCaching.BusinessLogic.AuthentificationManager;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.BusinessLogic.StatisticsManager;
using Swk5.GeoCaching.BusinessLogic.UserManager;

namespace Swk5.GeoCaching.BusinessLogic {
    public static class GeoCachingBLFactory {
        private static IAuthentificationManager authentificationManager;
        private static ICacheManager cacheManager;
        private static IStatisticsManager statisticsManager;
        private static IUserManager userManager;

        public static IAuthentificationManager GetAuthentificationManager() {
            return authentificationManager ?? (authentificationManager = new AuthentificationManager.AuthentificationManager());
        }

        public static ICacheManager GetCacheManager() {
            return cacheManager ?? (cacheManager = new CacheManager.CacheManager());
        }

        public static IStatisticsManager GetStatisticsManager() {
            return statisticsManager ?? (statisticsManager = new StatisticsManager.StatisticsManager());
        }

        public static IUserManager GetUserManager() {
            return userManager ?? (userManager = new UserManager.UserManager());
        }
    }
}