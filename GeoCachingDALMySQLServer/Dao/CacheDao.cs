﻿using System;
using System.Collections.Generic;
using System.Data;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public class CacheDao : AbstractDaoBase, ICacheDao {
        public CacheDao(IDatabase database) : base(database) {}

        public Cache GetById(int id) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, lt.sizeDescription, c.ownerId, c.latitude, c.longitude, c.description " +
                "FROM cache c INNER JOIN lt_cache_size lt ON c.sizeCode = lt.id " +
                "WHERE c.id = @id;");

            database.DefineParameter(cmd, "id", DbType.Int32, id);

            IList<Cache> list = GetCacheListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;
        }

        public List<Cache> GetAll() {
            return GetCacheListFor(database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, lt.sizeDescription, c.ownerId, c.latitude, c.longitude, c.description " +
                "FROM cache c INNER JOIN lt_cache_size lt ON c.sizeCode = lt.id " +
                "ORDER BY c.creationDate ASC;"));
        }

        public List<string> GetAllCacheSizes() {
            var sizes = new List<string>();

            IDbCommand cmd = database.CreateCommand("SELECT sizeDescription FROM lt_cache_size;");

            using (IDataReader reader = database.ExecuteReader(cmd)) {
                while (reader.Read()) {
                    sizes.Add(( string ) reader["sizeDescription"]);
                }
            }
            return sizes;
        }

        public List<Cache> GetByOwner(int id) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, lt.sizeDescription, c.ownerId, c.latitude, c.longitude, c.description " +
                "FROM cache c INNER JOIN lt_cache_size lt ON c.sizeCode = lt.id " +
                "WHERE c.ownerId = @ownerId;");

            database.DefineParameter(cmd, "ownerId", DbType.Int32, id);
            return GetCacheListFor(cmd);
        }

        public List<Cache> GetCachesByCriterium(FilterCriterium criterium, FilterOperation operation, string value) {
            // map filterCriterium to column name of cache table
            string columnName = criterium.ToColumnName();
            string operationName = operation.ToOperationName();

            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, lt.sizeDescription,c.ownerId, c.latitude, c.longitude, c.description " +
                "FROM cache c INNER JOIN lt_cache_size lt ON c.sizeCode = lt.id " +
                "WHERE c." + columnName + " " + operationName + " @value;");


            if (criterium.ToDataType() == DbType.Double) {
                database.DefineParameter(cmd, "value", DbType.Double, Double.Parse(value));
            }
            else if (criterium.ToDataType() == DbType.Int32) {
                database.DefineParameter(cmd, "value", DbType.Int32, Int32.Parse(value));
            }
            return GetCacheListFor(cmd);
        }

        public DateTime GetEarliestCacheCreationDate() {
            return GetDateTimeFor(database.CreateCommand(
                "SELECT c.creationDate FROM cache c ORDER BY c.creationDate ASC LIMIT 1;"));
        }

        public DateTime GetLatestCacheCreationDate() {
            return GetDateTimeFor(database.CreateCommand(
                "SELECT c.creationDate FROM cache c ORDER BY c.creationDate DESC LIMIT 1;"));
        }

        public GeoPosition GetLowestCachePosition() {
            var geoPosition = new GeoPosition {
                Latitude =
                    database.ExecuteScalarDoubleQuery(
                        database.CreateCommand("SELECT c.latitude FROM cache c ORDER BY c.latitude ASC LIMIT 1;")),
                Longitude =
                    database.ExecuteScalarDoubleQuery(
                        database.CreateCommand("SELECT c.longitude FROM cache c ORDER BY c.longitude ASC LIMIT 1;"))
            };

            return geoPosition;
        }

        public GeoPosition GetHighestCachePosition() {
            var geoPosition = new GeoPosition {
                Latitude =
                    database.ExecuteScalarDoubleQuery(
                        database.CreateCommand("SELECT c.latitude FROM cache c ORDER BY c.latitude DESC LIMIT 1;")),
                Longitude =
                    database.ExecuteScalarDoubleQuery(
                        database.CreateCommand("SELECT c.longitude FROM cache c ORDER BY c.longitude DESC LIMIT 1;"))
            };

            return geoPosition;
        }

        public List<StatisticData> GetHiddenCachesCountPerUser(DateTime fromDate, DateTime toDate, GeoPosition fromPosition, GeoPosition toPosition) {
            IDbCommand cmd = database.CreateCommand(
              "SELECT u.name, s.count " +
              "FROM user u INNER JOIN " +
                  "(SELECT ownerId, COUNT(id) AS count " +
                   "FROM cache " +
                   "WHERE (creationDate >= @begin AND creationDate <= @end) AND " +
                         "(latitude >= @latFrom AND latitude <= @latTo) AND " +
                         "(longitude >= @longFrom AND longitude <= @longTo) " +
                   "GROUP BY ownerId) AS s " +
              "ON u.id = s.ownerId " +
              "ORDER BY s.count DESC;");

            AddLimitationParameters(cmd, fromDate, toDate, fromPosition, toPosition);
            return GetStatisticsDataFor(cmd);
        }

        public List<StatisticData> GetCacheDistributionBySize(DateTime fromDate, DateTime toDate, GeoPosition fromPosition, GeoPosition toPosition) {
            IDbCommand cmd = database.CreateCommand("SELECT cs.sizeDescription, " +
                                                     "(c.count / " +
                                                         "(SELECT COUNT(id) " +
                                                          "FROM cache " +
                                                          "WHERE (creationDate >= @begin AND creationDate <= @end) AND " +
                                                                "(latitude >= @latFrom AND latitude <= @latTo) AND " +
                                                                "(longitude >= @longFrom AND longitude <= @longTo)) * 100) AS percent " +
                                                     "FROM lt_cache_size cs INNER JOIN " +
                                                           "(SELECT sizeCode, COUNT(sizeCode) AS count " +
                                                            "FROM cache " +                                                           
                                                            "WHERE (creationDate >= @begin AND creationDate <= @end) AND " +
                                                                  "(latitude >= @latFrom AND latitude <= @latTo) AND " +
                                                                  "(longitude >= @longFrom AND longitude <= @longTo) " +
                                                            "GROUP BY sizeCode) AS c " +
                                                     "ON cs.id = c.sizeCode " +
                                                     "ORDER BY percent DESC;");
            AddLimitationParameters(cmd, fromDate, toDate, fromPosition, toPosition);
            return GetStatisticsDataFor(cmd);
        }

        public List<StatisticData> GetCacheDistributionByCacheDifficulty(DateTime fromDate, DateTime toDate, GeoPosition fromPosition, GeoPosition toPosition) {
            IDbCommand cmd = database.CreateCommand("SELECT difficultyCache, " +
                                                     "(COUNT(difficultyCache) / " +
                                                         "(SELECT COUNT(id) " +
                                                          "FROM cache " +
                                                          "WHERE (creationDate >= @begin AND creationDate <= @end) AND " +
                                                                "(latitude >= @latFrom AND latitude <= @latTo) AND " +
                                                                "(longitude >= @longFrom AND longitude <= @longTo)) * 100) AS percent " +
                                                     "FROM cache " +
                                                     "WHERE (creationDate >= @begin AND creationDate <= @end) AND " +
                                                           "(latitude >= @latFrom AND latitude <= @latTo) AND " +
                                                           "(longitude >= @longFrom AND longitude <= @longTo) " +
                                                     "GROUP BY difficultyCache " +                                                     
                                                     "ORDER BY percent DESC;");

            AddLimitationParameters(cmd, fromDate, toDate, fromPosition, toPosition);
            return GetStatisticsDataFor(cmd);
        }

        public List<StatisticData> GetCacheDistributionByTerrainDifficulty(DateTime fromDate, DateTime toDate, GeoPosition fromPosition, GeoPosition toPosition) {
            IDbCommand cmd = database.CreateCommand("SELECT difficultyTerrain, " +
                                                     "(COUNT(difficultyCache) / " +
                                                         "(SELECT COUNT(id) " +
                                                          "FROM cache " +
                                                          "WHERE (creationDate >= @begin AND creationDate <= @end) AND " +
                                                                "(latitude >= @latFrom AND latitude <= @latTo) AND " +
                                                                "(longitude >= @longFrom AND longitude <= @longTo)) * 100) AS percent " +
                                                     "FROM cache " +
                                                     "WHERE (creationDate >= @begin AND creationDate <= @end) AND " +
                                                           "(latitude >= @latFrom AND latitude <= @latTo) AND " +
                                                           "(longitude >= @longFrom AND longitude <= @longTo) " +
                                                    "GROUP BY difficultyTerrain " +
                                                    "ORDER BY percent DESC;");

            AddLimitationParameters(cmd, fromDate, toDate, fromPosition, toPosition);
            return GetStatisticsDataFor(cmd);
        }

        public List<Cache> GetByAverageRating ( double rating, FilterOperation operation) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, lt.sizeDescription, c.ownerId, c.latitude, c.longitude, c.description " +
                "FROM cache c INNER JOIN lt_cache_size lt ON c.sizeCode = lt.id " +
                "INNER JOIN " +
                "(SELECT cacheId, AVG(grade) AS grade " +
                "FROM cache_rating GROUP BY cacheId " +
                "HAVING grade " + operation.ToOperationName() + " @grade) AS r " +
                "ON c.id = r.cacheId;");

            database.DefineParameter(cmd, "grade", DbType.Double, rating);

            return GetCacheListFor(cmd);
        }


        public List<Cache> GetInRegionCreatedBetween ( DateTime begin, DateTime end, GeoPosition @from, GeoPosition to ) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, lt.sizeDescription, c.ownerId, c.latitude, c.longitude, c.description " +
                "FROM cache c INNER JOIN lt_cache_size lt ON c.sizeCode = lt.id " +
                "WHERE (c.creationDate >= @begin AND c.creationDate <= @end) AND " +
                "(c.latitude >= @latFrom AND c.latitude <= @latTo) AND " +
                "(c.longitude >= @longFrom AND c.longitude <= @longTo );");

            database.DefineParameter(cmd, "begin", DbType.DateTime, begin);
            database.DefineParameter(cmd, "end", DbType.DateTime, end);
            database.DefineParameter(cmd, "latFrom", DbType.Double, from.Latitude);
            database.DefineParameter(cmd, "latTo", DbType.Double, to.Latitude);
            database.DefineParameter(cmd, "longFrom", DbType.Double, from.Longitude);
            database.DefineParameter(cmd, "longTo", DbType.Double, to.Longitude);

            return GetCacheListFor(cmd);
        }

        public List<Cache> GetInRegionFoundBetween ( DateTime begin, DateTime end, GeoPosition @from, GeoPosition to ) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, lt.sizeDescription, c.ownerId, c.latitude, c.longitude, c.description " +
                "FROM cache c INNER JOIN lt_cache_size lt ON c.sizeCode = lt.id " +
                "WHERE c.id IN " +
                "(SELECT cacheId FROM cache_log l " +
                "WHERE (l.creationDate >= @begin AND l.creationDate <= @end)) AND " +
                "(c.latitude >= @latFrom AND c.latitude <= @latTo) AND " +
                "(c.longitude >= @longFrom AND c.longitude <= @longTo );");

            database.DefineParameter(cmd, "begin", DbType.DateTime, begin);
            database.DefineParameter(cmd, "end", DbType.DateTime, end);
            database.DefineParameter(cmd, "latFrom", DbType.Double, from.Latitude);
            database.DefineParameter(cmd, "latTo", DbType.Double, to.Latitude);
            database.DefineParameter(cmd, "longFrom", DbType.Double, from.Longitude);
            database.DefineParameter(cmd, "longTo", DbType.Double, to.Longitude);

            return GetCacheListFor(cmd);
        }

        public List<Cache> GetInRegionRatedBetween ( DateTime begin, DateTime end, GeoPosition @from, GeoPosition to ) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, lt.sizeDescription, c.ownerId, c.latitude, c.longitude, c.description " +
                "FROM cache c INNER JOIN lt_cache_size lt ON c.sizeCode = lt.id " +
                "WHERE c.id IN " +
                "(SELECT cacheId FROM cache_rating r " +
                "WHERE (r.creationDate >= @begin AND r.creationDate <= @end)) AND " +
                "(c.latitude >= @latFrom AND c.latitude <= @latTo) AND " +
                "(c.longitude >= @longFrom AND c.longitude <= @longTo);");

            database.DefineParameter(cmd, "begin", DbType.DateTime, begin);
            database.DefineParameter(cmd, "end", DbType.DateTime, end);
            database.DefineParameter(cmd, "latFrom", DbType.Double, from.Latitude);
            database.DefineParameter(cmd, "latTo", DbType.Double, to.Latitude);
            database.DefineParameter(cmd, "longFrom", DbType.Double, from.Longitude);
            database.DefineParameter(cmd, "longTo", DbType.Double, to.Longitude);

            return GetCacheListFor(cmd);
        }

        public int Insert(Cache cache) {
            int sizeCode = GetIdForSize(cache.Size);

            IDbCommand cmd = database.CreateCommand(
                "INSERT INTO cache (name, creationDate, difficultyCache, difficultyTerrain, sizeCode, ownerId, latitude, longitude, description) " +
                "VALUES (@name, @creationDate, @difficultyCache, @difficultyTerrain, @sizeCode, @ownerId, @latitude, @longitude, @description);");

            database.DefineParameter(cmd, "name", DbType.String, cache.Name);
            database.DefineParameter(cmd, "creationDate", DbType.Date, cache.CreationDate);
            database.DefineParameter(cmd, "difficultyCache", DbType.Double, cache.CacheDifficulty);
            database.DefineParameter(cmd, "difficultyTerrain", DbType.Double, cache.TerrainDifficulty);
            database.DefineParameter(cmd, "sizeCode", DbType.Int32, sizeCode);
            database.DefineParameter(cmd, "ownerId", DbType.Int32, cache.OwnerId);
            database.DefineParameter(cmd, "latitude", DbType.Double, cache.Position.Latitude);
            database.DefineParameter(cmd, "longitude", DbType.Double, cache.Position.Longitude);
            database.DefineParameter(cmd, "description", DbType.String, cache.Description);

            if (database.ExecuteNonQuery(cmd) == 1) {
                // retrieve id of just generated database entry and store in in cache
                IDbCommand idCmd = database.CreateCommand("SELECT last_insert_id();");
                cache.Id = ( int ) database.ExecuteScalarQuery<long>(idCmd);
            }
            else {
                cache.Id = -1;
            }
            return cache.Id;
        }

        public bool Update(Cache cache) {
            int sizeCode = GetIdForSize(cache.Size);

            IDbCommand cmd = database.CreateCommand(
                "UPDATE cache SET name = @name, creationDate = @creationDate, difficultyCache = @difficultyCache, difficultyTerrain = @difficultyTerrain, " +
                "sizeCode = @sizeCode, ownerId = @ownerId, latitude = @latitude, longitude = @longitude, description = @description WHERE id = @id;");

            database.DefineParameter(cmd, "name", DbType.String, cache.Name);
            database.DefineParameter(cmd, "creationDate", DbType.Date, cache.CreationDate);
            database.DefineParameter(cmd, "difficultyCache", DbType.Double, cache.CacheDifficulty);
            database.DefineParameter(cmd, "difficultyTerrain", DbType.Double, cache.TerrainDifficulty);
            database.DefineParameter(cmd, "sizeCode", DbType.Int32, sizeCode);
            database.DefineParameter(cmd, "ownerId", DbType.Int32, cache.OwnerId);
            database.DefineParameter(cmd, "latitude", DbType.Double, cache.Position.Latitude);
            database.DefineParameter(cmd, "longitude", DbType.Double, cache.Position.Longitude);
            database.DefineParameter(cmd, "description", DbType.String, cache.Description);
            // primary key
            database.DefineParameter(cmd, "id", DbType.Int32, cache.Id);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        public bool Delete(int cacheId) {
            IDbCommand cmd = database.CreateCommand("DELETE FROM cache WHERE id = @id;");
            database.DefineParameter(cmd, "id", DbType.Int32, cacheId);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        private int GetIdForSize(string size) {
            IDbCommand cmd = database.CreateCommand("SELECT id FROM lt_cache_size WHERE sizeDescription = @size");

            database.DefineParameter(cmd, "size", DbType.String, size);

            return database.ExecuteScalarQuery<int>(cmd);
        }

        private List<Cache> GetCacheListFor(IDbCommand cmd) {
            using (IDataReader reader = database.ExecuteReader(cmd)) {
                var caches = new List<Cache>();

                while (reader.Read()) {
                    caches.Add(new Cache {
                        Id = ( int ) reader["id"],
                        Name = reader["name"].ToString(),
                        CreationDate = DateTime.Parse(reader["creationDate"].ToString()),
                        CacheDifficulty = ( double ) reader["difficultyCache"],
                        TerrainDifficulty = ( double ) reader["difficultyTerrain"],
                        Size = reader["sizeDescription"].ToString(),
                        OwnerId = ( int ) reader["ownerId"],
                        Position = new GeoPosition(( double ) reader["latitude"], ( double ) reader["longitude"]),
                        Description = reader["description"].ToString()
                    });
                }
                return caches;
            }
        }

        private DateTime GetDateTimeFor(IDbCommand cmd) {
            using (IDataReader reader = database.ExecuteReader(cmd)) {
                if (reader.Read()) {
                    return DateTime.Parse(reader["creationDate"].ToString());
                }
            }
            throw new Exception();
        }

        private void AddLimitationParameters ( IDbCommand cmd, DateTime fromDate, DateTime toDate, GeoPosition fromPosition, GeoPosition toPosition ) {
            database.DefineParameter(cmd, "begin", DbType.DateTime, fromDate);
            database.DefineParameter(cmd, "end", DbType.DateTime, toDate);
            database.DefineParameter(cmd, "latFrom", DbType.Double, fromPosition.Latitude);
            database.DefineParameter(cmd, "latTo", DbType.Double, toPosition.Latitude);
            database.DefineParameter(cmd, "longFrom", DbType.Double, fromPosition.Longitude);
            database.DefineParameter(cmd, "longTo", DbType.Double, toPosition.Longitude);
        }
    }
}