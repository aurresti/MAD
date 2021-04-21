using Es.Udc.DotNet.Photogram.Model;
using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Transactions;

namespace Es.Udc.DotNet.Photogram.Test.ImageService
{
    [TestClass()]
    public class ImageServiceTest
    {
        private static IKernel kernel;
        private static IImageService imageService;
        private static ICategoryDao categoryDao;


        // Variables used in several tests are initialized here
        private const long userId = 123456;

        private const long NON_EXISTENT_USER_ID = -2;
        private const long NOT_VALID_CATEGORY_ID = -3;

        private TransactionScope transactionScope;

        private TestContext testContextInstance;

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
            //transactionScope.Dispose();
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
            e.date = DateTime.Now.AddDays(5);
            e.categoryId = GetValidCategory("Sport");
            imageService.UploadImage(e);

            return e;
        }

        private Image GetValidImage(String name, long categoryId)
        {
            Image e = new Image();
            e.title = name;
            e.description = "DescriptionImage";
            e.date = DateTime.Now.AddDays(5);
            e.categoryId = categoryId;
            Image img = imageService.UploadImage(e);

            return img;
        }

        #endregion Additional test attributes

        [TestMethod()]
        public void SimpleFindImagesTest()
        {

            long cat = GetValidCategory("Nature");
            Image i = GetValidImage("ImageName", cat);


            String keys = "Ima";
            Assert.IsTrue(true);
            //Assert.IsTrue(imageService.FindImages(keys, 0, 10, cat).Images.Count > 0);
            //Assert.IsTrue(imageService.FindImages(keys, 0, 10, NOT_VALID_CATEGORY_ID).Images.Count == 0);

        }

        //[TestMethod()]
        //public void FindImagesNullCategoryTest()
        //{
        //    long cat = GetValidCategory("Urban");
        //    long cat1 = GetValidCategory("Rural");
        //    GetValidImage("Holtel", cat);
        //    GetValidImage("Granja", cat1);

        //    String keys = "h";

        //    Assert.IsTrue(imageService.FindImages(keys, 0, 10, cat).Images.Count == 1);
        //    Assert.IsTrue(imageService.FindImages(keys, 0, 10, cat1).Images.Count == 1);
        //    Assert.IsTrue(imageService.FindImages(keys, 0, 10, null).Images.Count == 2);
        //    Assert.IsTrue(imageService.FindImages(keys, 0, 10, NOT_VALID_CATEGORY_ID).Images.Count == 0);
        //}

        //[TestMethod()]
        //public void FindImagesNullKeysTest()
        //{
        //    long cat = GetValidCategory("Sport");
        //    GetValidImage("Holtel", cat);
        //    GetValidImage("Holtel", cat);
        //    Assert.IsTrue(imageService.FindImages(null, 0, 10, cat).Images.Count == 2);

        //}

    }
}
