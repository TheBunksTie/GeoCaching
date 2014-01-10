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

        private IAuthenticationManager authenticationManager = GeoCachingBLFactory.GetAuthentificationManager();
        private ICacheManager cacheManager = GeoCachingBLFactory.GetCacheManager();


        // -------------------------------- Authentication -------------------------------

        [WebMethod]
        public bool AuthenticateUser(string username, string password) {
            return (authenticationManager.AuthenticateUser(username, password, false) != null);
        }

        // -------------------------------- Authentication -------------------------------

        [WebMethod]
        public List<Cache> GetAllCaches() {
            return cacheManager.GetCacheList();
        }
    }
}