using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public abstract class AbstractDaoBase {
        protected readonly IDatabase database;

        protected AbstractDaoBase(IDatabase database) {
            this.database = database;
        }

        protected List<StatisticData> GetStatisticsDataFor(IDbCommand cmd) {
            var statistic = new List<StatisticData>();

            using (IDataReader reader = database.ExecuteReader(cmd)) {
                int i = 1;
                while (reader.Read()) {
                    statistic.Add(new StatisticData {
                        Nr = i.ToString(CultureInfo.InvariantCulture),
                        Name = reader[0].ToString(),
                        Value = reader[1].ToString()
                    });
                    i++;
                }
            }
            return statistic;
        }
    }
}