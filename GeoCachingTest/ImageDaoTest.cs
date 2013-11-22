using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DAL.MySQLServer;
using Swk5.GeoCaching.DAL.MySQLServer.Dao;

namespace GeoCachingTest {
    [TestClass]
    public class ImageDaoTest {
        private const string ConnectionString = "server=localhost;Uid=geocaching;Password=geocaching;Persist Security Info=False;database=geocachingtest";

        private IDatabase database;
        private IImageDao target;

        [TestInitialize]
        public void Initialize() {
            database = new Database(ConnectionString);
            target = new ImageDao(database);
        }

        [TestMethod]
        public void InsertAndDeleteTest() {
            const int cacheId = 137;
            const string fileName = "./images/coolPic.pic";
            Assert.IsTrue(target.Insert(cacheId, fileName));

            IList<string> list = target.GetAllForCache(cacheId);            
            Assert.AreEqual(1, list.Count);

            Assert.IsTrue(target.Delete(cacheId, fileName));
            list = target.GetAllForCache(cacheId);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void GetAllForCacheTest() {
            const int cacheId = 268;
            IList<string> expected = new List<string>();
            expected.Add("./images/myCoolCache.jpg");
            expected.Add("./images/northEastView.png");

            IList<string> actual = target.GetAllForCache(cacheId);

            Assert.AreEqual(expected.Count, actual.Count);

            foreach (var image in expected) {
                Assert.IsTrue(actual.Contains(image));
            }
        }
    }
}