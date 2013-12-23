using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.MySQLServer;

namespace GeoCachingTest {
    public abstract class AbstractTest {
        protected const string ConnectionString =
            "server=localhost;Uid=geocaching;Password=geocaching;Persist Security Info=False;database=geocachingtest";

        protected IDatabase Database = new Database(ConnectionString, @"D:\fh\05_WiSe13\SWK\UE\geocaching\src\GeoCaching\GeoCachingDALMySQLServer\images");     
    }
}