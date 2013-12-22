using Swk5.GeoCaching.DAL.Common;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public abstract class AbstractDaoBase {
        protected readonly IDatabase database;

        protected AbstractDaoBase(IDatabase database) {
            this.database = database;
        }
    }
}