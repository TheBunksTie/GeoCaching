using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface IRatingDao {
        // read
        IList<Rating> GetAll();
        Rating GetByPrimaryKey(int id);

        IList<Rating> GetRatingsForCache(int cacheId);
        IList<Rating> GetRatingsForUser(int userId);
        IList<Rating> GetRatingsForCacheAndUser(int cacheId, int userId);

        double GetAverageCacheRating(int cacheId);

        // write
        int Insert(Rating rating);
        // no update needed because user can add as many ratings for each cache as he wants
        // no deletion allowed
    }
}