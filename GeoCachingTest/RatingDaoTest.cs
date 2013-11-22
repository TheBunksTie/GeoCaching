using System.Collections.Generic;
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
        public void GetAverageCacheRatingTest() {
            // TODO type conversion of double ???
            const int cacheId = 292;
            const double expected = 6.6429;
            double actual = target.GetAverageCacheRating(cacheId);
            Assert.AreEqual(expected, actual, 0.0001);
        }

        [TestMethod]
        public void GetByPrimaryKeyTest() {
            IDatabase database = null; // TODO: Initialize to an appropriate value
            var target = new RatingDao(database); // TODO: Initialize to an appropriate value
            int cacheId = 0; // TODO: Initialize to an appropriate value
            string creatorName = string.Empty; // TODO: Initialize to an appropriate value
            Rating expected = null; // TODO: Initialize to an appropriate value
            Rating actual;
            actual = target.GetByPrimaryKey(cacheId, creatorName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetRatingsForCacheTest() {
            IDatabase database = null; // TODO: Initialize to an appropriate value
            var target = new RatingDao(database); // TODO: Initialize to an appropriate value
            int cacheId = 0; // TODO: Initialize to an appropriate value
            IList<Rating> expected = null; // TODO: Initialize to an appropriate value
            IList<Rating> actual;
            actual = target.GetRatingsForCache(cacheId);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetRatingsForUserTest() {
            IDatabase database = null; // TODO: Initialize to an appropriate value
            var target = new RatingDao(database); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            IList<Rating> expected = null; // TODO: Initialize to an appropriate value
            IList<Rating> actual;
            actual = target.GetRatingsForUser(userName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest() {
            IDatabase database = null; // TODO: Initialize to an appropriate value
            var target = new RatingDao(database); // TODO: Initialize to an appropriate value
            Rating rating = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Insert(rating);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest() {
            IDatabase database = null; // TODO: Initialize to an appropriate value
            var target = new RatingDao(database); // TODO: Initialize to an appropriate value
            Rating rating = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Update(rating);
            Assert.AreEqual(expected, actual);
        }
    }
}