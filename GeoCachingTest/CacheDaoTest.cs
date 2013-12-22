using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DAL.MySQLServer;
using Swk5.GeoCaching.DAL.MySQLServer.Dao;
using Swk5.GeoCaching.DomainModel;

namespace GeoCachingTest {
    [TestClass]
    public class CacheDaoTest : AbstractTest {
        private ICacheDao target;

        [TestInitialize]
        public void Initialize() {
            Database = new Database(ConnectionString, "<image is not important here>");
            target = new CacheDao(Database);
        }

        [TestMethod]
        public void UpdateTest() {
            const int id = 112;
            Cache actual = target.GetById(id);
            String originalName = actual.Name;
            String expectedName = actual.Name + id;
            var creationDate = new DateTime(2008, 07, 19);
            actual.Name = expectedName;

            Assert.IsTrue(target.Update(actual));
            actual = target.GetById(id);

            Assert.AreEqual(expectedName, actual.Name);
            Assert.AreEqual(creationDate, actual.CreationDate);

            actual.Name = originalName;
            Assert.IsTrue(target.Update(actual));
            actual = target.GetById(id);

            Assert.AreEqual(originalName, actual.Name);
        }

        [TestMethod]
        public void InsertDeleteTest() {
            var cache = new Cache(-1, "my special test cache", new DateTime(2010, 10, 17), 1.9, 2.5, "Regular", 223, new GeoPosition(47.451, 13.89),
                "this is a test unit test cache");
            int id = target.Insert(cache);
            Assert.IsTrue(id > 0);

            Cache newState = target.GetById(id);
            Assert.AreEqual(cache.TerrainDifficulty, newState.TerrainDifficulty);
            Assert.AreEqual(cache.Name, newState.Name);
            Assert.AreEqual(cache.OwnerId, newState.OwnerId);

            bool success = target.Delete(cache.Id);
            Assert.IsTrue(success);
            Assert.IsNull(target.GetById(id));
        }

        [TestMethod]
        public void GetCachesInRegionRatedBetweenTest() {
            var begin = new DateTime(2009, 10, 5);
            var end = new DateTime(2013, 05, 30);
            var from = new GeoPosition(47.18, 13.41);
            var to = new GeoPosition(47.43, 13.86);
            IList<Cache> expected = new List<Cache>();
            expected.Add(new Cache {Id = 201});
            expected.Add(new Cache {Id = 207});
            expected.Add(new Cache {Id = 217});

            //IList<Cache> actual = target.GetInRegionRatedBetween(begin, end, from, to);
            //Assert.AreEqual(expected.Count, actual.Count);

            //foreach (Cache cache in expected) {
            //    Assert.IsTrue(actual.Contains(cache));
            //}
        }

        [TestMethod]
        public void GetCachesInRegionFoundBetweenTest() {
            var begin = new DateTime(2007, 10, 5);
            var end = new DateTime(2011, 05, 30);
            var from = new GeoPosition(47.223, 13.91);
            var to = new GeoPosition(47.479, 14.86);
            IList<Cache> expected = new List<Cache>();
            expected.Add(new Cache {Id = 337});
            expected.Add(new Cache {Id = 359});
            expected.Add(new Cache {Id = 396});
            expected.Add(new Cache {Id = 426});
            expected.Add(new Cache {Id = 499});

            //IList<Cache> actual = target.GetInRegionFoundBetween(begin, end, from, to);
            //Assert.AreEqual(expected.Count, actual.Count);

            //foreach (Cache cache in expected) {
            //    Assert.IsTrue(actual.Contains(cache));
            //}
        }

        [TestMethod]
        public void GetCachesInRegionCreatedBetweenTest() {
            var begin = new DateTime(2007, 10, 5);
            var end = new DateTime(2013, 05, 30);
            var from = new GeoPosition(47.71, 13.43);
            var to = new GeoPosition(47.89, 13.96);
            IList<Cache> expected = new List<Cache>();
            expected.Add(new Cache {Id = 13});
            expected.Add(new Cache {Id = 45});
            expected.Add(new Cache {Id = 47});
            expected.Add(new Cache {Id = 73});

            //IList<Cache> actual = target.GetInRegionCreatedBetween(begin, end, from, to);
            //Assert.AreEqual(expected.Count, actual.Count);

            //foreach (Cache cache in expected) {
            //    Assert.IsTrue(actual.Contains(cache));
            //}
        }

