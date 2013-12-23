using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.AuthenticationManager;
using Swk5.GeoCaching.DomainModel;

namespace GeoCachingTest {
    [TestClass]
    public class AuthenticationManagerTest {
        private IAuthenticationManager authManager;

        [TestInitialize]
        public void Initialize() {
            authManager = GeoCachingBLFactory.GetAuthentificationManager();
        }

        [TestMethod]
        public void AuthenticateUserTest() {
            const string username = "Sookie514";
            const string password = "test";
            const bool priviligedRequired = false;

            User expected = new User {
                Id = 210,
                Name = username,
                Password = password.Encrypt(),
                Email = "Sookie514@domain.at",
                Position = new GeoPosition(48.149303, 13.765459),
            };

            User actual = authManager.AuthenticateUser(username, password, priviligedRequired);
            Assert.AreEqual(expected, actual);

            Assert.AreEqual(expected, authManager.AuthenticatedUser);
            authManager.LogoutUser();

            Assert.IsNull(authManager.AuthenticatedUser);

        }
    }
}