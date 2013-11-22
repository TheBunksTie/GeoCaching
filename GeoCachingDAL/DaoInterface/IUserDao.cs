using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface IUserDao {
        
        // read
        IList<User> GetAll();
        User GetByName(string name);

        // for stastistics
        IDictionary<User, int> GetNrCreatedCachesForUsers();
        IDictionary<User, int> GetNrFoundCachesForUsers();

        // write
        bool Insert(User user);
        bool Update(User user);
        bool Delete(string userName);
    }
}
