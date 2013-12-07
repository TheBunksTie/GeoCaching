using System.Collections.Generic;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface IImageDao {
        
        // read
        List<string> GetAllForCache(int cacheId);

        // write
        bool Insert(int cacheId, string fileName);
        bool Delete(int cacheId, string fileName);
    }
}