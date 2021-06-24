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
using Es.Udc.DotNet.Photogram.Model;
using System.Collections.Generic;
using System.Linq;

namespace Es.Udc.DotNet.Photogram.Test
{
    /// <summary>
    /// This is a test class for IUserServiceTest and is intended to contain all IUserServiceTest
    /// Unit Tests
    /// </summary>
    [TestClass]
    public class IImageServiceTest
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
        public void CreateImageTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user and find profile
                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var imageId = imageService.CreateImage("titulo",
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId , 0));

                var imageProfile = imageProfileDao.Find(imageId);

                // Check data
                Assert.AreEqual(imageId, imageProfile.imageId);
                Assert.AreEqual("titulo", imageProfile.title);
                Assert.AreEqual("description", imageProfile.description);
                Assert.AreEqual(new DateTime(2008, 5, 1, 8, 30, 52), imageProfile.date);
                Assert.AreEqual("de", imageProfile.exifInfo);
                Assert.AreEqual(categoryId, imageProfile.categoryId);
                Assert.AreEqual(userId, imageProfile.userId);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for FindUserProfileDetails
        /// </summary>
        [TestMethod]
        public void FindImageProfileDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var expected =
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0);

                var imageId = imageService.CreateImage("titulo", expected);

                var obtained =
                    imageService.FindImageProfileDetails(imageId);

                // Check data
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for FindUserProfileDetails
        /// </summary>
        [TestMethod]
        public void FindImageByTextTitleTest()
        {
            using (var scope = new TransactionScope())
            {
                int count = 10;
                int startIndex = 0;

                List<ImageInfo> list = new List<ImageInfo>();

                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var image1 =
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0);

                var image2 =
                    new ImageProfile("titulo2", "descriptioooooon", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0);

                var image3 =
                    new ImageProfile("lllll", "jajajajajajaj", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0);

                var imageId = imageService.CreateImage("titulo", image1);

                var imageId2 = imageService.CreateImage("titulo2", image2);

                var imageId3 = imageService.CreateImage("lllll", image3);

                var obtained =
                    imageService.FindImages("titu", "sofa", false, startIndex, count);
                
                list.Add(new ImageInfo(imageId, "titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, "sofa", 0, userId, firstName));
                list.Add(new ImageInfo(imageId2, "titulo2", "descriptioooooon", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, "sofa", 0, userId, firstName));

                ImageBlock block = new ImageBlock(list, true);
                // Check data, same size? same elements?

                Assert.IsTrue(block.Images.Count == obtained.Images.Count && !block.Images.Except(obtained.Images).Any());
            }
        }

        /// <summary>
        /// A test for FindUserProfileDetails
        /// </summary>
        [TestMethod]
        public void FindImageByTextDescriptionTest()
        {
            using (var scope = new TransactionScope())
            {
                int count = 10;
                int startIndex = 0;

                List<ImageInfo> list = new List<ImageInfo>();

                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var image1 =
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0);

                var image2 =
                    new ImageProfile("titulo2", "descriptioooooon", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0);

                var image3 =
                    new ImageProfile("lllll", "jajajajajajaj", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0);

                var imageId = imageService.CreateImage("titulo", image1);

                var imageId2 = imageService.CreateImage("titulo2", image2);

                var imageId3 = imageService.CreateImage("lllll", image3);

                var obtained =
                    imageService.FindImages("descripti", "sofa", false, startIndex, count);

                list.Add(new ImageInfo(imageId, "titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, "sofa", 0, userId, firstName));
                list.Add(new ImageInfo(imageId2, "titulo2", "descriptioooooon", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, "sofa", 0, userId, firstName));

                ImageBlock block = new ImageBlock(list, true);
                // Check data, same size? same elements?

                Assert.IsTrue(block.Images.Count == obtained.Images.Count && !block.Images.Except(obtained.Images).Any());

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for FindUserProfileDetails
        /// </summary>
        [TestMethod]
        public void FindImageByTextCategoryTest()
        {
            using (var scope = new TransactionScope())
            {
                int count = 10;
                int startIndex = 0;

                List<ImageInfo> list = new List<ImageInfo>();

                var userId =
                    userService.CreateUser(loginName, clearPassword,
                        new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var categoryId2 = categoryService.CreateCategory("cena");

                var image1 =
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0);

                var image2 =
                    new ImageProfile("titulo2", "descriptioooooon", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId2, userId, 0);

                var image3 =
                    new ImageProfile("lllll", "jajajajajajaj", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0);

                var imageId = imageService.CreateImage("titulo", image1);

                var imageId2 = imageService.CreateImage("titulo2", image2);

                var imageId3 = imageService.CreateImage("lllll", image3);

                var obtained =
                    imageService.FindImages("titu", "cena", true, startIndex, count);

                list.Add(new ImageInfo(imageId2, "titulo2", "descriptioooooon", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId2, "cena", 0, userId, firstName));

                ImageBlock block = new ImageBlock(list, true);
                // Check data, same size? same elements?
                Console.WriteLine("Obtained");
                for (int i = 0; i < obtained.Images.Count; i++)
                    Console.WriteLine(obtained.Images[i].ToString());
                Console.WriteLine(obtained.existMoreImages);

                Console.WriteLine("expected");
                for (int i = 0; i < block.Images.Count; i++)
                    Console.WriteLine(block.Images[i].ToString());
                Console.WriteLine(obtained.existMoreImages);
                Assert.IsTrue(block.Images.Count == obtained.Images.Count && !block.Images.Except(obtained.Images).Any());

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test to check if a not valid user loginame is found
        /// </summary>
        [TestMethod]
        public void ImageExistsForNotValidImage()
        {
            using (var scope = new TransactionScope())
            {
                bool imageExists = imageService.ImageExists(-1);

                Assert.IsFalse(imageExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test to check if a not valid user loginame is found
        /// </summary>
        [TestMethod]
        public void ImageExistsForValidImage()
        {
            using (var scope = new TransactionScope())
            {
                var userId =
                    userService.CreateUser(loginName, clearPassword,
                    new UserProfile(firstName, lastName, email, language, country));

                var categoryId = categoryService.CreateCategory("sofa");

                var imageId = imageService.CreateImage("titulo",
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", categoryId, userId, 0));

                bool imageExists = imageService.ImageExists(imageId);

                Assert.IsTrue(imageExists);

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

