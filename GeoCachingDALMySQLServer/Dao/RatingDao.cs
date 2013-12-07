using System;
using System.Collections.Generic;
using System.Data;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public class RatingDao : AbstractDaoBase, IRatingDao {
        public RatingDao(IDatabase database) : base(database) {}

        public IList<Rating> GetAll() {
            IDbCommand cmd = database.CreateCommand(
                "SELECT r.id, r.cacheId, r.creatorId, r.creationDate, r.grade " +
                "FROM cache_rating r;");

            return GetRatingListFor(cmd);
        }

        public Rating GetByPrimaryKey(int id) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT r.id, r.cacheId, r.creatorId, r.creationDate, r.grade " +
                "FROM cache_rating r WHERE r.id = @id;");
            database.DefineParameter(cmd, "id", DbType.Int32, id);

            IList<Rating> list = GetRatingListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;
        }

        public IList<Rating> GetRatingsForCache(int cacheId) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT r.id, r.cacheId, r.creatorId, r.creationDate, r.grade " +
                "FROM cache_rating r " +
                "WHERE r.cacheId = @cacheId;");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);

            return GetRatingListFor(cmd);
        }

        public IList<Rating> GetRatingsForUser(int userId) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT r.id, r.cacheId, r.creatorId, r.creationDate, r.grade " +
                "FROM cache_rating r " +
                "WHERE r.creatorId = @creatorId;");
            database.DefineParameter(cmd, "creatorId", DbType.Int32, userId);

            return GetRatingListFor(cmd);
        }

        public IList<Rating> GetRatingsForCacheAndUser(int cacheId, int userId) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT r.id, r.cacheId, r.creatorId, r.creationDate, r.grade " +
                "FROM cache_rating r WHERE r.cacheId = @cacheId AND r.creatorId = @creatorId;");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);
            database.DefineParameter(cmd, "creatorId", DbType.Int32, userId);

            return GetRatingListFor(cmd);
        }

        public double GetAverageCacheRating(int cacheId) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT r.grade " +
                "FROM " +
                "(SELECT AVG(grade) AS grade " +
                "FROM cache_rating " +
                "WHERE cacheId = @cacheId) AS r;");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);

            return database.ExecuteScalarDoubleQuery(cmd);
        }

        public int Insert(Rating rating) {
            IDbCommand cmd = database.CreateCommand(
                "INSERT INTO cache_rating (cacheId, creatorId, creationDate, grade) " +
                "VALUES (@cacheId, @creatorId, @creationDate, @grade);");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, rating.CacheId);
            database.DefineParameter(cmd, "creatorId", DbType.Int32, rating.CreatorId);
            database.DefineParameter(cmd, "creationDate", DbType.Date, rating.CreationDate);
            database.DefineParameter(cmd, "grade", DbType.Int32, rating.Grade);

            if (database.ExecuteNonQuery(cmd) == 1) {
                // retrieve id of just generated database entry and store in in cache
                IDbCommand idCmd = database.CreateCommand("SELECT last_insert_id();");
                rating.Id = ( int ) database.ExecuteScalarQuery<long>(idCmd);
            }
            else {
                rating.Id = -1;
            }

            return rating.Id;
        }

        private IList<Rating> GetRatingListFor(IDbCommand cmd) {
            using (IDataReader reader = database.ExecuteReader(cmd)) {
                IList<Rating> ratings = new List<Rating>();

                while (reader.Read()) {
                    ratings.Add(new Rating(
                        ( int ) reader["id"],
                        ( int ) reader["cacheId"],
                        ( int ) reader["creatorId"],
                        DateTime.Parse(reader["creationDate"].ToString()),
                        ( int ) reader["grade"]));
                }
                return ratings;
            }
        }
    }
}