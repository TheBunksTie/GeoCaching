using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.UserManager;
using Swk5.GeoCaching.DomainModel;

namespace GeoCachingTest {

    [TestClass]
    public class IUserManagerTest {

        private IUserManager uManager;

        [TestInitialize]
        public void Initialize ( ) {
            uManager = GeoCachingBLFactory.GetUserManager();
        }
        
        [TestMethod]
        public void CreateNewDefaultUserTest() {
            User expected = new User { Id = -1, Name = "<new user>", Password = "".Encrypt(), Email = "my.mail@domain.com", Position = new GeoPosition(47.123, 18.123), Role = "Finder" };
            User actual = uManager.CreateNewDefaultUser();
            Assert.AreEqual(expected.Password, actual.Password);
            Assert.AreEqual(expected.Position, actual.Position);

            actual.Role = "Superuser";
            Assert.IsTrue(uManager.UpdateExistingUser(actual));

            Assert.IsTrue(uManager.DeleteUser(actual.Id));
        }
  
        [TestMethod]
        public void GetUserListTest() {            
            List<User> actual = uManager.GetUserList();
            Assert.AreEqual(240, actual.Count);
        }

        [TestMethod]
        public void GetUserRoleListTest() {
            var expected = new List<string> {
                "Finder", "Hider", "Superuser"
            };
            List<string> actual = uManager.GetUserRoleList();

            Assert.AreEqual(expected.Count, actual.Count);
            foreach ( string size in expected ) {
                Assert.IsTrue(actual.Contains(size));
            }
        }       
    }
}