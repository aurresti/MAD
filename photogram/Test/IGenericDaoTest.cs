using Es.Udc.DotNet.Photogram.Model;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Data.Entity;
using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Dao;
using Ninject.Activation;

namespace Es.Udc.DotNet.Photogram.Test
{
    /// <summary>
    ///This is a test class for IGenericDaoTest and is intended
    ///to contain all IGenericDaoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IGenericDaoTest
    {
        private static IKernel kernel;

        private TestContext testContextInstance;
        private User userProfile;
        private static IUserDao userProfileDao;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        #region Additional test attributes

        //
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            userProfileDao = kernel.Get<IUserDao>();
        }

        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            userProfile = new User();
            userProfile.Login = "jsmith";
            userProfile.Password = "password";
            userProfile.FirstName = "John";
            userProfile.LastName = "Smith";
            userProfile.Email = "jsmith@acme.com";
            userProfile.Language = "en";
            userProfile.Country = "US";

            userProfileDao.Create(userProfile);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            try
            {
                userProfileDao.Remove(userProfile.Id);
            }
            catch (Exception)
            {
            }
        }

        #endregion Additional test attributes

        [TestMethod()]
        public void DAO_FindTest()
        {
            try
            {
                User actual = userProfileDao.Find(userProfile.Id);

                Assert.AreEqual(userProfile, actual, "User found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod()]
        public void DAO_ExistsTest()
        {
            try
            {
                bool userExists = userProfileDao.Exists(userProfile.Id);

                Assert.IsTrue(userExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod()]
        public void DAO_NotExistsTest()
        {
            try
            {
                bool userNotExists = userProfileDao.Exists(-1);

                Assert.IsFalse(userNotExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void DAO_UpdateTest()
        {
            try
            {
                userProfile.FirstName = "Juan";
                userProfile.LastName = "González";
                userProfile.Email = "jgonzalez@acme.es";
                userProfile.Language = "es";
                userProfile.Country = "ES";
                userProfile.Password = "contraseña";

                userProfileDao.Update(userProfile);

                User actual = userProfileDao.Find(userProfile.Id);

                Assert.AreEqual(userProfile, actual);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Remove
        [TestMethod()]
        public void DAO_RemoveTest()
        {
            try
            {
                userProfileDao.Remove(userProfile.Id);

                bool userExists = userProfileDao.Exists(userProfile.Id);

                Assert.IsFalse(userExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Create
        ///</summary> 
        [TestMethod()]
        public void DAO_CreateTest()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                User newUserProfile = new User();
                newUserProfile.Login = "login";
                newUserProfile.Password = "password";
                newUserProfile.FirstName = "John";
                newUserProfile.LastName = "Smith";
                newUserProfile.Email = "john.smith@acme.com";
                newUserProfile.Language = "en";
                newUserProfile.Country = "US";

                userProfileDao.Create(newUserProfile);

                bool userExists = userProfileDao.Exists(newUserProfile.Id);

                Assert.IsTrue(userExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for Attach
        ///</summary>
        [TestMethod()]
        public void DAO_AttachTest()
        {
            User user = userProfileDao.Find(userProfile.Id);
            userProfileDao.Remove(user.Id);   // removes the user created in MyTestInitialize();

            // First we get CommonContext from GenericDAO...
            DbContext dbContext = ((GenericDaoEntityFramework<User, Int64>)userProfileDao).Context;

            // Check the user is not in the context now (EntityState.Detached notes that entity is not tracked by the context)
            Assert.AreEqual(dbContext.Entry(user).State, EntityState.Detached);

            // If we attach the entity it will be tracked again
            userProfileDao.Attach(user);


            // EntityState.Unchanged = entity exists in context and in DataBase with the same values 
            Assert.AreEqual(dbContext.Entry(user).State, EntityState.Unchanged);

        }
    }

}