using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DAL.MySQLServer;
using Swk5.GeoCaching.DAL.MySQLServer.Dao;
using Swk5.GeoCaching.DomainModel;

namespace GeoCachingTest {
    [TestClass]
    public class RatingDaoTest {
        private const string ConnectionString = "server=localhost;Uid=geocaching;Password=geocaching;Persist Security Info=False;database=geocachingtest";

        private IDatabase database;
        private IRatingDao target;

        [TestInitialize]
        public void Initialize() {
            database = new Database(ConnectionString);
            target = new RatingDao(database);
        }

        [TestMethod]
        public void GetAllTest() {
            const int ratingsCnt = 6734;
            IList<Rating> actual = target.GetAll();
            Assert.AreEqual(ratingsCnt, actual.Count);
        }

        [TestMethod]
        public void GetAverageCacheRatingTest ( ) {
            // TODO type conversion of double ???
            const int cacheId = 292;
            const double expected = 6.6429;
            double actual = target.GetAverageCacheRating(cacheId);
            Assert.AreEqual(expected, actual, 0.0001);
        }

        [TestMethod]
        public void GetByPrimaryKeyTest() {
            const int cacheId = 172;
            const string creatorName = "Gabriele177";
            Rating expected = new Rating(cacheId, creatorName, new DateTime(2009, 07, 20), 4);
            Rating actual = target.GetByPrimaryKey(cacheId, creatorName);
            
            Assert.AreEqual(expected.CacheId, actual.CacheId);
            Assert.AreEqual(expected.Creator, actual.Creator);
            Assert.AreEqual(expected.CreationDate, actual.CreationDate);
            Assert.AreEqual(expected.Grade, actual.Grade);
        }

        [TestMethod]
        public void GetRatingsForCacheTest() {
            const int cacheId = 403; 
            IList<Rating> expected = new List<Rating>();
            expected.Add(new Rating {Creator = "Jan911"});
            expected.Add(new Rating { Creator = "Jana884" });
            expected.Add(new Rating { Creator = "Karoline687" });
            expected.Add(new Rating { Creator = "Tyrion588" });
                        
            IList<Rating> actual = target.GetRatingsForCache(cacheId);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach ( var entry in expected ) {
                entry.CacheId = cacheId;
                Assert.IsTrue(actual.Contains(entry));
            }
        }

        [TestMethod]
        public void GetRatingsForUserTest() {
            const string userName = "Leia256";
            IList<Rating> expected = new List<Rating>();
            expected.Add(new Rating { CacheId = 8 });
            expected.Add(new Rating { CacheId = 177 });
            expected.Add(new Rating { CacheId = 215 });
            expected.Add(new Rating { CacheId = 250 });
            expected.Add(new Rating { CacheId = 258 });
            expected.Add(new Rating { CacheId = 288 });
            expected.Add(new Rating { CacheId = 293 });
            expected.Add(new Rating { CacheId = 357 });
            expected.Add(new Rating { CacheId = 409 });

            IList<Rating> actual = target.GetRatingsForUser(userName);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach ( var entry in expected ) {
                entry.Creator = userName;
                Assert.IsTrue(actual.Contains(entry));
            }
        }

        [TestMethod]
        public void InsertTest() {
            const int cacheId = 499;
            const string creatorName = "Bunk417";
            Rating toInsert = new Rating(cacheId, creatorName, new DateTime(2009, 12, 6), 10);

            Assert.IsTrue(target.Insert(toInsert));
            Rating expected = target.GetByPrimaryKey(cacheId, creatorName);

            Assert.AreEqual(expected, toInsert);
            Assert.AreEqual(expected.CreationDate, toInsert.CreationDate);
            DeleteRating(cacheId, creatorName); // internal method, not available in DaoInterface
            Assert.IsNull(target.GetByPrimaryKey(cacheId, creatorName));
        }

        [TestMethod]
        public void UpdateTest() {
            const int cacheId = 319;
            const string creatorName = "Sheldon838";
            Rating entry = target.GetByPrimaryKey(cacheId, creatorName);
            int initialGrade = entry.Grade;
            const int updatedGrade = 0;
            entry.Grade = updatedGrade;

            Assert.IsTrue(target.Update(entry));

            entry = target.GetByPrimaryKey(cacheId, creatorName);
            Assert.AreEqual(updatedGrade, entry.Grade);

            entry.Grade = initialGrade;
            Assert.IsTrue(target.Update(entry));

            entry = target.GetByPrimaryKey(cacheId, creatorName);
            Assert.AreEqual(initialGrade, entry.Grade);
        }

        private bool DeleteRating ( int cacheId, string creatorName ) {
            IDbCommand cmd = database.CreateCommand(
           "DELETE FROM cache_rating " +
           "WHERE cacheId = @cacheId AND creatorName = @creatorName;");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);
            database.DefineParameter(cmd, "creatorName", DbType.String, creatorName);

            return database.ExecuteNonQuery(cmd) == 1;
        }
    }
}