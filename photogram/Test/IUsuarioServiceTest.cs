using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Model.UserService.Exceptions;
using Es.Udc.DotNet.Photogram.Model.UserService.Util;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Transactions;
using Es.Udc.DotNet.Photogram.Model;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Photogram.Test
{
    /// <summary>
    /// This is a test class for IUserServiceTest and is intended to contain all IUserServiceTest
    /// Unit Tests
    /// </summary>
    [TestClass]
    public class AIUsuarioServiceTest
    {
        // Variables used in several tests are initialized here
        private const string loginName = "loginNameTest";

        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "user@udc.es";
        private const string language = "es";
        private const string country = "ES";
        private const Boolean conexion = false;
        private const Boolean mantener = false;
        private const long NON_EXISTENT_USER_ID = -1;
        private static IKernel kernel;
        private static IUserService userService;
        private static IUserDao userProfileDao;

        private TransactionScope transactionScope;

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the
        /// current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// A test for RegisterUser
        /// </summary>
        [TestMethod]
        public void RegisterUserTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user and find profile
                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var userProfile = userProfileDao.Find(userId);

                // Check data
                Assert.AreEqual(userId, userProfile.userId);
                Assert.AreEqual(loginName, userProfile.loginName);
                Assert.AreEqual(PasswordEncrypter.Crypt(clearPassword), userProfile.password);
                Assert.AreEqual(firstName, userProfile.firstName);
                Assert.AreEqual(lastName, userProfile.lastName);
                Assert.AreEqual(email, userProfile.email);
                Assert.AreEqual(language, userProfile.language);
                Assert.AreEqual(country, userProfile.country);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for registering a user that already exists in the database
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void RegisterDuplicatedUserTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                userService.CreateUser(loginName, clearPassword,
                    new UserProfile(firstName, lastName, email, language, country));

                // Register the same user
                userService.CreateUser(loginName, clearPassword,
                    new UserProfile(firstName, lastName, email, language, country));

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with clear password
        /////</summary>
        [TestMethod]
        public void LoginClearPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.CreateUser(loginName, clearPassword,
                    new UserProfile(firstName, lastName, email, language, country));

                var expected = new LoginResult(userId, firstName,
                    PasswordEncrypter.Crypt(clearPassword), language, country);

                // Login with clear password
                var actual =
                    userService.ConectUser(loginName,
                        clearPassword,false);

                // Check data
                Assert.AreEqual(expected, actual);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with encrypted password
        /////</summary>
        [TestMethod]
        public void LoginEncryptedPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.CreateUser(loginName, clearPassword,
                    new UserProfile(firstName, lastName, email, language, country));

                var expected = new LoginResult(userId, firstName,
                    PasswordEncrypter.Crypt(clearPassword), language, country);

                // Login with encrypted password
                var obtained =
                    userService.ConectUser(loginName,
                        PasswordEncrypter.Crypt(clearPassword), true);

                // Check data
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with incorrect password
        /////</summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void LoginIncorrectPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.CreateUser(loginName, clearPassword,
                    new UserProfile(firstName, lastName, email, language, country));

                // Login with incorrect (clear) password
                var actual =
                    userService.ConectUser(loginName, clearPassword + "X", false);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with a non-existing user
        /////</summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void LoginNonExistingUserTest()
        {
            // Login for a user that has not been registered
            var actual =
                userService.ConectUser(loginName, clearPassword, false);
        }
        
        /// <summary>
        /// A test for FindUserProfileDetails
        /// </summary>
        [TestMethod]
        public void FindUserProfileDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                var expected =
                    new UserProfile(firstName, lastName, email, language, country);

                var userId =
                    userService.CreateUser(loginName, clearPassword, expected);

                var obtained =
                    userService.FindUserProfileDetails(userId);

                // Check data
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        
        /// <summary>
        /// A test for FindUserProfileDetails
        /// </summary>
        [TestMethod]
        public void FindFollowerProfileDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                UserAccount user1 = GetValidUser("user1");

                UserAccount user2 = GetValidUser("user2");

                userService.FollowUser(user1.userId, user2.userId);

                List<UserAccount> obtained =
                    userService.SeeFollowers(user1.userId);


                // Check data
                Assert.AreEqual(user2, obtained[0]);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for FindUserProfileDetails
        /// </summary>
        [TestMethod]
        public void FindFollowsProfileDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                UserAccount user1 = GetValidUser("user1");

                UserAccount user2 = GetValidUser("user2");

                userService.FollowUser(user1.userId, user2.userId);

                List<UserAccount> obtained =
                    userService.SeeFollow(user2.userId);


                // Check data
                Assert.AreEqual(user1, obtained[0]);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for FindUserProfileDetails when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindUserProfileDetailsForNonExistingUserTest()
        {
            userService.FindUserProfileDetails(NON_EXISTENT_USER_ID);
        }
        
        /// <summary>
        /// A test for UpdateUserProfileDetails
        /// </summary>
        [TestMethod]
        public void UpdateUserProfileDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user and update profile details
                var userId = userService.CreateUser(loginName, clearPassword,
                    new UserProfile(firstName, lastName, email, language, country));

                var expected =
                    new UserProfile(firstName + "X", lastName + "X",
                        email + "X", "XX", "XX");

                userService.UpdateProfile(userId, expected);

                var obtained =
                    userService.FindUserProfileDetails(userId);

                // Check changes
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for UpdateUserProfileDetails when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateUserProfileDetailsForNonExistingUserTest()
        {
            using (var scope = new TransactionScope())
            {
                userService.UpdateProfile(NON_EXISTENT_USER_ID,
                    new UserProfile(firstName, lastName, email, language, country));

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for ChangePassword
        /// </summary>
        [TestMethod]
        public void ChangePasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.CreateUser(loginName, clearPassword,
                    new UserProfile(firstName, lastName, email, language, country));

                // Change password
                var newClearPassword = clearPassword + "X";
                userService.ChangePassword(userId, clearPassword, newClearPassword);

                // Try to login with the new password. If the login is correct, then the password
                // was successfully changed.
                userService.ConectUser(loginName, newClearPassword, false);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }
       
        /// <summary>
        /// A test for ChangePassword entering a wrong old password
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void ChangePasswordWithIncorrectPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.CreateUser(loginName, clearPassword,
                    new UserProfile(firstName, lastName, email, language, country));

                // Change password
                var newClearPassword = clearPassword + "X";
                userService.ChangePassword(userId, clearPassword + "Y", newClearPassword);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }
       
       
        /// <summary>
        /// A test for ChangePassword when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ChangePasswordForNonExistingUserTest()
        {
            userService.ChangePassword(NON_EXISTENT_USER_ID,
                clearPassword, clearPassword + "X");
        }

        /// <summary>
        /// A test to check if a valid user loginName is found
        /// </summary>
        [TestMethod]
        public void UserExistsForValidUser()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                userService.CreateUser(loginName, clearPassword,
                    new UserProfile(firstName, lastName, email, language, country));

                bool userExists = userService.UserExists(loginName);

                Assert.IsTrue(userExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test to check if a not valid user loginame is found
        /// </summary>
        [TestMethod]
        public void UserExistsForNotValidUser()
        {
            using (var scope = new TransactionScope())
            {
                String invalidLoginName = loginName + "_someFakeUserSuffix";

                bool userExists = userService.UserExists(invalidLoginName);

                Assert.IsFalse(userExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for Follow
        ///</summary>
        [TestMethod()]
        public void FollowUserTest()
        {
            using (var scope = new TransactionScope())
            {
                List<UserAccount> followers = new List<UserAccount>();

                UserAccount user1 = GetValidUser("user1");

                UserAccount user2 = GetValidUser("user2");

                UserAccount user3 = GetValidUser("user3");

                followers.Add(user2);
                followers.Add(user3);
                try
                {

                    userService.FollowUser(user1.userId, user2.userId);
                    
                    userService.FollowUser(user1.userId, user3.userId);

                    List<UserAccount> followersBD = userService.SeeFollowers(user1.userId);

                    CollectionAssert.AreEqual(followers, followersBD);
                    
                }
                catch (Exception e)
                {
                    Assert.Fail(e.Message);
                }

            }
        }

        /// <summary>
        ///A test for Follow
        ///</summary>
        [TestMethod()]
        public void UnFollowUserTest()
        {
            using (var scope = new TransactionScope())
            {
                UserAccount user1 = GetValidUser("user1");

                UserAccount user2 = GetValidUser("user2");

                UserAccount user3 = GetValidUser("user3");

                try
                {

                    userService.FollowUser(user1.userId, user2.userId);
                    userService.FollowUser(user1.userId, user3.userId);

                    userService.UnFollowUser(user1.userId, user2.userId);

                    List<UserAccount> followersBD = userService.SeeFollowers(user1.userId);
                    Console.WriteLine(followersBD[0].loginName);

                    Assert.AreEqual(1, followersBD.Count);

                }
                catch (Exception e)
                {
                    Assert.Fail(e.Message);
                }

            }
        }

        /// <summary>
        ///A test for Follow
        ///</summary>
        [TestMethod()]
        public void WhoUserFollowTest()
        {
            using (var scope = new TransactionScope())
            {
                List<UserAccount> follow = new List<UserAccount>();

                UserAccount user1 = GetValidUser("user1");

                UserAccount user2 = GetValidUser("user2");

                UserAccount user3 = GetValidUser("user3");

                follow.Add(user2);
                follow.Add(user3);
                try
                {

                    userService.FollowUser(user2.userId, user1.userId);
                    userService.FollowUser(user3.userId, user1.userId);

                    List<UserAccount> followBD = userService.SeeFollow(user1.userId);

                    CollectionAssert.AreEqual(follow, followBD);

                }
                catch (Exception e)
                {
                    Assert.Fail(e.Message);
                }

            }
        }

        /// <summary>
        ///A test for UserFollowExists
        ///</summary>
        [TestMethod()]
        public void UserFollowExistsTest()
        {
            using (var scope = new TransactionScope())
            {
                List<UserAccount> follow = new List<UserAccount>();

                UserAccount user1 = GetValidUser("user1");

                UserAccount user2 = GetValidUser("user2");

                UserAccount user3 = GetValidUser("user3");

                follow.Add(user2);
                follow.Add(user3);
                try
                {

                    userService.FollowUser(user2.userId, user1.userId);
                    userService.FollowUser(user3.userId, user1.userId);

                    bool followBD = userService.UserFollowExists("user1","user2");

                    Assert.IsTrue(followBD);

                }
                catch (Exception e)
                {
                    Assert.Fail(e.Message);
                }

            }
        }


        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            userProfileDao = kernel.Get<IUserDao>();
            userService = kernel.Get<IUserService>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        private UserAccount GetValidUser(String loginName)
        {
            UserAccount user = new UserAccount();
            user.firstName = "pepito";
            user.loginName = loginName;
            user.lastName = "perez";
            user.email = "pepito@udc.es";
            user.language = "es";
            user.country = "ES";
            user.password = "password";
            userProfileDao.Create(user);
            return user;
        }

        #endregion Additional test attributes
    }
}
