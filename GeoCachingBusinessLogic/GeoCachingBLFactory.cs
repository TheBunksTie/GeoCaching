using Swk5.GeoCaching.BusinessLogic.AuthenticationManager;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.BusinessLogic.StatisticsManager;
using Swk5.GeoCaching.BusinessLogic.UserManager;

namespace Swk5.GeoCaching.BusinessLogic {
    public static class GeoCachingBLFactory {
        private static IAuthenticationManager authenticationManager;
        private static ICacheManager cacheManager;
        private static IStatisticsManager statisticsManager;
        private static IUserManager userManager;

        public static IAuthenticationManager GetAuthentificationManager() {
            return authenticationManager ?? (authenticationManager = new AuthenticationManager.AuthenticationManager());
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