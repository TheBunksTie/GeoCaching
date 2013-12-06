using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DAL.MySQLServer;
using Swk5.GeoCaching.DAL.MySQLServer.Dao;
using Swk5.GeoCaching.DomainModel;

namespace GeoCachingTest {
    [TestClass]
    public class UserDaoTest : AbstractTest {
        private IUserDao target;

        [TestInitialize]
        public void Initialize() {
            Database = new Database(ConnectionString);
            target = new UserDao(Database);
        }

        [TestMethod]
        public void InsertAndDeleteTest() {
            const string userName = "SpecialTestUser";
            var user = new User(-1, userName, "top-secret", "top@secret.com", new GeoPosition(81.56, 14.56), "Finder");
            Assert.IsTrue(target.Insert(user));

            User newUser = target.GetByName(userName);
            Assert.AreEqual(user.Position, newUser.Position);
            Assert.AreEqual(user.Email, newUser.Email);

            Assert.IsTrue(target.Delete(userName));
            Assert.IsNull(target.GetByName(userName));
        }

        [TestMethod]
        public void GetAllTest() {
            const int expectedUserCount = 240;
            IList<User> actual = target.GetAll();
            Assert.AreEqual(expectedUserCount, actual.Count);
        }

        [TestMethod]
        public void GetByNameTest() {
            const String userName = "Lukas557";
            var pos = new GeoPosition(47.425659, 13.825798);
            User actual = target.GetByName(userName);

            Assert.AreEqual(userName, actual.Name);
            Assert.AreEqual(pos, actual.Position);
        }

        [TestMethod]
        public void UpdateTest() {
            const string userName = "Rebecca419";
            User actual = target.GetByName(userName);
            String originalMail = actual.Email;
            String expectedMail = actual.Email + "-~°gx,";
            actual.Email = expectedMail;

            Assert.IsTrue(target.Update(actual));
            actual = target.GetByName(userName);

            Assert.AreEqual(expectedMail, actual.Email);

            actual.Email = originalMail;
            Assert.IsTrue(target.Update(actual));
            actual = target.GetByName(userName);

            Assert.AreEqual(originalMail, actual.Email);
        }
    }
}