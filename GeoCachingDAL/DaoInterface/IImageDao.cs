using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface IImageDao {
        // read
        List<Image> GetAllForCache(int cacheId);

        // write
        bool Insert(Image image);
        bool Delete(int id);
    }
}