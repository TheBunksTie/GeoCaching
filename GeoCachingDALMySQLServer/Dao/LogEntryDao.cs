using System;
using System.Collections.Generic;
using System.Data;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public class LogEntryDao : AbstractDaoBase, ILogEntryDao {
        public LogEntryDao(IDatabase database) : base(database) {}

        public List<LogEntry> GetAll() {
            return GetLogEntryListFor(Database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorId, u.name, l.creationDate, l.found, l.comment " +
                "FROM cache_log l INNER JOIN user u ON l.creatorId = user.id;"));
        }

        public LogEntry GetById(int id) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorId, l.creationDate, l.found, l.comment " +
                "FROM cache_log l " +
                "WHERE l.id = @id;");
            Database.DefineParameter(cmd, "id", DbType.Int32, id);

            IList<LogEntry> list = GetLogEntryListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;
        }

        public List<LogEntry> GetLogEntriesForCacheAndUser(int cacheId, int userId) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorId, u.name, l.creationDate, l.found, l.comment " +
                "FROM cache_log l INNER JOIN user u ON l.creatorId = u.id " +
                "WHERE cacheId = @cacheId AND creatorId = @creatorId;");
            Database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);
            Database.DefineParameter(cmd, "creatorId", DbType.Int32, userId);

            return GetLogEntryListFor(cmd);
        }

        public List<StatisticData> GetFoundCachesCountPerUser(DataFilter filter) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT u.name, s.count " +
                "FROM user u INNER JOIN " +
                    "(SELECT creatorId, COUNT(cacheId) AS count " +
                     "FROM cache_log " +
                     "WHERE (found = 1) AND " +
                           "(creationDate >= @begin AND creationDate <= @end) AND " +
                            "cacheId IN (SELECT id " +
                                        "FROM cache " +
                                        "WHERE (latitude >= @latFrom AND latitude <= @latTo) AND " +
                                              "(longitude >= @longFrom AND longitude <= @longTo ))" +
                     "GROUP BY creatorId) as s " +
                "ON u.id = s.creatorId " +
                "ORDER BY s.count DESC;");

            AddLimitationParameters(cmd, filter);
            return GetStatisticsDataFor(cmd);
        }

        public List<StatisticData> GetMostLoggedCaches(DataFilter filter) {
            IDbCommand cmd = Database.CreateCommand("SELECT c.name, l.cnt " +
                                                    "FROM cache c INNER JOIN " +
                                                        "(SELECT cacheId, COUNT(id) AS cnt " +
                                                         "FROM cache_log " +
                                                         "GROUP BY cacheId) AS l " +
                                                    "ON c.id = l.cacheId " +
                                                    "WHERE (c.creationDate >= @begin AND c.creationDate <= @end) AND " +
                                                          "(c.latitude >= @latFrom AND c.latitude <= @latTo) AND " +
                                                          "(c.longitude >= @longFrom AND c.longitude <= @longTo) " +
                                                    "ORDER BY l.cnt DESC;");
            AddLimitationParameters(cmd, filter);
            return GetStatisticsDataFor(cmd);
        }

        public List<LogEntry> GetLogEntriesForCache(int cacheId) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorId, u.name, l.creationDate, l.found, l.comment " +
                "FROM cache_log l INNER JOIN user u ON l.creatorId = u.id " +
                "WHERE l.cacheId = @cacheId " +
                "ORDER BY l.creationDate DESC;");
            Database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);

            return GetLogEntryListFor(cmd);
        }

        public List<LogEntry> GetLogentriesForUser(int userId) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorId, u.name, l.creationDate, l.found, l.comment " +
                "FROM cache_log l INNER JOIN user u ON l.creatorId = u.id " +
                "WHERE l.creatorId = @creatorId;");
            Database.DefineParameter(cmd, "creatorId", DbType.Int32, userId);

            return GetLogEntryListFor(cmd);
        }

        public int Insert(LogEntry entry) {
            IDbCommand cmd = Database.CreateCommand(
                "INSERT INTO cache_log (cacheId, creatorId, creationDate, found, comment) " +
                "VALUES (@cacheId, @creatorId, @creationDate, @found, @comment);");
            Database.DefineParameter(cmd, "cacheId", DbType.Int32, entry.CacheId);
            Database.DefineParameter(cmd, "creatorId", DbType.Int32, entry.CreatorId);
            Database.DefineParameter(cmd, "creationDate", DbType.Date, entry.CreationDate);
            Database.DefineParameter(cmd, "found", DbType.Boolean, entry.IsFound);
            Database.DefineParameter(cmd, "comment", DbType.String, entry.Comment);

            if (Database.ExecuteNonQuery(cmd) == 1) {
                // retrieve id of just generated database entry and store in in cache
                IDbCommand idCmd = Database.CreateCommand("SELECT last_insert_id();");
                entry.Id = ( int ) Database.ExecuteScalarQuery<long>(idCmd);
            }
            else {
                entry.Id = -1;
            }

            return entry.Id;
        }

        private List<LogEntry> GetLogEntryListFor(IDbCommand cmd) {
            using (IDataReader reader = Database.ExecuteReader(cmd)) {
                var entries = new List<LogEntry>();

                while (reader.Read()) {
                    entries.Add(new LogEntry {
                        Id = ( int ) reader["id"],
                        CacheId = ( int ) reader["cacheId"],
                        CreatorId = ( int ) reader["creatorId"],
                        CreatorName = reader["name"].ToString(),
                        CreationDate = DateTime.Parse(reader["creationDate"].ToString()),
                        IsFound = ( bool ) reader["found"],
                        Comment = reader["comment"].ToString()});
                }
                return entries;
            }
        }
    }
}