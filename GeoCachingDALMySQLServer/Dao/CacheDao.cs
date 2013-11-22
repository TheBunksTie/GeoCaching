﻿using System;
using System.Collections.Generic;
using System.Data;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public class CacheDao : AbstractDao, ICacheDao {
        public CacheDao(IDatabase database) : base(database) {}

        public Cache GetById(int id) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, c.sizeId,  c.ownerName, c.latitude, c.longitude, c.description " +
                "FROM cache c WHERE c.id = @id");

            database.DefineParameter(cmd, "id", DbType.Int32, id);

            IList<Cache> list = GetCacheListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;
        }

        public IList<Cache> GetAll() {
            return GetCacheListFor(database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, c.sizeId,  c.ownerName, c.latitude, c.longitude, c.description " +
                "FROM cache c;"));
        }

        public IList<Cache> GetByOwner(string userName) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, c.sizeId,  c.ownerName, c.latitude, c.longitude, c.description " +
                "FROM cache c WHERE c.ownerName = @owner;");

            database.DefineParameter(cmd, "owner", DbType.String, userName);
            return GetCacheListFor(cmd);
        }

        public IList<Cache> GetByCacheDifficulty(double diffictulty, FilterCriterium criterium) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, c.sizeId,  c.ownerName, c.latitude, c.longitude, c.description " +
                "FROM cache c WHERE difficultyCache" + FilterCriteriumToString(criterium) + "@difficulty;");

            database.DefineParameter(cmd, "difficulty", DbType.Double, diffictulty);
            return GetCacheListFor(cmd);
        }

        public IList<Cache> GetByTerrainDifficulty(double diffictulty, FilterCriterium criterium) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, c.sizeId,  c.ownerName, c.latitude, c.longitude, c.description " +
                "FROM cache c WHERE difficultyTerrain" + FilterCriteriumToString(criterium) + "@difficulty;");

            database.DefineParameter(cmd, "difficulty", DbType.Double, diffictulty);
            return GetCacheListFor(cmd);
        }

        public IList<Cache> GetByAverageRating(double rating, FilterCriterium criterium) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, c.sizeId, c.ownerName, c.latitude, c.longitude, c.description " +
                "FROM cache c INNER JOIN " +
                "(SELECT cacheId, AVG(grade) AS grade " +
                "FROM cache_rating GROUP BY cacheId " +
                "HAVING grade" + FilterCriteriumToString(criterium) + "@grade) AS r " +
                "ON c.id = r.cacheId;");

            database.DefineParameter(cmd, "grade", DbType.Double, rating);

            return GetCacheListFor(cmd);
        }

        public IList<Cache> GetBySize(CacheSize size, FilterCriterium criterium) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, c.sizeId,  c.ownerName, c.latitude, c.longitude, c.description " +
                "FROM cache c WHERE sizeId " + FilterCriteriumToString(criterium) + "@size;");

            database.DefineParameter(cmd, "size", DbType.Int32, ( int ) size);
            return GetCacheListFor(cmd);
        }

        public IList<Cache> GetInRegionCreatedBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, c.sizeId,  c.ownerName, c.latitude, c.longitude, c.description " +
                "FROM cache c WHERE (c.creationDate >= @begin AND c.creationDate <= @end) AND " +
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

        public IList<Cache> GetInRegionFoundBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, c.sizeId,  c.ownerName, c.latitude, c.longitude, c.description " +
                "FROM cache c " +
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

        public IList<Cache> GetInRegionRatedBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT c.id, c.name, c.creationDate, c.difficultyCache, c.difficultyTerrain, c.sizeId, c.ownerName, c.latitude, c.longitude, c.description " +
                "FROM cache c " +
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
            IDbCommand cmd = database.CreateCommand(
                "INSERT INTO cache (name, creationDate, difficultyCache, difficultyTerrain, sizeId, ownerName, latitude, longitude, description) " +
                "VALUES (@name, @creationDate, @difficultyCache, @difficultyTerrain, @sizeId, @ownerName, @latitude, @longitude, @description)");

            database.DefineParameter(cmd, "name", DbType.String, cache.Name);
            database.DefineParameter(cmd, "creationDate", DbType.Date, cache.CreationDate);
            database.DefineParameter(cmd, "difficultyCache", DbType.Double, cache.DifficultyCache);
            database.DefineParameter(cmd, "difficultyTerrain", DbType.Double, cache.DifficultyTerrain);
            database.DefineParameter(cmd, "sizeId", DbType.Int32, cache.GetCacheSizeAsId());
            database.DefineParameter(cmd, "ownerName", DbType.String, cache.Owner);
            database.DefineParameter(cmd, "latitude", DbType.Double, cache.Position.Latitude);
            database.DefineParameter(cmd, "longitude", DbType.Double, cache.Position.Longitude);
            database.DefineParameter(cmd, "description", DbType.String, cache.Description);

            if (database.ExecuteNonQuery(cmd) == 1) {
                // retrieve id of just generated database entry and store in in cache
                IDbCommand idCmd = database.CreateCommand("SELECT last_insert_id()");
                cache.Id = ( int ) database.ExecuteScalarQuery<long>(idCmd);
            }
            else {
                cache.Id = -1;
            }
            return cache.Id;
        }

        public bool Update(Cache cache) {
            IDbCommand cmd = database.CreateCommand(
                "UPDATE cache SET name = @name, creationDate = @creationDate, difficultyCache = @difficultyCache, difficultyTerrain = @difficultyTerrain, " +
                "sizeId = @sizeId, ownerName = @ownerName, latitude = @latitude, longitude = @longitude, description = @description WHERE id = @id");

            database.DefineParameter(cmd, "name", DbType.String, cache.Name);
            database.DefineParameter(cmd, "creationDate", DbType.Date, cache.CreationDate);
            database.DefineParameter(cmd, "difficultyCache", DbType.Double, cache.DifficultyCache);
            database.DefineParameter(cmd, "difficultyTerrain", DbType.Double, cache.DifficultyTerrain);
            database.DefineParameter(cmd, "sizeId", DbType.Int32, cache.GetCacheSizeAsId());
            database.DefineParameter(cmd, "ownerName", DbType.String, cache.Owner);
            database.DefineParameter(cmd, "latitude", DbType.Double, cache.Position.Latitude);
            database.DefineParameter(cmd, "longitude", DbType.Double, cache.Position.Longitude);
            database.DefineParameter(cmd, "description", DbType.String, cache.Description);
            // primary key
            database.DefineParameter(cmd, "id", DbType.Int32, cache.Id);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        public bool Delete(int cacheId) {
            IDbCommand cmd = database.CreateCommand("DELETE FROM cache WHERE id = @id");
            database.DefineParameter(cmd, "id", DbType.Int32, cacheId);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        private IList<Cache> GetCacheListFor(IDbCommand cmd) {
            using (IDataReader reader = database.ExecuteReader(cmd)) {
                IList<Cache> caches = new List<Cache>();

                while (reader.Read()) {
                    caches.Add(new Cache(
                        ( int ) reader["id"],
                        ( string ) reader["name"],
                        DateTime.Parse(reader["creationDate"].ToString()),
                        ( double ) reader["difficultyCache"],
                        ( double ) reader["difficultyTerrain"],
                        ( int ) reader["sizeId"],
                        ( string ) reader["ownerName"],
                        new GeoPosition(( double ) reader["latitude"], ( double ) reader["longitude"]),
                        ( string ) reader["description"]));
                }
                return caches;
            }
        }
    }
}