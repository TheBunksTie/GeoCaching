using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface ILogEntryDao {
        // read
        List<LogEntry> GetAll();
        LogEntry GetById(int id);

        List<LogEntry> GetLogEntriesForCache(int cacheId);
        List<LogEntry> GetLogentriesForUser(int userId);
        List<LogEntry> GetLogEntriesForCacheAndUser(int cacheId, int userId);

        // write
        int Insert(LogEntry logEntry);
        // no update needed because user can add as many log entries for each cache as he wants
        // no deletion allowed
    }
}