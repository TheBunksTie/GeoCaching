using Swk5.GeoCaching.DAL.Common;

namespace Swk5.GeoCaching.BusinessLogic {
    public abstract class AbstractManagerBase {
        protected static readonly IDatabase database = DalFactory.CreateDatabase();
    }
}