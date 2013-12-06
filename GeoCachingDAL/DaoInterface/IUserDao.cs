using System;
using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface IUserDao {
        // read
        List<User> GetAll();
        User GetByName(string name);
        User GetById(int id);

        List<String> GetAllUserRoles();

        // write
        bool Insert(User user);
        bool Update(User user);
        bool Delete(string userName);
        bool DeleteById(int id);
    }
}