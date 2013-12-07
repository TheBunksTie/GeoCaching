using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface IRatingDao {
        // read
        List<Rating> GetAll();
        Rating GetByPrimaryKey(int id);

        List<Rating> GetRatingsForCache(int cacheId);
        List<Rating> GetRatingsForUser(int userId);
        List<Rating> GetRatingsForCacheAndUser(int cacheId, int userId);

        double GetAverageCacheRating(int cacheId);

        // write
        int Insert(Rating rating);
        // no update needed because user can add as many ratings for each cache as he wants
        // no deletion allowed
    }
}