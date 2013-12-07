using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.CacheManager {
    public interface ICacheManager {
        bool CreateCache(Cache c);
        bool EditCache(Cache c);
        bool DeleteCache(Cache c);
    }
}