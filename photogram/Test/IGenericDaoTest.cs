using Es.Udc.DotNet.Photogram.Model;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.CategoryDao;
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
        private UserAccount userProfile;
        private static IUserDao userProfileDao;
        private Category categoryProfile;
        private static ICategoryDao categoryProfileDao;

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
            categoryProfileDao = kernel.Get<ICategoryDao>();
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
            using (var scope = new TransactionScope())
            {
                userProfile = new UserAccount();
                userProfile.loginName = "jsmith";
                userProfile.password = "password";
                userProfile.firstName = "John";
                userProfile.lastName = "Smith";
                userProfile.email = "jsmith@acme.com";
                userProfile.language = "en";
                userProfile.country = "US";

                userProfileDao.Create(userProfile);

                categoryProfile = new Category();
                categoryProfile.name = "moda";

                categoryProfileDao.Create(categoryProfile);
            }
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            try
            {
                userProfileDao.Remove(userProfile.userId);
                categoryProfileDao.Remove(categoryProfile.categoryId);
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
                UserAccount actual = userProfileDao.Find(userProfile.userId);

                Assert.AreEqual(userProfile, actual, "User found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod()]
        public void DAO_FindTestC()
        {
            try
            {
                Category actual = categoryProfileDao.Find(categoryProfile.categoryId);

                Assert.AreEqual(categoryProfile, actual, "User found does not correspond with the original one.");
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
                bool userExists = userProfileDao.Exists(userProfile.userId);

                Assert.IsTrue(userExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod()]
        public void DAO_ExistsTestC()
        {
            try
            {
                bool categoryExists = categoryProfileDao.Exists(categoryProfile.categoryId);

                Assert.IsTrue(categoryExists);
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

        [TestMethod()]
        public void DAO_NotExistsTestC()
        {
            try
            {
                bool categoryNotExists = categoryProfileDao.Exists(-1);

                Assert.IsFalse(categoryNotExists);
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
                userProfile.firstName = "Juan";
                userProfile.lastName = "González";
                userProfile.email = "jgonzalez@acme.es";
                userProfile.language = "es";
                userProfile.country = "ES";
                userProfile.password = "contraseña";

                userProfileDao.Update(userProfile);

                UserAccount actual = userProfileDao.Find(userProfile.userId);

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
                userProfileDao.Remove(userProfile.userId);

                bool userExists = userProfileDao.Exists(userProfile.userId);

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
                UserAccount newUserProfile = new UserAccount();
                newUserProfile.loginName = "login";
                newUserProfile.password = "password";
                newUserProfile.firstName = "John";
                newUserProfile.lastName = "Smith";
                newUserProfile.email = "john.smith@acme.com";
                newUserProfile.language = "en";
                newUserProfile.country = "US";

                userProfileDao.Create(newUserProfile);

                bool userExists = userProfileDao.Exists(newUserProfile.userId);

                Assert.IsTrue(userExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for Create
        ///</summary> 
        [TestMethod()]
        public void DAO_CreateTestC()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                Category newCategoryProfile = new Category();
                newCategoryProfile.name = "login";

                categoryProfileDao.Create(newCategoryProfile);

                bool categoryExists = categoryProfileDao.Exists(newCategoryProfile.categoryId);

                Assert.IsTrue(categoryExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for Attach
        ///</summary>
        [TestMethod()]
        public void DAO_AttachTest()
        {
            UserAccount user = userProfileDao.Find(userProfile.userId);
            userProfileDao.Remove(user.userId);   // removes the user created in MyTestInitialize();

            // First we get CommonContext from GenericDAO...
            DbContext dbContext = ((GenericDaoEntityFramework<UserAccount, Int64>)userProfileDao).Context;

            // Check the user is not in the context now (EntityState.Detached notes that entity is not tracked by the context)
            Assert.AreEqual(dbContext.Entry(user).State, EntityState.Detached);

            // If we attach the entity it will be tracked again
            userProfileDao.Attach(user);


            // EntityState.Unchanged = entity exists in context and in DataBase with the same values 
            Assert.AreEqual(dbContext.Entry(user).State, EntityState.Unchanged);

        }

        /// <summary>
        ///A test for Attach
        ///</summary>
        [TestMethod()]
        public void DAO_AttachTestC()
        {
            Category category = categoryProfileDao.Find(categoryProfile.categoryId);
            categoryProfileDao.Remove(category.categoryId);   // removes the user created in MyTestInitialize();

            // First we get CommonContext from GenericDAO...
            DbContext dbContext = ((GenericDaoEntityFramework<Category, long>)categoryProfileDao).Context;

            // Check the user is not in the context now (EntityState.Detached notes that entity is not tracked by the context)
            Assert.AreEqual(dbContext.Entry(category).State, EntityState.Detached);

            // If we attach the entity it will be tracked again
            categoryProfileDao.Attach(category);


            // EntityState.Unchanged = entity exists in context and in DataBase with the same values 
            Assert.AreEqual(dbContext.Entry(category).State, EntityState.Unchanged);

        }
    }

}