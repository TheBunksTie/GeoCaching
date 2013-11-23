using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface ILogEntryDao {
        // read
        IList<LogEntry> GetAll();
        LogEntry GetById(int id);

        IList<LogEntry> GetLogEntriesForCache(int cacheId);
        IList<LogEntry> GetLogentriesForUser(string userName);
        IList<LogEntry> GetLogEntriesForCacheAndUser(int cacheId, string userName);

        // write
        int Insert(LogEntry logEntry);
        // no update needed because user can add as many log entries for each cache as he wants
        // no deletion allowed
    }
}