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
    public class UserDaoTest {
        private const string ConnectionString = "server=localhost;Uid=geocaching;Password=geocaching;Persist Security Info=False;database=geocachingtest";

        private IDatabase database;
        private IUserDao target;

        [TestInitialize]
        public void Initialize() {
            database = new Database(ConnectionString);
            target = new UserDao(database);
        }

        [TestMethod]
        public void InsertAndDeleteTest() {
            const string userName = "SpecialTestUser";
            User user = new User(userName, "top-secret", "top@secret.com", new GeoPosition(81.56, 14.56), 2, new DateTime(2013, 07, 09));
            Assert.IsTrue(target.Insert(user));

            User newUser = target.GetByName(userName);
            Assert.AreEqual(user.Position, newUser.Position);
            Assert.AreEqual(user.RegistrationDate, newUser.RegistrationDate);
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
            GeoPosition pos = new GeoPosition(47.425659, 13.825798);
            DateTime registrationDate = new DateTime(2007, 05, 12);        
            User actual = target.GetByName(userName);

            Assert.AreEqual(userName, actual.Name);
            Assert.AreEqual(pos, actual.Position);
            Assert.AreEqual(registrationDate, actual.RegistrationDate);    
        }

        [TestMethod]
        public void GetNrCreatedCachesForUsersTest() {
            IDatabase database = null; // TODO: Initialize to an appropriate value
            var target = new UserDao(database); // TODO: Initialize to an appropriate value
            IDictionary<User, int> expected = null; // TODO: Initialize to an appropriate value
            IDictionary<User, int> actual;
            actual = target.GetNrCreatedCachesForUsers();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetNrFoundCachesForUsersTest() {
            IDatabase database = null; // TODO: Initialize to an appropriate value
            var target = new UserDao(database); // TODO: Initialize to an appropriate value
            IDictionary<User, int> expected = null; // TODO: Initialize to an appropriate value
            IDictionary<User, int> actual;
            actual = target.GetNrFoundCachesForUsers();
            Assert.AreEqual(expected, actual);
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