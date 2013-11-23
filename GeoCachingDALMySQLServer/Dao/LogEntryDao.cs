﻿using System;
using System.Collections.Generic;
using System.Data;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public class LogEntryDao : AbstractDao, ILogEntryDao {
        public LogEntryDao(IDatabase database) : base(database) {        
        }

        public IList<LogEntry> GetAll() {
            return GetLogEntryListFor(database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorName, l.creationDate, l.found, l.comment " +
                "FROM cache_log l;"));
        }

        public LogEntry GetById(int id) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorName, l.creationDate, l.found, l.comment " +
                "FROM cache_log l " +
                "WHERE l.id = @id;");
            database.DefineParameter(cmd, "id", DbType.Int32, id);

            IList<LogEntry> list = GetLogEntryListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;
        }

        public IList<LogEntry> GetLogEntriesForCacheAndUser ( int cacheId, string creator ) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorName, l.creationDate, l.found, l.comment " +
                "FROM cache_log l " +
                "WHERE cacheId = @cacheId AND creatorName = @creatorName;");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);
            database.DefineParameter(cmd, "creatorName", DbType.String, creator);

            return GetLogEntryListFor(cmd);
        }

        public IList<LogEntry> GetLogEntriesForCache(int cacheId) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorName, l.creationDate, l.found, l.comment " +
                "FROM cache_log l " +
                "WHERE l.cacheId = @cacheId;");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);

            return GetLogEntryListFor(cmd);
        }

        public IList<LogEntry> GetLogentriesForUser(string userName) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT l.id, l.cacheId, l.creatorName, l.creationDate, l.found, l.comment " +
                "FROM cache_log l " +
                "WHERE l.creatorName = @creatorName;");
            database.DefineParameter(cmd, "creatorName", DbType.String, userName);

            return GetLogEntryListFor(cmd);
        }

        public int Insert(LogEntry entry) {
            IDbCommand cmd = database.CreateCommand(
                "INSERT INTO cache_log (cacheId, creatorName, creationDate, found, comment) " +
                "VALUES (@cacheId, @creatorName, @creationDate, @found, @comment);");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, entry.CacheId);
            database.DefineParameter(cmd, "creatorName", DbType.String, entry.Creator);
            database.DefineParameter(cmd, "creationDate", DbType.Date, entry.CreationDate);
            database.DefineParameter(cmd, "found", DbType.Boolean, entry.IsFound);
            database.DefineParameter(cmd, "comment", DbType.String, entry.Comment);

            if (database.ExecuteNonQuery(cmd) == 1) {
                // retrieve id of just generated database entry and store in in cache
                IDbCommand idCmd = database.CreateCommand("SELECT last_insert_id()");
                entry.Id = ( int ) database.ExecuteScalarQuery<long>(idCmd);
            }
            else {
                entry.Id = -1;
            }

            return entry.Id;
        }

        private IList<LogEntry> GetLogEntryListFor (IDbCommand cmd ) {
            using ( IDataReader reader = database.ExecuteReader(cmd) ) {
                IList<LogEntry> entries = new List<LogEntry>();

                while ( reader.Read() ) {
                    entries.Add(new LogEntry(
                        ( int ) reader["id"],
                        ( int ) reader["cacheId"],
                        ( string ) reader["creatorName"],
                        DateTime.Parse(reader["creationDate"].ToString()),
                        ( bool ) reader["found"],
                        ( string ) reader["comment"]));
                }
                return entries;
            }
        }
    }
}