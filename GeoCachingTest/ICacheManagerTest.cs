using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.DomainModel;

namespace GeoCachingTest {
    [TestClass]
    public class ICacheManagerTest : AbstractTest {
        private CacheManager cacheManager;        

        [TestInitialize]
        public void Initialize() {
            cacheManager = new CacheManager();
        }

        [TestMethod]
        public void CreateNewPositionedCacheTest() {
            User u = new User {
                Id = 209
            };
            
            // set user as authenticated (method is only for TESTING)
            cacheManager.SetAuthenticatedUser(u);

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
                OwnerId = u.Id,
                Size = "Regular"
            };

            Cache actual = cacheManager.CreateNewPositionedCache(u.Id, latitude, longitude);
            

            Assert.AreEqual(expected.OwnerId, actual.OwnerId);
            Assert.AreEqual(expected.Position, actual.Position);

            actual.Name = "a little change of unit test name";
            Assert.IsTrue(cacheManager.UpdateExisitingCache(actual));
            
            Assert.IsTrue(cacheManager.DeleteCache(actual.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void InvalidUpdateCache() {

            User u = new User {
                Id = 209
            };

            // set user as authenticated (method is only for TESTING)
            cacheManager.SetAuthenticatedUser(u);

            var c = new Cache {
                Id = -1,
                Name = "testcache",
                Description = "a very special cache, used by unittesting",
                Position = new GeoPosition(47.11, 11.47),
                CacheDifficulty = 2.5,
                TerrainDifficulty = 1.5,
                CreationDate = new DateTime(1999, 7, 23),
                OwnerId = 101,
                Size = "Regular"
            };
           
            // exception because authenticate user is not cache owner
            cacheManager.UpdateExisitingCache(c);
        }

        [TestMethod]
        public void UploadImageTest() {

            // needs to be the owner of the cache
            User u = new User {
                Id = 133
            };

            // set user as authenticated (method is only for TESTING)
            cacheManager.SetAuthenticatedUser(u);

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
        public void GetFullCacheListTest ( ) {
            List<Cache> actual = cacheManager.GetFilteredCacheList(cacheManager.GetDefaultFilter());
            Assert.AreEqual(500, actual.Count);
        }

        [TestMethod]
        public void GetCacheOwnerTest() {            
            var c = new Cache {
                Id = -1,
                Name = "the super special cache",
                Description = "a very special cache, used by unittesting",
                Position = new GeoPosition(47.11, 11.47),
                CacheDifficulty = 2.5,
                TerrainDifficulty = 1.5,
                CreationDate = new DateTime(1999, 7, 23),
                OwnerId = 101,
                Size = "Regular"
            };

            var expected = new User {
                Name = "Heinz170",
            };

            User actual = cacheManager.GetCacheOwner(c);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void InvalidGetCacheOwnerTest ( ) {
            var c = new Cache {
                Id = -1,              
                Description = "a very special cache, used by unittesting",
                Position = new GeoPosition(47.11, 11.47),
                CacheDifficulty = 2.5,
                TerrainDifficulty = 1.5,
                CreationDate = new DateTime(1999, 7, 23),
                OwnerId = 101,
                Size = "Regular"
            };           
            cacheManager.GetCacheOwner(c);            
            // exception because cache is invalid (missing name)
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
        public void GetFilteredCacheListTest ( ) {
            DataFilter filter = cacheManager.GetDefaultFilter();
            filter.FromCacheDifficulty = 3;
            filter.ToCacheDifficulty = 3;

            List<Cache> actual = cacheManager.GetFilteredCacheList(filter);
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