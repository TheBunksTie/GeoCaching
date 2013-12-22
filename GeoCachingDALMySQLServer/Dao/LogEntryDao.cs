using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public class LogEntryDao : AbstractDaoBase, ILogEntryDao {
        public LogEntryDao(IDatabase database) : base(database) {}

        public List<LogEntry> GetAll() {
            return GetLogEntryListFor(database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorId, l.creationDate, l.found, l.comment " +
                "FROM cache_log l;"));
        }

        public LogEntry GetById(int id) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorId, l.creationDate, l.found, l.comment " +
                "FROM cache_log l " +
                "WHERE l.id = @id;");
            database.DefineParameter(cmd, "id", DbType.Int32, id);

            IList<LogEntry> list = GetLogEntryListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;
        }

        public List<LogEntry> GetLogEntriesForCacheAndUser(int cacheId, int userId) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorId, l.creationDate, l.found, l.comment " +
                "FROM cache_log l " +
                "WHERE cacheId = @cacheId AND creatorId = @creatorId;");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);
            database.DefineParameter(cmd, "creatorId", DbType.Int32, userId);

            return GetLogEntryListFor(cmd);
        }

        public List<StatisticData> GetFoundCachesCountPerUser(DateTime begin, DateTime end, GeoPosition from, GeoPosition to) {
            
            IDbCommand cmd = database.CreateCommand(
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

            database.DefineParameter(cmd, "begin", DbType.DateTime, begin);
            database.DefineParameter(cmd, "end", DbType.DateTime, end);
            database.DefineParameter(cmd, "latFrom", DbType.Double, from.Latitude);
            database.DefineParameter(cmd, "latTo", DbType.Double, to.Latitude);
            database.DefineParameter(cmd, "longFrom", DbType.Double, from.Longitude);
            database.DefineParameter(cmd, "longTo", DbType.Double, to.Longitude);
            
            return GetStatisticsDataFor(cmd);
        }

        public List<LogEntry> GetLogEntriesForCache(int cacheId) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorId, l.creationDate, l.found, l.comment " +
                "FROM cache_log l " +
                "WHERE l.cacheId = @cacheId;");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);

            return GetLogEntryListFor(cmd);
        }

        public List<LogEntry> GetLogentriesForUser(int userId) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorId, l.creationDate, l.found, l.comment " +
                "FROM cache_log l " +
                "WHERE l.creatorId = @creatorId;");
            database.DefineParameter(cmd, "creatorId", DbType.Int32, userId);

            return GetLogEntryListFor(cmd);
        }

        public int Insert(LogEntry entry) {
            IDbCommand cmd = database.CreateCommand(
                "INSERT INTO cache_log (cacheId, creatorId, creationDate, found, comment) " +
                "VALUES (@cacheId, @creatorId, @creationDate, @found, @comment);");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, entry.CacheId);
            database.DefineParameter(cmd, "creatorId", DbType.Int32, entry.CreatorId);
            database.DefineParameter(cmd, "creationDate", DbType.Date, entry.CreationDate);
            database.DefineParameter(cmd, "found", DbType.Boolean, entry.IsFound);
            database.DefineParameter(cmd, "comment", DbType.String, entry.Comment);

            if (database.ExecuteNonQuery(cmd) == 1) {
                // retrieve id of just generated database entry and store in in cache
                IDbCommand idCmd = database.CreateCommand("SELECT last_insert_id();");
                entry.Id = ( int ) database.ExecuteScalarQuery<long>(idCmd);
            }
            else {
                entry.Id = -1;
            }

            return entry.Id;
        }

        private List<LogEntry> GetLogEntryListFor(IDbCommand cmd) {
            using (IDataReader reader = database.ExecuteReader(cmd)) {
                var entries = new List<LogEntry>();

                while (reader.Read()) {
                    entries.Add(new LogEntry(
                        ( int ) reader["id"],
                        ( int ) reader["cacheId"],
                        ( int ) reader["creatorId"],
                        DateTime.Parse(reader["creationDate"].ToString()),
                        ( bool ) reader["found"],
                        reader["comment"].ToString()));
                }
                return entries;
            }
        }
    }
}