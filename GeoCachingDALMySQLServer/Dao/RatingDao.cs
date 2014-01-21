using System;
using System.Collections.Generic;
using System.Data;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public class RatingDao : AbstractDaoBase, IRatingDao {
        public RatingDao(IDatabase database) : base(database) {}

        public List<Rating> GetAll() {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT r.id, r.cacheId, r.creatorId, r.creationDate, r.grade " +
                "FROM cache_rating r;");

            return GetRatingListFor(cmd);
        }

        public Rating GetByPrimaryKey(int id) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT r.id, r.cacheId, r.creatorId, r.creationDate, r.grade " +
                "FROM cache_rating r WHERE r.id = @id;");
            Database.DefineParameter(cmd, "id", DbType.Int32, id);

            IList<Rating> list = GetRatingListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;
        }

        public List<Rating> GetRatingsForCache(int cacheId) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT r.id, r.cacheId, r.creatorId, r.creationDate, r.grade " +
                "FROM cache_rating r " +
                "WHERE r.cacheId = @cacheId;");
            Database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);

            return GetRatingListFor(cmd);
        }

        public List<Rating> GetRatingsForUser(int userId) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT r.id, r.cacheId, r.creatorId, r.creationDate, r.grade " +
                "FROM cache_rating r " +
                "WHERE r.creatorId = @creatorId;");
            Database.DefineParameter(cmd, "creatorId", DbType.Int32, userId);

            return GetRatingListFor(cmd);
        }

        public List<Rating> GetRatingsForCacheAndUser(int cacheId, int userId) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT r.id, r.cacheId, r.creatorId, r.creationDate, r.grade " +
                "FROM cache_rating r WHERE r.cacheId = @cacheId AND r.creatorId = @creatorId;");
            Database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);
            Database.DefineParameter(cmd, "creatorId", DbType.Int32, userId);

            return GetRatingListFor(cmd);
        }

        public double GetAverageCacheRating(int cacheId) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT r.grade " +
                "FROM " +
                "(SELECT AVG(grade) AS grade " +
                "FROM cache_rating " +
                "WHERE cacheId = @cacheId) AS r;");
            Database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);

            return Database.ExecuteScalarDoubleQuery(cmd);
        }

        public int Insert(Rating rating) {
            IDbCommand cmd = Database.CreateCommand(
                "INSERT INTO cache_rating (cacheId, creatorId, creationDate, grade) " +
                "VALUES (@cacheId, @creatorId, @creationDate, @grade);");
            Database.DefineParameter(cmd, "cacheId", DbType.Int32, rating.CacheId);
            Database.DefineParameter(cmd, "creatorId", DbType.Int32, rating.CreatorId);
            Database.DefineParameter(cmd, "creationDate", DbType.Date, rating.CreationDate);
            Database.DefineParameter(cmd, "grade", DbType.Int32, rating.Grade);

            if (Database.ExecuteNonQuery(cmd) == 1) {
                // retrieve id of just generated database entry and store in in cache
                IDbCommand idCmd = Database.CreateCommand("SELECT last_insert_id();");
                rating.Id = ( int ) Database.ExecuteScalarQuery<long>(idCmd);
            }
            else {
                rating.Id = -1;
            }

            return rating.Id;
        }

        private List<Rating> GetRatingListFor(IDbCommand cmd) {
            using (IDataReader reader = Database.ExecuteReader(cmd)) {
                List<Rating> ratings = new List<Rating>();

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