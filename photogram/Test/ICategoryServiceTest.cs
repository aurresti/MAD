using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.Photogram.Model.CategoryService;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Transactions;
using System.Collections.Generic;
using Es.Udc.DotNet.Photogram.Model;
using System.Linq;

namespace Es.Udc.DotNet.Photogram.Test
{
    /// <summary>
    /// This is a test class for IUserServiceTest and is intended to contain all IUserServiceTest
    /// Unit Tests
    /// </summary>
    [TestClass]
    public class ICategoryServiceTest
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
        private static ICategoryService categoryService;
        private static ICategoryDao categoryProfileDao;

        private TransactionScope transaction;

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the
        /// current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// A test for RegisterUser
        /// </summary>
        [TestMethod]
        public void RegisterCategoryTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user and find profile
                var categoryId =
                    categoryService.CreateCategory(loginName);

                var categoryProfile = categoryProfileDao.Find(categoryId);

                // Check data
                Assert.AreEqual(categoryId, categoryProfile.categoryId);
                Assert.AreEqual(loginName, categoryProfile.name);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for registering a user that already exists in the database
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void RegisterDuplicatedCategoryTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                categoryService.CreateCategory(loginName);

                // Register the same user
                categoryService.CreateCategory(loginName);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for FindUserProfileDetails when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindCategoryProfileDetailsForNonExistingCategoryTest()
        {
            categoryService.FindCategory("mesa");
        }

        /// <summary>
        /// A test for FindUserProfileDetails when the user does not exist
        /// </summary>
        [TestMethod]
        public void FindCategoryProfileDetailsForExistingCategoryTest()
        {
            using (var scope = new TransactionScope())
            {
                var categoryId =
                    categoryService.CreateCategory(loginName);

                var expected =
                    categoryProfileDao.Find(categoryId);

                var obtained = categoryService.FindCategory(loginName);

                // Check data
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test to check if a not valid user loginame is found
        /// </summary>
        [TestMethod]
        public void CategoryExistsForNotValidCategory()
        {
            using (var scope = new TransactionScope())
            {
                String invalidLoginName = loginName + "_someFakeUserSuffix";

                bool categoryExists = categoryService.CategoryExists(-1);

                Assert.IsFalse(categoryExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test to check if a not valid user loginame is found
        /// </summary>
        [TestMethod]
        public void CategoryExistsForValidCategory()
        {
            using (var scope = new TransactionScope())
            {
                var categoryId =
                    categoryService.CreateCategory(loginName);

                bool categoryExists = categoryService.CategoryExists(categoryId);

                Assert.IsTrue(categoryExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test to check all Categories
        /// </summary>
        [TestMethod]
        public void FindAllCategoriesTest()
        {
            using (var scope = new TransactionScope())
            {
                List<Category> list = new List<Category>();
                Category a = new Category();
                Category b = new Category();
                Category c = new Category();

                var categoryId =
                    categoryService.CreateCategory("Nature");
                a.categoryId = categoryId;
                a.name = "Nature";

                var categoryId2 =
                    categoryService.CreateCategory("alga");
                b.categoryId = categoryId2;
                b.name = "alga";

                var categoryId3 =
                    categoryService.CreateCategory("Noche");
                c.categoryId = categoryId3;
                c.name = "Noche";

                var obtained = categoryService.FindCategories();

                list.Add(a); list.Add(b); list.Add(c);

                Assert.AreEqual(obtained[2].name,list[2].name);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            categoryProfileDao = kernel.Get<ICategoryDao>();
            categoryService = kernel.Get<ICategoryService>();
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
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
        }

        #endregion Additional test attributes
    }
}

