using System;
using System.Collections.Generic;
using System.Data;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public class RatingDao : AbstractDao, IRatingDao {
        public RatingDao(IDatabase database) : base(database) {}

        public IList<Rating> GetAll() {
            IDbCommand cmd = database.CreateCommand(
                "SELECT r.cacheId, r.creatorName, r.creationDate, r.grade " +
                "FROM cache_rating r;");

            return GetRatingListFor(cmd);
        }

        public Rating GetByPrimaryKey(int cacheId, string creatorName) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT r.cacheId, r.creatorName, r.creationDate, r.grade " +
                "FROM cache_rating r WHERE r.cacheId = @cacheId AND r.creatorName = @creatorName");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);
            database.DefineParameter(cmd, "creatorName", DbType.String, creatorName);

            IList<Rating> list = GetRatingListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;
        }

        public IList<Rating> GetRatingsForCache(int cacheId) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT r.cacheId, r.creatorName, r.creationDate, r.grade " +
                "FROM cache_rating r " +
                "WHERE r.cacheId = @cacheId;");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);

            return GetRatingListFor(cmd);
        }

        public IList<Rating> GetRatingsForUser(string userName) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT r.cacheId, r.creatorName, r.creationDate, r.grade " +
                "FROM cache_rating r " +
                "WHERE r.creatorName = @creatorName;");
            database.DefineParameter(cmd, "creatorName", DbType.String, userName);

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

        public bool Update(Rating rating) {
            IDbCommand cmd = database.CreateCommand(
                "UPDATE cache_rating SET creationDate = @creationDate, grade = @grade " +
                "WHERE cacheId = @cacheId AND creatorName = @creatorName");
            database.DefineParameter(cmd, "creationDate", DbType.Date, rating.CreationDate);
            database.DefineParameter(cmd, "grade", DbType.Int32, rating.Grade);
            // primary key
            database.DefineParameter(cmd, "cacheId", DbType.Int32, rating.CacheId);
            database.DefineParameter(cmd, "creatorName", DbType.String, rating.Creator);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        public bool Insert(Rating rating) {
            IDbCommand cmd = database.CreateCommand(
                "INSERT INTO cache_rating (cacheId, creatorName, creationDate, grade) " +
                "VALUES (@cacheId, @creatorName, @creationDate, @grade)");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, rating.CacheId);
            database.DefineParameter(cmd, "creatorName", DbType.String, rating.Creator);
            database.DefineParameter(cmd, "creationDate", DbType.Date, rating.CreationDate);
            database.DefineParameter(cmd, "grade", DbType.Int32, rating.Grade);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        private IList<Rating> GetRatingListFor(IDbCommand cmd) {
            using (IDataReader reader = database.ExecuteReader(cmd)) {
                IList<Rating> ratings = new List<Rating>();

                while (reader.Read()) {
                    ratings.Add(new Rating(
                        ( int ) reader["cacheId"],
                        ( string ) reader["creatorName"],
                        DateTime.Parse(reader["creationDate"].ToString()),
                        ( int ) reader["grade"]));
                }
                return ratings;
            }
        }
    }
}