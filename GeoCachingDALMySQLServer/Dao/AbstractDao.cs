using Swk5.GeoCaching.DAL.Common;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public abstract class AbstractDao {
        protected readonly IDatabase database;

        protected AbstractDao(IDatabase database) {
            this.database = database;
        }

        protected string FilterCriteriumToString(FilterCriterium c) {
            if (c == FilterCriterium.Below) {
                return " < ";
            }
            if (c == FilterCriterium.BelowEquals) {
                return " <= ";
            }
            if (c == FilterCriterium.Above) {
                return " > ";
            }
            if (c == FilterCriterium.AboveEquals) {
                return " >= ";
            }
            if (c == FilterCriterium.Exact) {
                return " = ";
            }
            return null;
        }
    }
}