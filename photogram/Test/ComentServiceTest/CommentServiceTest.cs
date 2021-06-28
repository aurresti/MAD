using Es.Udc.DotNet.Photogram.Model.CommentDao;
using Es.Udc.DotNet.Photogram.Model.CommentService;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.Photogram.Model.CategoryService;
using Es.Udc.DotNet.Photogram.Model.UserService.Exceptions;
using Es.Udc.DotNet.Photogram.Model.UserService.Util;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Transactions;
using Castle.Core;
using System.Collections.Generic;
using Es.Udc.DotNet.Photogram.Model;
using System.Linq;
using System.Web.UI;

namespace Es.Udc.DotNet.Photogram.Test
{
    /// <summary>
    /// This is a test class for IUserServiceTest and is intended to contain all IUserServiceTest
    /// Unit Tests
    /// </summary>
    [TestClass]
    public class ICommentServiceTest
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
        private static ICommentService commentService;
        private static ICommentDao commentProfileDao;
        private static IImageService imageService;
        private static IImageDao imageProfileDao;
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
        public void CreateCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user and find profile
                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var imageId = imageService.CreateImage("titulo",
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0, ""));

                var commentId = commentService.AddComment(userId, imageId, "text");

                var commentProfile = commentProfileDao.Find(commentId);
                // Check data
                Assert.AreEqual(commentId, commentProfile.commentId);
                Assert.AreEqual(userId, commentProfile.userId);
                Assert.AreEqual(imageId, commentProfile.imageId);
                Assert.AreEqual("text", commentProfile.comment1);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for RegisterUser
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void CreateCommentImageNoExistsTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user and find profile
                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var imageId = imageService.CreateImage("titulo",
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0, ""));

                var commentId = commentService.AddComment(userId, -1, "text");



                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for RegisterUser
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void CreateCommentUserNoExistsTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user and find profile
                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var imageId = imageService.CreateImage("titulo",
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0, ""));

                var commentId = commentService.AddComment(-1, imageId, "text");



                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for FindUserProfileDetails
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void RemoveCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var expected =
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0,"");

                var imageId = imageService.CreateImage("titulo", expected);

                var commentId = commentService.AddComment(userId, imageId, "text");

                commentService.RemoveComment(userId, imageId, commentId);

                var commentProfile = commentProfileDao.Find(commentId);
                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for FindUserProfileDetails
        /// </summary>
        [TestMethod]
        public void UpdateCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var expected =
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0,"");

                var imageId = imageService.CreateImage("titulo", expected);

                var obtained =
                    imageService.FindImages("titulo", "sofa", false,0,10);

                var commentId = commentService.AddComment(userId, imageId, "text");
                commentService.UpdateComment(userId, imageId, commentId, "texto");

                var commentProfile = commentProfileDao.Find(commentId);
                // Check data
                Assert.AreEqual("texto", commentProfile.comment1);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for FindUserProfileDetails
        /// </summary>
        [TestMethod]
        public void SeeCommentsImageTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var expected =
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0, "");

                var imageId = imageService.CreateImage("titulo", expected);

                var commentId = commentService.AddComment(userId, imageId, "text");

                var commentId2 = commentService.AddComment(userId, imageId, "texto");

                var obtained =
                    imageService.SeeComments("titulo");

                // Check data
                Assert.AreEqual(commentId, obtained[0].commentId);
                Assert.AreEqual(commentId2, obtained[1].commentId);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for FindUserProfileDetails
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void SeeCommentsImageNoExistTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var expected =
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0, "");

                var imageId = imageService.CreateImage("titulo", expected);

                var obtained =
                    imageService.SeeComments("titulo2");

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for GetComments
        /// </summary>
        [TestMethod]
        public void SeeAllCommentsImageTest()
        {
            using (var scope = new TransactionScope())
            {
                List<Pair<Comment, UserAccount>> list = new List<Pair<Comment, UserAccount>>();

                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var image =
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0, "");

                var imageId = imageService.CreateImage("titulo", image);

                var commentId = commentService.AddComment(userId, imageId, "text");

                var commentId2 = commentService.AddComment(userId, imageId, "jajajajajaj ");

                var obtained = commentService.GetComments(imageId);

                list.Add(new Pair<Comment, UserAccount>(commentProfileDao.Find(commentId), userProfileDao.Find(userId)));
                list.Add(new Pair<Comment, UserAccount>(commentProfileDao.Find(commentId2), userProfileDao.Find(userId)));

                // Check data, same size? same elements?
                Assert.IsTrue(list.Count == obtained.Count && !list.Except(obtained).Any());

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }
        /// <summary>
        /// A test for IsCommentsByUser
        /// </summary>
        [TestMethod]
        public void IsCommentByUserTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var image =
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0, "");

                var imageId = imageService.CreateImage("titulo", image);

                var commentId = commentService.AddComment(userId, imageId, "text");

                var commentId2 = commentService.AddComment(userId, imageId, "jajajajajaj ");

                var obtained = commentService.GetComments(imageId);


                // Check data, same size? same elements?
                Assert.IsTrue(commentService.IsCommentsByUser(commentId,userId));

                // transaction.Complete() is not called, so Rollback is executed.
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
            categoryProfileDao = kernel.Get<ICategoryDao>();
            categoryService = kernel.Get<ICategoryService>();
            imageProfileDao = kernel.Get<IImageDao>();
            imageService = kernel.Get<IImageService>();
            commentProfileDao = kernel.Get<ICommentDao>();
            commentService = kernel.Get<ICommentService>();
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