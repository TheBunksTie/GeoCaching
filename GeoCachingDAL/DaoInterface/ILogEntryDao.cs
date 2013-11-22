using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface ILogEntryDao {
        // read
        IList<LogEntry> GetAll();
        LogEntry GetByPrimaryKey(int cacheId, string creator);

        IList<LogEntry> GetLogEntriesForCache(int cacheId);
        IList<LogEntry> GetLogentriesForUser(string userName);

        // write
        bool Insert(LogEntry logEntry);
        bool Update(LogEntry logEntry);
        // no deletion allowed
    }
}