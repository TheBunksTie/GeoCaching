using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.DomainModel;

namespace GeoCachingTest {
    [TestClass]
    public class ICacheManagerTest : AbstractTest {
        private ICacheManager cacheManager;

        [TestInitialize]
        public void Initialize() {
            cacheManager = new CacheManager();
        }

        [TestMethod]
        public void CreateNewPositionedCacheTest() {
            const int ownerId = 203;
            const double latitude = 47.123;
            const double longitude = 13.99;
            var expected = new Cache {
                Id = -1,
                Name = "the super special cache",
                Description = "a very special cache, used by unittesting",
                Position = new GeoPosition(latitude, longitude),
                CacheDifficulty = 2.5,
                TerrainDifficulty = 1.5,
                CreationDate = new DateTime(1999, 7, 23),
                OwnerId = ownerId,
                Size = "Regular"
            };

            Cache actual = cacheManager.CreateNewPositionedCache(ownerId, latitude, longitude);
            
            Assert.AreEqual(expected.OwnerId, actual.OwnerId);
            Assert.AreEqual(expected.Position, actual.Position);

            actual.Name = "a little change of unit test name";
            Assert.IsTrue(cacheManager.UpdateExisitingCache(actual));
            
            Assert.IsTrue(cacheManager.DeleteCache(actual.Id));
        }

        [TestMethod]
        public void UploadImageTest() {
            const int cacheId = 499;
            Stream imageStream =
                new FileStream("D:\\fh\\05_WiSe13\\SWK\\UE\\geocaching\\testdata\\images\\building00005p.jpg",
                    FileMode.Open);
            const string fileExtension = ".jpg";
            var expected = new Image {
                CacheId = 499
            };
            Image actual = cacheManager.UploadImage(cacheId, imageStream, fileExtension);
            Assert.AreEqual(expected.CacheId, actual.CacheId);
            Assert.IsTrue(cacheManager.DeleteImage(actual));
        }

        [TestMethod]
        public void GetCacheListTest() {
            List<Cache> actual = cacheManager.GetCacheList();
            Assert.AreEqual(500, actual.Count);
        }

        [TestMethod]
        public void GetCacheOwnerTest() {
            var c = new Cache {
                OwnerId = 101
            };

            var expected = new User {
                Name = "Heinz170"
            };
            User actual = cacheManager.GetCacheOwner(c);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod]
        public void GetCacheSizeListTest() {
            var expected = new List<string> {
                "Micro", "Small", "Regular", "Large", "Other", "None"
            };

            List<string> actual = cacheManager.GetCacheSizeList();
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (string size in expected) {
                Assert.IsTrue(actual.Contains(size));
            }
        }

        [TestMethod]
        public void GetFilteredCacheListTest() {
            var criterium = FilterCriterium.CacheDifficulty;
            var operation = FilterOperation.Exact;
            string filterValue = "3";
            List<Cache> actual = cacheManager.GetFilteredCacheList(criterium, operation, filterValue);
            Assert.AreEqual(61, actual.Count);
        }

        [TestMethod]
        public void GetImagesForCacheTest() {
            int cacheId = 309;
            List<Image> actual = cacheManager.GetImagesForCache(cacheId);
            Assert.AreEqual(5, actual.Count);
        }
    }
}