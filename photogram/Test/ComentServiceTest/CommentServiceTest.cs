﻿using Es.Udc.DotNet.Photogram.Model;
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

namespace Es.Udc.DotNet.Photogram.Test.CommentServiceTest
{
    [TestClass()]
    public class CommentServiceTest
    {
        private static IKernel kernel;
        private static IImageService imageService;
        private static ICategoryDao categoryDao;
        private static IUserDao userDao;
        private static IUserService userService;


        // Variables used in several tests are initialized here

        private TransactionScope transactionScope;

        private TestContext testContextInstance;
        private static ICategoryService categoryService;
        private static IImageDao imageDao;
        private static ICommentDao commentDao;
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
            commentDao = kernel.Get<ICommentDao>();
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
            // transactionScope = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            // transactionScope.Dispose();
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
        public void CreateSimpleCommentImage()
        {
            using (var scope = new TransactionScope())
            {
                long cat = GetValidCategory("Nature");
                Image i = GetValidImage("ImageName", cat);
                UserAccount user = GetValidUser("pepito.p");

                Comment comment = commentService.AddComment(user.userId, i.imageId, "que guapa esta foto");
                Comment actualComment = commentDao.Find(comment.commentId);

                Assert.IsTrue(comment.commentId == actualComment.commentId);

            }
        }

        [TestMethod()]
        public void RemoveSimpleComment()
        {
            using (var scope = new TransactionScope())
            {
                long cat = GetValidCategory("Nature");
                Image i = GetValidImage("ImageName", cat);
                UserAccount user = GetValidUser("pepito.p");
                Comment comment = commentService.AddComment(user.userId, i.imageId, "que guapa esta foto");

                commentService.RemoveComment(user.userId, i.imageId, comment.commentId);

                Assert.IsFalse(commentDao.Exists(comment.commentId));
            }
        }

        [TestMethod()]
        public void RemoveNoOwnerComment()
        {
            using (var scope = new TransactionScope())
            {
                long cat = GetValidCategory("Nature");
                Image i = GetValidImage("ImageName", cat);
                UserAccount user = GetValidUser("pepito.p");
                UserAccount user2 = GetValidUser("intruso.x");
                Comment comment = commentService.AddComment(user.userId, i.imageId, "que guapa esta foto");

                commentService.RemoveComment(user2.userId, i.imageId, comment.commentId);

                Assert.IsTrue(commentDao.Exists(comment.commentId));
            }
        }

        [TestMethod()]
        public void SimpleUpdateComment()
        {
            using (var scope = new TransactionScope())
            {
                long cat = GetValidCategory("Nature");
                Image i = GetValidImage("ImageName", cat);
                UserAccount user = GetValidUser("pepito.p");

                Comment comment = commentService.AddComment(user.userId, i.imageId, "que guapa esta foto");
                comment.comment1 = "cambie de opinion está horrorosa";

                commentService.UpdateComment(user.userId, i.imageId, comment);

                Comment actualComment = commentDao.Find(comment.commentId);

                Assert.IsTrue(comment.comment1.Equals(actualComment.comment1));
            }
        }

        public void SimpleUpdateNoOwnerComment()
        {
            using (var scope = new TransactionScope())
            {
                long cat = GetValidCategory("Nature");
                Image i = GetValidImage("ImageName", cat);
                UserAccount user = GetValidUser("pepito.p");
                UserAccount user1 = GetValidUser("intruso.p");


                Comment comment = commentService.AddComment(user.userId, i.imageId, "que guapa esta foto");
                comment.comment1 = "cambie de opinion está horrorosa";

                commentService.UpdateComment(user1.userId, i.imageId, comment);

                Comment actualComment = commentDao.Find(comment.commentId);

                Assert.IsFalse(comment.comment1.Equals(actualComment.comment1));
            }
        }

    }


}