        [TestMethod]
        public void GetCachesByTerrainDifficultyTest() {
            const double difficulty = 4.9;
            IList<Cache> expected = new List<Cache>();
            expected.Add(new Cache {Id = 9});
            expected.Add(new Cache {Id = 64});
            expected.Add(new Cache {Id = 72});
            expected.Add(new Cache {Id = 80});
            expected.Add(new Cache {Id = 82});
            expected.Add(new Cache {Id = 89});
            expected.Add(new Cache {Id = 171});
            expected.Add(new Cache {Id = 299});
            expected.Add(new Cache {Id = 316});
            expected.Add(new Cache {Id = 413});
            
            // TODO IList<Cache> actual = target.GetByTerrainDifficulty(difficulty, FilterCriterium.AboveEquals);
            //Assert.AreEqual(expected.Count, actual.Count);

            //foreach (Cache cache in expected) {
            //    Assert.IsTrue(actual.Contains(cache));
            //}
        }

        [TestMethod]
        public void GetCachesByOwnerTest() {
            const int ownerId = 49;
            IList<Cache> expected = new List<Cache>();
            expected.Add(new Cache {Id = 57});
            expected.Add(new Cache {Id = 75});
            expected.Add(new Cache {Id = 216});
            expected.Add(new Cache {Id = 238});
            expected.Add(new Cache {Id = 327});
            expected.Add(new Cache {Id = 429});
            expected.Add(new Cache {Id = 444});
            expected.Add(new Cache {Id = 459});

            IList<Cache> actual = target.GetByOwner(ownerId);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Cache cache in expected) {
                Assert.IsTrue(actual.Contains(cache));
            }
        }

        [TestMethod]
        public void GetCachesBySizeTest() {
            IList<Cache> expected = new List<Cache>();
            expected.Add(new Cache {Id = 49});
            expected.Add(new Cache {Id = 106});
            expected.Add(new Cache {Id = 132});
            expected.Add(new Cache {Id = 277});
            expected.Add(new Cache {Id = 297});
            expected.Add(new Cache {Id = 301});
            expected.Add(new Cache {Id = 387});
            expected.Add(new Cache {Id = 441});
            expected.Add(new Cache {Id = 473});

            // TODO IList<Cache> actual = target.GetBySize("Large", FilterCriterium.Exact);
            //Assert.AreEqual(expected.Count, actual.Count);

            //foreach (Cache cache in expected) {
            //    Assert.IsTrue(actual.Contains(cache));
            //}
        }

        [TestMethod]
        public void GetCachesByRatingTest() {
            const double rating = 7.5;
            IList<Cache> expected = new List<Cache>();
            expected.Add(new Cache {Id = 131});
            expected.Add(new Cache {Id = 188});
            expected.Add(new Cache {Id = 229});
            expected.Add(new Cache {Id = 470});

            // TODO IList<Cache> actual = target.GetByAverageRating(rating, FilterCriterium.AboveEquals);
            //Assert.AreEqual(expected.Count, actual.Count);

            //foreach (Cache cache in expected) {
            //    Assert.IsTrue(actual.Contains(cache));
            //}
        }

        [TestMethod]
        public void GetCachesByCacheDifficultyTest() {
            const double difficulty = 4.8;
            IList<Cache> expected = new List<Cache>();
            expected.Add(new Cache {Id = 64});
            expected.Add(new Cache {Id = 72});
            expected.Add(new Cache {Id = 90});
            expected.Add(new Cache {Id = 95});
            expected.Add(new Cache {Id = 108});
            expected.Add(new Cache {Id = 424});

            // TODO IList<Cache> actual = target.GetByCacheDifficulty(difficulty, FilterCriterium.Above);
            //Assert.AreEqual(expected.Count, actual.Count);

            //foreach (Cache cache in expected) {
            //    Assert.IsTrue(actual.Contains(cache));
            //}
        }

        [TestMethod]
        public void GetByIdTest() {
            const string expectedName = "Der Franzose";
            var expectedPos = new GeoPosition(47.417983, 14.191783);
            const double expectedTerrainDifficulty = 3.0;
            var creationDate = new DateTime(2009, 06, 20);

            Cache actual = target.GetById(396);

            Assert.AreEqual(expectedName, actual.Name);
            Assert.AreEqual(expectedPos, actual.Position);
            Assert.AreEqual(expectedTerrainDifficulty, actual.TerrainDifficulty);
            Assert.AreEqual(creationDate, actual.CreationDate);
        }

        [TestMethod]
        public void GetAllTest() {
            IList<Cache> actual = target.GetAll();
            Assert.AreEqual(500, actual.Count);
        }
    }
}