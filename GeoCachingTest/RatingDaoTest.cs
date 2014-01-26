using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DAL.MySQLServer;
using Swk5.GeoCaching.DAL.MySQLServer.Dao;
using Swk5.GeoCaching.DomainModel;

namespace GeoCachingTest {
    [TestClass]
    public class RatingDaoTest : AbstractTest {
        private IRatingDao target;

        [TestInitialize]
        public void Initialize() {
            Database = new Database(ConnectionString, "<image is not important here>");
            target = new RatingDao(Database);
        }

        [TestMethod]
        public void GetAllTest() {
            const int ratingsCnt = 6737;
            IList<Rating> actual = target.GetAll();
            Assert.AreEqual(ratingsCnt, actual.Count);
        }

        [TestMethod]
        public void GetAverageCacheRatingTest() {
            const int cacheId = 292;
            const double expected = 6.6429;
            double actual = target.GetAverageCacheRating(cacheId);
            Assert.AreEqual(expected, actual, 0.0001);
        }

        [TestMethod]
        public void GetByPrimaryKeyTest() {
            const int id = 2326;
            const int cacheId = 172;
            const int creatorId = 82;
            var expected = new Rating(id, cacheId, creatorId, new DateTime(2009, 07, 20), 4);
            Rating actual = target.GetByPrimaryKey(id);

            Assert.AreEqual(expected.CacheId, actual.CacheId);
            Assert.AreEqual(expected.CreatorId, actual.CreatorId);
            Assert.AreEqual(expected.CreationDate, actual.CreationDate);
            Assert.AreEqual(expected.Grade, actual.Grade);
        }

        [TestMethod]
        public void GetRatingsForCacheTest() {
            const int cacheId = 403;
            IList<Rating> expected = new List<Rating>();
            expected.Add(new Rating {Id = 5424});
            expected.Add(new Rating {Id = 5425});
            expected.Add(new Rating {Id = 5426});
            expected.Add(new Rating {Id = 5427});

            IList<Rating> actual = target.GetRatingsForCache(cacheId);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Rating entry in expected) {
                Assert.IsTrue(actual.Contains(entry));
            }
        }

        [TestMethod]
        public void GetRatingsForUserTest() {
            const int userId = 140;
            IList<Rating> expected = new List<Rating>();
            expected.Add(new Rating {Id = 103});
            expected.Add(new Rating {Id = 2391});
            expected.Add(new Rating {Id = 2894});
            expected.Add(new Rating {Id = 3349});
            expected.Add(new Rating {Id = 3456});
            expected.Add(new Rating {Id = 3875});
            expected.Add(new Rating {Id = 3953});
            expected.Add(new Rating {Id = 4812});
            expected.Add(new Rating {Id = 5521});

            IList<Rating> actual = target.GetRatingsForUser(userId);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Rating entry in expected) {
                Assert.IsTrue(actual.Contains(entry));
            }
        }

        [TestMethod]
        public void GetRatingsForCacheAndUser() {
            const int cacheId = 499;
            const int creatorId = 107;
            IList<Rating> expected = new List<Rating>();
            expected.Add(new Rating(6715, cacheId, creatorId, new DateTime(2008, 02, 20), 3));
            expected.Add(new Rating(6721, cacheId, creatorId, new DateTime(2010, 12, 03), 6));

            IList<Rating> actual = target.GetRatingsForCacheAndUser(cacheId, creatorId);

            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Rating entry in expected) {
                Assert.IsTrue(actual.Contains(entry));
            }
        }

        [TestMethod]
        public void InsertTest() {
            const int cacheId = 499;
            const int creatorId = 32;
            var toInsert = new Rating(-1, cacheId, creatorId, new DateTime(2009, 12, 6), 10);

            target.Insert(toInsert);
            Rating expected = target.GetByPrimaryKey(toInsert.Id);

            //Assert.AreEqual(expected.Id, toInsert.Id);
            Assert.AreEqual(expected.CreationDate, toInsert.CreationDate);
            Assert.AreEqual(expected.Grade, toInsert.Grade);

            DeleteRating(toInsert.Id); // internal method, not available in DaoInterface
            Assert.IsNull(target.GetByPrimaryKey(toInsert.Id));
        }

        private bool DeleteRating(int id) {
            IDbCommand cmd = Database.CreateCommand(
                "DELETE FROM cache_rating " +
                "WHERE id = @id;");
            Database.DefineParameter(cmd, "id", DbType.Int32, id);

            return Database.ExecuteNonQuery(cmd) == 1;
        }
    }
}