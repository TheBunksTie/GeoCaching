using System.Data;

namespace Swk5.GeoCaching.DAL.Common {
    public interface IDatabase {
        IDbCommand CreateCommand(string sql);

        // declares a parameter, sets a value and adds it to the given command  
        void DefineParameter(IDbCommand cmd, string name, DbType type, object value);

        IDataReader ExecuteReader(IDbCommand cmd);
        int ExecuteNonQuery(IDbCommand cmd);
        T ExecuteScalarQuery<T>(IDbCommand cmd);
        double ExecuteScalarDoubleQuery(IDbCommand cmd);
    }
}