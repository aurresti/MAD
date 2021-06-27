using Es.Udc.DotNet.Photogram.Model;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.Photogram.Model.CommentDao;
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
        private Image imageProfile;
        private static IImageDao imageProfileDao;
        private Category categoryProfile;
        private static ICategoryDao categoryProfileDao;
        private Comment commentProfile;
        private static ICommentDao commentProfileDao;

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
            imageProfileDao = kernel.Get<IImageDao>();
            commentProfileDao = kernel.Get<ICommentDao>();
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
            categoryProfile.name = "fauna";

            categoryProfileDao.Create(categoryProfile);

            imageProfile = new Image();
            imageProfile.Category =categoryProfile;
            imageProfile.date = new DateTime(2008, 5, 1, 8, 30, 52);
            imageProfile.description = "description";
            imageProfile.exifInfo = "ddsa";
            imageProfile.imageView = "url";
            imageProfile.title = "title";
            imageProfile.userId = userProfile.userId;

            imageProfileDao.Create(imageProfile);

            commentProfile = new Comment();
            commentProfile.comment1 = "comment";
            commentProfile.date = new DateTime(2008, 5, 1, 8, 30, 52);
            commentProfile.imageId = imageProfile.imageId;
            commentProfile.userId = userProfile.userId;

            commentProfileDao.Create(commentProfile);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            try
            {
                userProfileDao.Remove(userProfile.userId);
            }
            catch (Exception)
            {
            }
        }

        #endregion Additional test attributes

        /// <summary>
        ///A test for Find User
        ///</summary>
        [TestMethod()]
        public void DAO_FindUserTest()
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

        /// <summary>
        ///A test for Exists User
        ///</summary>
        [TestMethod()]
        public void DAO_ExistsUserTest()
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

        /// <summary>
        ///A test for Not Exists User
        ///</summary>
        [TestMethod()]
        public void DAO_NotExistsUserTest()
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
        ///A test for Update User
        ///</summary>
        [TestMethod()]
        public void DAO_UpdateUserTest()
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
        ///A test for Remove User
        [TestMethod()]
        public void DAO_RemoveUserTest()
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
        ///A test for Create User
        ///</summary> 
        [TestMethod()]
        public void DAO_CreateUserTest()
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
        ///A test for Attach User
        ///</summary>
        [TestMethod()]
        public void DAO_AttachUserTest()
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
        ///A test for Find Category
        ///</summary>
        [TestMethod()]
        public void DAO_FindCategoryTest()
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

        /// <summary>
        ///A test for Exists Category
        ///</summary>
        [TestMethod()]
        public void DAO_ExistsCategoryTest()
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

        /// <summary>
        ///A test for Non Exists Category
        ///</summary>
        [TestMethod()]
        public void DAO_NotExistsCategoryTest()
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
        ///A test for Remove Category
        [TestMethod()]
        public void DAO_RemoveCategoryTest()
        {
            try
            {
                categoryProfileDao.Remove(categoryProfile.categoryId);

                bool categoryExists = categoryProfileDao.Exists(categoryProfile.categoryId);

                Assert.IsFalse(categoryExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Create Category
        ///</summary> 
        [TestMethod()]
        public void DAO_CreateCategoryTest()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                Category newCategoryProfile = new Category();
                newCategoryProfile.name = "fauna";

                categoryProfileDao.Create(newCategoryProfile);

                categoryProfileDao.Create(newCategoryProfile);

                bool categoryExists = categoryProfileDao.Exists(newCategoryProfile.categoryId);

                Assert.IsTrue(categoryExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for Attach Category
        ///</summary>
        [TestMethod()]
        public void DAO_AttachCategoryTest()
        {
            Category category = categoryProfileDao.Find(categoryProfile.categoryId);
            categoryProfileDao.Remove(category.categoryId);   // removes the category created in MyTestInitialize();

            // First we get CommonContext from GenericDAO...
            DbContext dbContext = ((GenericDaoEntityFramework<Category, Int64>)categoryProfileDao).Context;

            // Check the user is not in the context now (EntityState.Detached notes that entity is not tracked by the context)
            Assert.AreEqual(dbContext.Entry(category).State, EntityState.Detached);

            // If we attach the entity it will be tracked again
            categoryProfileDao.Attach(category);


            // EntityState.Unchanged = entity exists in context and in DataBase with the same values 
            Assert.AreEqual(dbContext.Entry(category).State, EntityState.Unchanged);

        }

        /// <summary>
        ///A test for Find Image
        ///</summary>
        [TestMethod()]
        public void DAO_FindImageTest()
        {
            try
            {
                Image actual = imageProfileDao.Find(imageProfile.imageId);

                Assert.AreEqual(imageProfile, actual, "User found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Exists Image
        ///</summary>
        [TestMethod()]
        public void DAO_ExistsImageTest()
        {
            try
            {
                bool imageExists = imageProfileDao.Exists(imageProfile.imageId);

                Assert.IsTrue(imageExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Non Exists Image
        ///</summary>
        [TestMethod()]
        public void DAO_NotExistsImageTest()
        {
            try
            {
                bool imageNotExists = imageProfileDao.Exists(-1);

                Assert.IsFalse(imageNotExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Update Image
        ///</summary>
        [TestMethod()]
        public void DAO_UpdateImageTest()
        {
            try
            {
                imageProfile.description = "descriptionNew";
                imageProfile.exifInfo = "ddsaNew";
                imageProfile.title = "titleNew";

                imageProfileDao.Update(imageProfile);

                Image actual = imageProfileDao.Find(imageProfile.imageId);

                Assert.AreEqual(imageProfile, actual);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Remove Image
        [TestMethod()]
        public void DAO_RemoveImageTest()
        {
            try
            {
                imageProfileDao.Remove(imageProfile.imageId);

                bool imageExists = imageProfileDao.Exists(imageProfile.imageId);

                Assert.IsFalse(imageExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Create Image
        ///</summary> 
        [TestMethod()]
        public void DAO_CreateImageTest()
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

                Category newCategoryProfile = new Category();
                newCategoryProfile.name = "fauna";

                categoryProfileDao.Create(newCategoryProfile);

                Image newImageProfile = new Image();
                newImageProfile.Category = newCategoryProfile;
                newImageProfile.date = new DateTime(2008, 5, 1, 8, 30, 52);
                newImageProfile.description = "description";
                newImageProfile.exifInfo = "ddsa";
                newImageProfile.imageView = "url";
                newImageProfile.title = "title";
                newImageProfile.userId = newUserProfile.userId;

                imageProfileDao.Create(newImageProfile);
                bool imageExists = imageProfileDao.Exists(newImageProfile.imageId);

                Assert.IsTrue(imageExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for Attach Image
        ///</summary>
        [TestMethod()]
        public void DAO_AttachImageTest()
        {
            Image image = imageProfileDao.Find(imageProfile.imageId);
            imageProfileDao.Remove(image.imageId);   // removes the image created in MyTestInitialize();

            // First we get CommonContext from GenericDAO...
            DbContext dbContext = ((GenericDaoEntityFramework<Image, Int64>)imageProfileDao).Context;

            // Check the image is not in the context now (EntityState.Detached notes that entity is not tracked by the context)
            Assert.AreEqual(dbContext.Entry(image).State, EntityState.Detached);

            // If we attach the entity it will be tracked again
            imageProfileDao.Attach(image);


            // EntityState.Unchanged = entity exists in context and in DataBase with the same values 
            Assert.AreEqual(dbContext.Entry(image).State, EntityState.Unchanged);

        }

        /// <summary>
        ///A test for Find Comment
        ///</summary>
        [TestMethod()]
        public void DAO_FindCommentTest()
        {
            try
            {
                Comment actual = commentProfileDao.Find(commentProfile.commentId);

                Assert.AreEqual(commentProfile, actual, "User found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Exists Comment
        ///</summary>
        [TestMethod()]
        public void DAO_ExistsCommentTest()
        {
            try
            {
                bool commentExists = commentProfileDao.Exists(commentProfile.commentId);

                Assert.IsTrue(commentExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Non Exists Comment
        ///</summary>
        [TestMethod()]
        public void DAO_NotExistsCommentTest()
        {
            try
            {
                bool commentNotExists = commentProfileDao.Exists(-1);

                Assert.IsFalse(commentNotExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Update Comment
        ///</summary>
        [TestMethod()]
        public void DAO_UpdateCommentTest()
        {
            try
            {
                commentProfile.comment1 = "commentNew";
                commentProfile.date = DateTime.Now;

                commentProfileDao.Update(commentProfile);

                Comment actual = commentProfileDao.Find(commentProfile.commentId);

                Assert.AreEqual(commentProfile, actual);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Remove Comment
        [TestMethod()]
        public void DAO_RemoveCommentTest()
        {
            try
            {
                commentProfileDao.Remove(commentProfile.commentId);

                bool commentExists = commentProfileDao.Exists(commentProfile.commentId);

                Assert.IsFalse(commentExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Create Comment
        ///</summary> 
        [TestMethod()]
        public void DAO_CreateCommentTest()
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

                Category newCategoryProfile = new Category();
                newCategoryProfile.name = "fauna";

                categoryProfileDao.Create(newCategoryProfile);

                Image newImageProfile = new Image();
                newImageProfile.Category = newCategoryProfile;
                newImageProfile.date = new DateTime(2008, 5, 1, 8, 30, 52);
                newImageProfile.description = "description";
                newImageProfile.exifInfo = "ddsa";
                newImageProfile.imageView = "url";
                newImageProfile.title = "title";
                newImageProfile.userId = newUserProfile.userId;

                imageProfileDao.Create(newImageProfile);

                Comment newCommentProfile = new Comment();
                newCommentProfile.comment1 = "comment";
                newCommentProfile.date = new DateTime(2008, 5, 1, 8, 30, 52);
                newCommentProfile.imageId = newImageProfile.imageId;
                newCommentProfile.userId = newUserProfile.userId;

                commentProfileDao.Create(newCommentProfile);
                bool commentExists = commentProfileDao.Exists(newCommentProfile.commentId);

                Assert.IsTrue(commentExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for Attach Comment
        ///</summary>
        [TestMethod()]
        public void DAO_AttachCommentTest()
        {
            Comment comment = commentProfileDao.Find(commentProfile.commentId);
            commentProfileDao.Remove(comment.commentId);   // removes the comment created in MyTestInitialize();

            // First we get CommonContext from GenericDAO...
            DbContext dbContext = ((GenericDaoEntityFramework<Comment, Int64>)commentProfileDao).Context;

            // Check the image is not in the context now (EntityState.Detached notes that entity is not tracked by the context)
            Assert.AreEqual(dbContext.Entry(comment).State, EntityState.Detached);

            // If we attach the entity it will be tracked again
            commentProfileDao.Attach(comment);


            // EntityState.Unchanged = entity exists in context and in DataBase with the same values 
            Assert.AreEqual(dbContext.Entry(comment).State, EntityState.Unchanged);

        }
    }

}