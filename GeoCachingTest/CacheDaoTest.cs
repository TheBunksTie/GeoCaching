using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.GeoCaching.BusinessLogic.FilterManager;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DAL.MySQLServer;
using Swk5.GeoCaching.DAL.MySQLServer.Dao;
using Swk5.GeoCaching.DomainModel;

namespace GeoCachingTest {
    [TestClass]
    public class CacheDaoTest : AbstractTest {
        private ICacheDao target;
        private IFilterManager filterManager;

        [TestInitialize]
        public void Initialize() {
            Database = new Database(ConnectionString, "<image is not important here>");
            target = new CacheDao(Database);
            filterManager = new FilterManager();
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
            var cache = new Cache {
                Id = -1,
                Name = "my special test cache",
                CreationDate = new DateTime(2010, 10, 17),
                TerrainDifficulty = 1.9,
                CacheDifficulty = 2.5,
                Size = "Regular",
                OwnerId = 223,
                Position = new GeoPosition(47.451, 13.89),
                Description = "this is a test unit test cache"
            };

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
        public void GetCachesByTerrainDifficultyTest ( ) {
            DataFilter filter = filterManager.GetDefaultFilter();
            filter.FromTerrainDifficulty = 4.9;

            IList<Cache> expected = new List<Cache>();
            expected.Add(new Cache { Id = 9 });
            expected.Add(new Cache { Id = 64 });
            expected.Add(new Cache { Id = 72 });
            expected.Add(new Cache { Id = 80 });
            expected.Add(new Cache { Id = 82 });
            expected.Add(new Cache { Id = 89 });
            expected.Add(new Cache { Id = 171 });
            expected.Add(new Cache { Id = 299 });
            expected.Add(new Cache { Id = 316 });
            expected.Add(new Cache { Id = 413 });

            IList<Cache> actual = target.GetCachesMatchingFilter(filter);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach ( Cache cache in expected ) {
                Assert.IsTrue(actual.Contains(cache));
            }
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
        public void GetCachesBySizeTest ( ) {
            DataFilter filter = filterManager.GetDefaultFilter();
            filter.FromCacheSize = 4;
            filter.ToCacheSize = 4;

            IList<Cache> expected = new List<Cache>();
            expected.Add(new Cache { Id = 49 });
            expected.Add(new Cache { Id = 106 });
            expected.Add(new Cache { Id = 132 });
            expected.Add(new Cache { Id = 277 });
            expected.Add(new Cache { Id = 297 });
            expected.Add(new Cache { Id = 301 });
            expected.Add(new Cache { Id = 387 });
            expected.Add(new Cache { Id = 441 });
            expected.Add(new Cache { Id = 473 });

            IList<Cache> actual = target.GetCachesMatchingFilter(filter);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach ( Cache cache in expected ) {
                Assert.IsTrue(actual.Contains(cache));
            }
        }

        [TestMethod]
        public void GetCachesByCacheDifficultyTest() {
            DataFilter filter = filterManager.GetDefaultFilter();
            filter.FromCacheDifficulty = 4.8;

            IList<Cache> expected = new List<Cache>();
            expected.Add(new Cache {Id = 64});
            expected.Add(new Cache {Id = 72});
            expected.Add(new Cache {Id = 90});
            expected.Add(new Cache {Id = 95});
            expected.Add(new Cache {Id = 108});
            expected.Add(new Cache {Id = 424});

            IList<Cache> actual = target.GetCachesMatchingFilter(filter);

            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Cache cache in expected) {
                Assert.IsTrue(actual.Contains(cache));
            }
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
        public void GetAllTest ( ) {
            IList<Cache> actual = target.GetCachesMatchingFilter(filterManager.GetDefaultFilter());
            Assert.AreEqual(500, actual.Count);
        }

        [TestMethod]
        public void GetEarliestCacheCreationDateTest() {
            var expected = new DateTime(2001, 01, 20);
            
            Assert.AreEqual(expected, target.GetEarliestCacheCreationDate());    
        }

        [TestMethod]
        public void GetLatestCacheCreationDateTest ( ) {
            var expected = new DateTime(2031, 12, 20);

            Assert.AreEqual(expected, target.GetLatestCacheCreationDate());
        }

        [TestMethod]
        public void GetLowestCachePositionTest() {
            var expected = new GeoPosition(46.966667, 12.1913);
            var actual = target.GetLowestCachePosition();
            
            Assert.AreEqual(expected.Latitude, actual.Latitude);
            Assert.AreEqual(expected.Longitude, actual.Longitude);
        }

        [TestMethod]
        public void GetHighestCachePositionTest ( ) {
            var expected = new GeoPosition(48.4719, 14.399167);
            var actual = target.GetHighestCachePosition();

            Assert.AreEqual(expected.Latitude, actual.Latitude);
            Assert.AreEqual(expected.Longitude, actual.Longitude);
        }

        [TestMethod]
        public void GetHiddenCachesCountPerUserTest() {
            DataFilter filter = filterManager.GetDefaultFilter();
            filter.Limit = 5;
            filter.FromDate = new DateTime(2005, 01, 20);
            filter.ToDate = new DateTime(2020, 01, 20);

            var expected = new List<StatisticData> {
                new StatisticData {Name = "Heinz170", Value = "12"},
                new StatisticData {Name = "Christiane205", Value = "11"},
                new StatisticData {Name = "Georg932", Value = "10"},
                new StatisticData {Name = "Herman117", Value = "10"},
                new StatisticData {Name = "Andreas872", Value = "9"},
            };

            IList<StatisticData> actual = target.GetHiddenCachesCountPerUser(filter);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach ( StatisticData data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }
        }

        [TestMethod]
        public void GetBestRatedCaches() {
            DataFilter filter = filterManager.GetDefaultFilter();
            filter.Limit = 5;
            filter.FromDate = new DateTime(2005, 01, 20);
            filter.ToDate = new DateTime(2020, 01, 20);

            var expected = new List<StatisticData> {
                new StatisticData {Name = "K&K Berg und Tal", Value = "7,3810"},
                new StatisticData {Name = "Der alte Mann vom Maltatal", Value = "7,3636"},
                new StatisticData {Name = "Ardningalm", Value = "7,3636"},
                new StatisticData {Name = "Geologie am Hochfelln", Value = "7,3571"},
                new StatisticData {Name = "Changing money in Hellbrunn", Value = "7,3333"},
            };

            IList<StatisticData> actual = target.GetBestRatedCaches(filter);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach ( StatisticData data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }
        }

        [TestMethod]
        public void GetCacheDistributionBySizeTest() {
            DataFilter filter = filterManager.GetDefaultFilter();
            filter.FromPosition = new GeoPosition(47.0, 12.5);
            filter.ToPosition = new GeoPosition(48.0, 14.0);

            var expected = new List<StatisticData> {
                new StatisticData {Name = "Regular", Value = "47,8599"},
                new StatisticData {Name = "Small", Value = "30,3502"},
                new StatisticData {Name = "Micro", Value = "10,8949"},
                new StatisticData {Name = "None", Value = "7,3930"},
                new StatisticData {Name = "Large", Value = "2,3346"},
                new StatisticData {Name = "Other", Value = "1,1673"},
            };

            IList<StatisticData> actual = target.GetCacheDistributionBySize(filter);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach ( StatisticData data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }
        }

        [TestMethod]
        public void GetCacheDistributionByCacheDifficultyTest ( ) {
            DataFilter filter = filterManager.GetDefaultFilter();
            filter.FromPosition = new GeoPosition(47.0, 12.5);
            filter.ToPosition = new GeoPosition(48.0, 14.0);

            var expected = new List<StatisticData> {
                new StatisticData {Name = "2", Value = "28,7938"},
                new StatisticData {Name = "1,5", Value = "19,0661"},
                new StatisticData {Name = "2,5", Value = "16,7315"},
                new StatisticData {Name = "3", Value = "13,2296"},
                new StatisticData {Name = "1", Value = "11,6732"},
                new StatisticData {Name = "3,5", Value = "5,8366"},
                new StatisticData {Name = "4", Value = "1,9455"},
                new StatisticData {Name = "5", Value = "1,9455"},
                new StatisticData {Name = "4,5", Value = "0,7782"}
            };

            IList<StatisticData> actual = target.GetCacheDistributionByCacheDifficulty(filter);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach ( StatisticData data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }
        }

        [TestMethod]
        public void GetCacheDistributionByTerrainDifficultyTest ( ) {
            DataFilter filter = filterManager.GetDefaultFilter();
            filter.FromPosition = new GeoPosition(47.0, 12.5);
            filter.ToPosition = new GeoPosition(48.0, 14.0);

            var expected = new List<StatisticData> {
                new StatisticData {Name = "3", Value = "17,5097"},
                new StatisticData {Name = "2", Value = "15,1751"},
                new StatisticData {Name = "1,5", Value = "14,7860"},
                new StatisticData {Name = "2,5", Value = "13,2296"},
                new StatisticData {Name = "3,5", Value = "12,4514"},
                new StatisticData {Name = "4", Value = "10,1167"},
                new StatisticData {Name = "1", Value = "7,3930"},
                new StatisticData {Name = "4,5", Value = "6,2257"},
                new StatisticData {Name = "5", Value = "3,1128"}
            };

            IList<StatisticData> actual = target.GetCacheDistributionByTerrainDifficulty(filter);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach ( StatisticData data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }
        }
    }
}