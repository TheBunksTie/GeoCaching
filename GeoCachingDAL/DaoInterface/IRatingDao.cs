using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface IRatingDao {
        // read
        IList<Rating> GetAll();
        Rating GetByPrimaryKey(int cacheId, string creator);

        IList<Rating> GetRatingsForCache(int cacheId);
        IList<Rating> GetRatingsForUser(string userName);

        double GetAverageCacheRating(int cacheId);

        // write
        bool Insert(Rating rating);
        bool Update(Rating rating);
        // no deletion allowed
    }
}