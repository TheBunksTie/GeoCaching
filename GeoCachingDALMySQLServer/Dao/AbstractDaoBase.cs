using System.Collections.Generic;
using System.Data;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public abstract class AbstractDaoBase {
        protected readonly IDatabase Database;

        protected AbstractDaoBase(IDatabase database) {
            Database = database;
        }

        protected List<StatisticData> GetStatisticsDataFor(IDbCommand cmd) {
            var statistic = new List<StatisticData>();

            using (IDataReader reader = Database.ExecuteReader(cmd)) {
                int i = 1;
                while (reader.Read()) {
                    statistic.Add(new StatisticData {
                        Nr = i.ToString(),
                        Name = reader[0].ToString(),
                        Value = reader[1].ToString()
                    });
                    i++;
                }
            }
            return statistic;
        }

        protected void AddGeneralFilterParameters(IDbCommand cmd, DataFilter filter) {
            Database.DefineParameter(cmd, "begin", DbType.DateTime, filter.FromDate);
            Database.DefineParameter(cmd, "end", DbType.DateTime, filter.ToDate);
            Database.DefineParameter(cmd, "latFrom", DbType.Double, filter.FromPosition.Latitude);
            Database.DefineParameter(cmd, "latTo", DbType.Double, filter.ToPosition.Latitude);
            Database.DefineParameter(cmd, "longFrom", DbType.Double, filter.FromPosition.Longitude);
            Database.DefineParameter(cmd, "longTo", DbType.Double, filter.ToPosition.Longitude);
        }
    }
}