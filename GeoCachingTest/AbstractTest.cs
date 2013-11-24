using Swk5.GeoCaching.DAL.Common;

namespace GeoCachingTest {
    public abstract class AbstractTest {
        protected const string ConnectionString =
            "server=localhost;Uid=geocaching;Password=geocaching;Persist Security Info=False;database=geocachingtest";

        protected IDatabase Database = null;
    }
}