using Es.Udc.DotNet.Photogram.Model;
using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.Photogram.Model.CategoryService;
using Es.Udc.DotNet.Photogram.Model.CommentDao;
using Es.Udc.DotNet.Photogram.Model.CommentService;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.UserService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Transactions;
using static System.Net.Mime.MediaTypeNames;
using Image = Es.Udc.DotNet.Photogram.Model.Image;

namespace Es.Udc.DotNet.Photogram.Test.ImageServiceTest
{
    [TestClass()]
    public class ImageServiceTest
    {
        private static IKernel kernel;
        private static IImageService imageService;
        private static ICategoryDao categoryDao;
        private static IUserDao userDao;
        private static IUserService userService;


        // Variables used in several tests are initialized here
        private const long userId = 123456;

        private const long NON_EXISTENT_USER_ID = -2;
        private const long NOT_VALID_CATEGORY_ID = -3;

        private TransactionScope transactionScope;

        private TestContext testContextInstance;
        private static ICategoryService categoryService;
        private static IImageDao imageDao;
        private static ICommentDao commentProfileDao;
        private static ICommentService commentService;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {

            kernel = TestManager.ConfigureNInjectKernel();
            imageService = kernel.Get<IImageService>();
            categoryDao = kernel.Get<ICategoryDao>();
            userDao = kernel.Get<IUserDao>();
            userService = kernel.Get<IUserService>();
            categoryService = kernel.Get<ICategoryService>();
            imageDao = kernel.Get<IImageDao>();
            commentProfileDao = kernel.Get<ICommentDao>();
            commentService = kernel.Get<ICommentService>();

        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
           transactionScope = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
           transactionScope.Dispose();
        }

        private long GetValidCategory(String name)
        {
            Category c = new Category();
            c.name = name;
            categoryDao.Create(c);

            return c.categoryId;
        }

        private Image GetValidImage(String name)
        {
            Image e = new Image();
            e.title = name;
            e.description = "DescriptionImage";
            e.date = DateTime.Now;
            e.categoryId = GetValidCategory("Sport");
            imageService.UploadImage(e);

            return e;
        }

        private Image GetValidImage(String name, long categoryId)
        {
            Image e = new Image();
            e.title = name;
            e.description = "DescriptionImage";
            e.date = DateTime.Now;
            e.categoryId = categoryId;
            Image img = imageService.UploadImage(e);

            return img;
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
            userDao.Create(user);
            return user;
        }


        #endregion Additional test attributes

        [TestMethod()]
        public void SimpleLikeImage()
        {

                long cat = GetValidCategory("Nature");
                Image i = GetValidImage("ImageName", cat);
                UserAccount user = GetValidUser("pepito.p");

                imageService.AddLike(user.userId, i.imageId);


                ImageProfile info = imageService.FindImageProfileDetails(i.imageId);
                Assert.IsTrue(info.Likes == 1);

        }
        [TestMethod()]
        public void MultiplelikeUserImage()
        {
            using (var scope = new TransactionScope())
            {
                int count = 10;
                int startIndex = 0;

                List<ImageInfo> list = new List<ImageInfo>();

                var user = GetValidUser("pepito.p");

                var user2 = GetValidUser("pepito.pp");

                var user3 = GetValidUser("pepito.ppp");

                long cat = GetValidCategory("Nature");

                long cat2 = GetValidCategory("something");

                var image1 =
                    new ImageProfile("titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", cat, user.userId, 3, "");

                var imageId = imageService.CreateImage("titulo", image1);

                var image2 =
                    new ImageProfile("titulo2", "descriptioooooon", new DateTime(2008, 5, 1, 8, 30, 52), "de", cat, user.userId, 2, "");

                var imageId2 = imageService.CreateImage("titulo2", image2);

                var image3 =
                    new ImageProfile("titulo3", "jajajajajajaj", new DateTime(2008, 5, 1, 8, 30, 52), "de", cat2, user.userId, 1, "");

                var imageId3 = imageService.CreateImage("titulo3", image3);

                imageService.AddLike(user.userId, imageId);
                imageService.AddLike(user2.userId, imageId);
                imageService.AddLike(user3.userId, imageId);
                imageService.AddLike(user.userId, imageId2);
                imageService.AddLike(user2.userId, imageId2);
                imageService.AddLike(user3.userId, imageId3);

                var obtained =
                    imageService.FindImages("titu", "Nature", true, startIndex, count);

                list.Add(new ImageInfo(imageId, "titulo", "description", new DateTime(2008, 5, 1, 8, 30, 52), "de", cat, "Nature", 3, user.userId, user.firstName));
                list.Add(new ImageInfo(imageId, "titulo2", "descriptioooooon", new DateTime(2008, 5, 1, 8, 30, 52), "de", cat, "Nature", 2, user.userId, user.firstName));
                
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


                Assert.AreEqual(block, obtained);

                // transaction.Complete() is not called, so Rollback is executed.
            }

        }
    }
}
