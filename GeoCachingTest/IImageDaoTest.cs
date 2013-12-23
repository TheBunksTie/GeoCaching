using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DAL.MySQLServer.Dao;
using Swk5.GeoCaching.DomainModel;

namespace GeoCachingTest {
    [TestClass]
    public class IImageDaoTest : AbstractTest {
        private IImageDao target;

        [TestInitialize]
        public void Initialize() {
            target = new ImageDao(Database);
        }

        [TestMethod]
        public void GetAllForCacheTest() {
            const int cacheId = 257;
            var expected = new List<Image> {
                new Image {Id = 2},
                new Image {Id = 3}
            };

            List<Image> actual = target.GetAllForCache(cacheId);
            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Image image in expected) {
                Assert.IsTrue(actual.Contains(image));
            }
        }

        [TestMethod]
        public void DeleteAllforCacheTest() {
            const int cacheId = 477;
            // no images in db for that cache
            Assert.IsFalse(target.DeleteAllForCache(cacheId));
        }

        [TestMethod]
        public void InsertAndDeleteTest() {
            var image1 = new Image {
                CacheId = 499,
                ImageData = new byte[10],
                FileName = "test1.jpg"
            };

            var image2 = new Image {
                CacheId = 499,
                ImageData = new byte[10],
                FileName = "test2.jpg"
            };

            var image3 = new Image {
                CacheId = 498,
                ImageData = new byte[10],
                FileName = "test3.jpg"
            };

            Assert.IsTrue(target.Insert(image1));
            Assert.IsTrue(target.Insert(image2));
            Assert.IsTrue(target.Insert(image3));

            Assert.IsTrue(target.Delete(image3));
            Assert.IsTrue(target.DeleteAllForCache(499));
        }
    }
}