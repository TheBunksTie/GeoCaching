using System;
using System.Collections.Generic;
using System.Data;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public class CacheDao : AbstractDaoBase, ICacheDao {
        public CacheDao(IDatabase database) : base(database) {}

        public Cache GetById(int id) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, lt.sizeDescription, c.ownerId, c.latitude, c.longitude, c.description " +
                "FROM cache c INNER JOIN lt_cache_size lt ON c.sizeCode = lt.id " +
                "WHERE c.id = @id;");

            Database.DefineParameter(cmd, "id", DbType.Int32, id);

            IList<Cache> list = GetCacheListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;
        }

        public List<string> GetAllCacheSizes() {
            var sizes = new List<string>();

            IDbCommand cmd = Database.CreateCommand("SELECT sizeDescription FROM lt_cache_size;");

            using (IDataReader reader = Database.ExecuteReader(cmd)) {
                while (reader.Read()) {
                    sizes.Add(( string ) reader["sizeDescription"]);
                }
            }
            return sizes;
        }
 
        public List<Cache> GetByOwner(int id) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, lt.sizeDescription, c.ownerId, c.latitude, c.longitude, c.description " +
                "FROM cache c INNER JOIN lt_cache_size lt ON c.sizeCode = lt.id " +
                "WHERE c.ownerId = @ownerId;");

            Database.DefineParameter(cmd, "ownerId", DbType.Int32, id);
            return GetCacheListFor(cmd);
        }
       
        public DateTime GetEarliestCacheCreationDate() {
            return GetDateTimeFor(Database.CreateCommand(
                "SELECT c.creationDate FROM cache c ORDER BY c.creationDate ASC LIMIT 1;"));
        }

        public DateTime GetLatestCacheCreationDate() {
            return GetDateTimeFor(Database.CreateCommand(
                "SELECT c.creationDate FROM cache c ORDER BY c.creationDate DESC LIMIT 1;"));
        }

        public GeoPosition GetLowestCachePosition() {
            var geoPosition = new GeoPosition {
                Latitude = Database.ExecuteScalarDoubleQuery(
                        Database.CreateCommand("SELECT c.latitude FROM cache c ORDER BY c.latitude ASC LIMIT 1;")),
                Longitude = Database.ExecuteScalarDoubleQuery(
                        Database.CreateCommand("SELECT c.longitude FROM cache c ORDER BY c.longitude ASC LIMIT 1;"))
            };

            return geoPosition;
        }

        public GeoPosition GetHighestCachePosition() {
            var geoPosition = new GeoPosition {
                Latitude =
                    Database.ExecuteScalarDoubleQuery(
                        Database.CreateCommand("SELECT c.latitude FROM cache c ORDER BY c.latitude DESC LIMIT 1;")),
                Longitude =
                    Database.ExecuteScalarDoubleQuery(
                        Database.CreateCommand("SELECT c.longitude FROM cache c ORDER BY c.longitude DESC LIMIT 1;"))
            };

            return geoPosition;
        }

        public List<StatisticData> GetHiddenCachesCountPerUser(DataFilter filter) {
            IDbCommand cmd = Database.CreateCommand(
              "SELECT u.name, s.count " +
              "FROM user u INNER JOIN " +
                  "(SELECT ownerId, COUNT(id) AS count " +
                   "FROM cache " +
                   "WHERE (creationDate >= @begin AND creationDate <= @end) AND " +
                         "(latitude >= @latFrom AND latitude <= @latTo) AND " +
                         "(longitude >= @longFrom AND longitude <= @longTo) " +
                   "GROUP BY ownerId) AS s " +
              "ON u.id = s.ownerId " +
              "ORDER BY s.count DESC " +
              "LIMIT @limit;");

            AddGeneralFilterParameters(cmd, filter);

            // add special filter parameters
            Database.DefineParameter(cmd, "limit", DbType.Int32, filter.Limit);
            return GetStatisticsDataFor(cmd);
        }

        public List<StatisticData> GetCacheDistributionBySize(DataFilter filter)  {
            IDbCommand cmd = Database.CreateCommand("SELECT cs.sizeDescription, " +
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
            AddGeneralFilterParameters(cmd, filter);
            return GetStatisticsDataFor(cmd);
        }

        public List<StatisticData> GetCacheDistributionByCacheDifficulty(DataFilter filter) {
            IDbCommand cmd = Database.CreateCommand("SELECT difficultyCache, " +
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

            AddGeneralFilterParameters(cmd, filter);
            return GetStatisticsDataFor(cmd);
        }

        public List<StatisticData> GetCacheDistributionByTerrainDifficulty(DataFilter filter) {
            IDbCommand cmd = Database.CreateCommand("SELECT difficultyTerrain, " +
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

            AddGeneralFilterParameters(cmd, filter);
            return GetStatisticsDataFor(cmd);
        }

        public List<StatisticData> GetBestRatedCaches(DataFilter filter) {
            IDbCommand cmd = Database.CreateCommand("SELECT c.name, r.grade " +
                                                   "FROM cache c INNER JOIN " +
                                                       "(SELECT cacheId, AVG(grade) AS grade " +
                                                        "FROM cache_rating " +
                                                        "GROUP BY cacheId) AS r " +
                                                   "ON c.id = r.cacheId " +
                                                   "WHERE (c.creationDate >= @begin AND c.creationDate <= @end) AND " +
                                                         "(c.latitude >= @latFrom AND c.latitude <= @latTo) AND " +
                                                         "(c.longitude >= @longFrom AND c.longitude <= @longTo) " +
                                                   "ORDER BY r.grade DESC " +
                                                   "LIMIT @limit;");

            AddGeneralFilterParameters(cmd, filter);
            
            // add special filter parameters
            Database.DefineParameter(cmd, "limit", DbType.Int32, filter.Limit);
            return GetStatisticsDataFor(cmd);
        }

        public int Insert(Cache cache) {
            int sizeCode = GetIdForSize(cache.Size);

            IDbCommand cmd = Database.CreateCommand(
                "INSERT INTO cache (name, creationDate, difficultyCache, difficultyTerrain, sizeCode, ownerId, latitude, longitude, description) " +
                "VALUES (@name, @creationDate, @difficultyCache, @difficultyTerrain, @sizeCode, @ownerId, @latitude, @longitude, @description);");

            Database.DefineParameter(cmd, "name", DbType.String, cache.Name);
            Database.DefineParameter(cmd, "creationDate", DbType.Date, cache.CreationDate);
            Database.DefineParameter(cmd, "difficultyCache", DbType.Double, cache.CacheDifficulty);
            Database.DefineParameter(cmd, "difficultyTerrain", DbType.Double, cache.TerrainDifficulty);
            Database.DefineParameter(cmd, "sizeCode", DbType.Int32, sizeCode);
            Database.DefineParameter(cmd, "ownerId", DbType.Int32, cache.OwnerId);
            Database.DefineParameter(cmd, "latitude", DbType.Double, cache.Position.Latitude);
            Database.DefineParameter(cmd, "longitude", DbType.Double, cache.Position.Longitude);
            Database.DefineParameter(cmd, "description", DbType.String, cache.Description);

            if (Database.ExecuteNonQuery(cmd) == 1) {
                // retrieve id of just generated database entry and store in in cache
                IDbCommand idCmd = Database.CreateCommand("SELECT last_insert_id();");
                cache.Id = ( int ) Database.ExecuteScalarQuery<long>(idCmd);
            }
            else {
                cache.Id = -1;
            }
            return cache.Id;
        }

        public bool Update(Cache cache) {
            int sizeCode = GetIdForSize(cache.Size);

            IDbCommand cmd = Database.CreateCommand(
                "UPDATE cache SET name = @name, creationDate = @creationDate, difficultyCache = @difficultyCache, difficultyTerrain = @difficultyTerrain, " +
                "sizeCode = @sizeCode, ownerId = @ownerId, latitude = @latitude, longitude = @longitude, description = @description WHERE id = @id;");

            Database.DefineParameter(cmd, "name", DbType.String, cache.Name);
            Database.DefineParameter(cmd, "creationDate", DbType.Date, cache.CreationDate);
            Database.DefineParameter(cmd, "difficultyCache", DbType.Double, cache.CacheDifficulty);
            Database.DefineParameter(cmd, "difficultyTerrain", DbType.Double, cache.TerrainDifficulty);
            Database.DefineParameter(cmd, "sizeCode", DbType.Int32, sizeCode);
            Database.DefineParameter(cmd, "ownerId", DbType.Int32, cache.OwnerId);
            Database.DefineParameter(cmd, "latitude", DbType.Double, cache.Position.Latitude);
            Database.DefineParameter(cmd, "longitude", DbType.Double, cache.Position.Longitude);
            Database.DefineParameter(cmd, "description", DbType.String, cache.Description);
            // primary key
            Database.DefineParameter(cmd, "id", DbType.Int32, cache.Id);

            return Database.ExecuteNonQuery(cmd) == 1;
        }

        public bool Delete(int cacheId) {
            IDbCommand cmd = Database.CreateCommand("DELETE FROM cache WHERE id = @id;");
            Database.DefineParameter(cmd, "id", DbType.Int32, cacheId);

            return Database.ExecuteNonQuery(cmd) == 1;
        }

        public List<Cache> GetCachesMatchingFilter(DataFilter filter) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, lt.sizeDescription, c.ownerId, c.latitude, c.longitude, c.description " +
                "FROM cache c INNER JOIN lt_cache_size lt ON c.sizeCode = lt.id " +
                "WHERE (c.creationDate >= @begin AND c.creationDate <= @end) AND " +
                "(c.latitude >= @latFrom AND c.latitude <= @latTo) AND " +
                "(c.longitude >= @longFrom AND c.longitude <= @longTo) AND " +
                "(c.sizeCode >= @sizeFrom AND c.sizeCode <= @sizeTo) AND " +
                "(c.difficultyCache >= @cacheDiffFrom AND c.difficultyCache <= @cacheDiffTo) AND " +
                "(c.difficultyTerrain >= @terrainDiffFrom AND c.difficultyTerrain <= @terrainDiffTo) " +
                "ORDER BY c.creationDate ASC;");

            AddGeneralFilterParameters(cmd, filter);

            Database.DefineParameter(cmd, "sizeFrom", DbType.Int32, filter.FromCacheSize);
            Database.DefineParameter(cmd, "sizeTo", DbType.Int32, filter.ToCacheSize);
            Database.DefineParameter(cmd, "cacheDiffFrom", DbType.Double, filter.FromCacheDifficulty);
            Database.DefineParameter(cmd, "cacheDiffTo", DbType.Double, filter.ToCacheDifficulty);
            Database.DefineParameter(cmd, "terrainDiffFrom", DbType.Double, filter.FromTerrainDifficulty);
            Database.DefineParameter(cmd, "terrainDiffTo", DbType.Double, filter.ToTerrainDifficulty);

            return GetCacheListFor(cmd);
        }

        private int GetIdForSize(string size) {
            IDbCommand cmd = Database.CreateCommand("SELECT id FROM lt_cache_size WHERE sizeDescription = @size");

            Database.DefineParameter(cmd, "size", DbType.String, size);

            return Database.ExecuteScalarQuery<int>(cmd);
        }

        private List<Cache> GetCacheListFor(IDbCommand cmd) {
            using (IDataReader reader = Database.ExecuteReader(cmd)) {
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
            using (IDataReader reader = Database.ExecuteReader(cmd)) {
                if (reader.Read()) {
                    return DateTime.Parse(reader["creationDate"].ToString());
                }
            }
            throw new Exception();
        }
    }
}