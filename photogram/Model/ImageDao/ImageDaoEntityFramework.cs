using Castle.Core;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.Photogram.Model.ImageDao
{
    public class ImageDaoEntityFramework : GenericDaoEntityFramework<Image, long>, IImageDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public ImageDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IImagenDao Members. Specific Operations

        /// <summary>
        /// Finds a Image by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Image</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public Image FindById(long id)
        {
            Image imageProfile = null;

            #region Option 1: Using Linq.

            DbSet<Image> imageProfiles = Context.Set<Image>();

            var result =
                (from u in imageProfiles
                 where u.imageId == id
                 select u);

            imageProfile = result.FirstOrDefault();

            #endregion Option 1: Using Linq.


            if (imageProfile == null)
                throw new InstanceNotFoundException(id,
                    typeof(Image).FullName);

            return imageProfile;
        }

        /// <summary>
        /// Finds a Image by his title
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Image</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public Image FindByTitle(String title)
        {
            Image imageProfile = null;

            #region Option 1: Using Linq.

            DbSet<Image> imageProfiles = Context.Set<Image>();

            var result =
                (from u in imageProfiles
                 where u.title == title
                 select u);

            imageProfile = result.FirstOrDefault();

            #endregion Option 1: Using Linq.


            if (imageProfile == null)
                throw new InstanceNotFoundException(title,
                    typeof(Image).FullName);

            return imageProfile;
        }

        /// <summary>
        /// Finds a Image by text
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns>Image list with text contain in title or description</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public List<ImageInfo> FindByText(String texto, int startIndex, int count)
        {
            
            List<ImageInfo> imageProfile = new List<ImageInfo>();

            List<Image> imageList = null;

            List<Category> categoryList = null;

            #region Option 1: Using Linq.

            DbSet<Image> imageLists = Context.Set<Image>();

            DbSet<Category> CategoryLists = Context.Set<Category>();

            var result =
                (from u in imageLists
                 where (u.title.Contains(texto) || u.description.Contains(texto))
                 orderby u.imageId
                 select u);

            imageList = result.Skip(startIndex).Take(count).ToList();

            var result2 =
                (from u in imageLists
                 from c in CategoryLists
                 where (u.title.Contains(texto) || u.description.Contains(texto))
                 where u.categoryId == c.categoryId
                 orderby u.imageId
                 select c);

            categoryList = result2.Skip(startIndex).Take(count).ToList();

            #endregion Option 1: Using Linq.

            ///Como accedemos a la categoria? excepcion carga perezosa, paginacion

            if (imageProfile == null)
                throw new InstanceNotFoundException(texto,
                    typeof(Image).FullName);

            for (int i = 0; i < imageList.Count; i++)
                imageProfile.Add(new ImageInfo(imageList[i].imageId, imageList[i].title, imageList[i].description, imageList[i].date,
                    imageList[i].exifInfo, categoryList[i].categoryId, categoryList[i].name, imageList[i].UserAccounts.Count, 
                    imageList[i].UserAccount.userId, imageList[i].UserAccount.firstName, imageList[i].imageView));

            return imageProfile;
        }

        /// <summary>
        /// Finds Images by his userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of Images</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public List<Image> FindByUserId(long userId)
        {
            List<Image> imageProfile = null;

            #region Option 1: Using Linq.

            DbSet<Image> imageProfiles = Context.Set<Image>();

            var result =
                (from u in imageProfiles
                 where u.userId == userId
                 orderby u.date descending
                 select u);

            imageProfile = result.ToList();

            #endregion Option 1: Using Linq.


            if (imageProfile == null)
                throw new InstanceNotFoundException(userId,
                    typeof(Image).FullName);

            return imageProfile;
        }

        /// <summary>
        /// Finds a Image by his title or description of a category
        /// </summary>
        /// <param name="text"></param>
        /// <param name="category"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns>List of Images</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public List<ImageInfo> FindByCategory(string text, string category, int startIndex, int count)
        {
            List<ImageInfo> imageProfile = new List<ImageInfo>();

            List<Image> imageList = null;

            List<Category> categoryList = null;

            #region Option 1: Using Linq.

            DbSet<Image> imageLists = Context.Set<Image>();

            DbSet<Category> CategoryLists = Context.Set<Category>();

            imageList =
                (from u in imageLists
                 from c in CategoryLists
                 where (u.title.Contains(text) || u.description.Contains(text)) && c.name == category
                 where u.categoryId == c.categoryId
                 orderby u.imageId
                 select u).Skip(startIndex).Take(count).ToList();

            categoryList =
                (from u in imageLists
                 from c in CategoryLists
                 where (u.title.Contains(text) || u.description.Contains(text)) && c.name == category
                 where u.categoryId == c.categoryId
                 orderby u.imageId
                 select c).Skip(startIndex).Take(count).ToList();


            #endregion Option 1: Using Linq.

            ///Como accedemos a la categoria? excepcion carga perezosa, paginacion

            if (imageProfile == null)
                throw new InstanceNotFoundException(text,
                    typeof(Image).FullName);

            for (int i = 0; i < imageList.Count; i++)
                imageProfile.Add(new ImageInfo(imageList[i].imageId, imageList[i].title, imageList[i].description , imageList[i].date,
                    imageList[i].exifInfo, categoryList[i].categoryId, categoryList[i].name, imageList[i].UserAccounts.Count,
                    imageList[i].UserAccount.userId, imageList[i].UserAccount.firstName, imageList[i].imageView));

            return imageProfile;
        }

        /// <summary>
        /// Finds Comments by his imageId
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public List<Comment> FindComments(long imageId)
        {
            List<Comment> comentarioProfile = null;

            #region Option 1: Using Linq.

            DbSet<Comment> comentarioProfiles = Context.Set<Comment>();

            var result =
                (from u in comentarioProfiles
                 where u.imageId == imageId orderby u.date
                 select u);

            comentarioProfile = result.ToList();

            #endregion Option 1: Using Linq.


            if (comentarioProfile == null)
                throw new InstanceNotFoundException(imageId,
                    typeof(Image).FullName);

            ///Como accedemos al nombre? excepcion carga perezosa

            return comentarioProfile;
        }

        /// <summary>
        /// Add a Comment
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageId"></param>
        /// <param name="description"></param>
        /// <returns>Comment added to database</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public void AddCommentDao(UserAccount user, long imageId, String description)
        {
            Image c = Find(imageId);
            if (c != null && user != null)
            {
                Comment comment= new Comment();
                comment.userId = user.userId;
                comment.imageId = imageId;
                comment.comment1 = description; 
                comment.date = new DateTime(2008, 5, 1, 8, 30, 52);
                //Comment.Add(comment);
               String query = "INSERT INTO Comment(imageId, userId, comment1, date)" + 
                    "VALUES("+ imageId + ", "+ user.userId + ", "+ description + ", "+ comment.date +")";
            }
            Update(c);
        }

        /// <summary>
        /// Remove a Comment
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageId"></param>
        /// <returns>Comment removed to database</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public void RemoveCommentDao(UserAccount user, long imageId)
        {
            Image c = Find(imageId);
            if (c != null && user != null)
            {
                Comment comment = new Comment();
                comment.userId = user.userId;
                comment.imageId = imageId;
                //Comment.Remove(comment);
                //Suposicion, non sei como vai
                String query = "REMOVE INTO Comment(imageId, userId, comment1, date)" +
                     "VALUES(" + imageId + ", " + user.userId + ")";
            }
            Update(c);
        }

        /// <summary>
        /// Add a Like
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageId"></param>
        /// <returns>Like added to database</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public void AddLikeDao(long userId, long imageId)
        {
            UserAccount userAccount;
            Image image;

            DbSet<UserAccount> userProfiles = Context.Set<UserAccount>();

            DbSet<Image> imageProfiles = Context.Set<Image>();

            try
            {
                userAccount =
                    (from u in userProfiles
                     where u.userId == userId
                     select u).Single();
            }
            catch (Exception e)
            {
                throw new InstanceNotFoundException(userId, typeof(UserAccount).FullName);
            }
            try
            {
                image =
                     (from u in imageProfiles
                      where u.imageId == imageId
                      select u).Single();
            }
            catch (Exception e)
            {
                throw new InstanceNotFoundException(imageId, typeof(UserAccount).FullName);
            }

            //Console.Write("usuario " + user.loginName + " is been followed by " + follow.loginName + "\n");

            image.UserAccounts.Add(userAccount);

            Context.SaveChanges();
        }

        /// <summary>
        /// Remove a Like
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageId"></param>
        /// <returns>Like removed to database</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public void RemoveLikeDao(long userId, long imageId)
        {
            UserAccount userAccount;
            Image image;

            DbSet<UserAccount> userProfiles = Context.Set<UserAccount>();

            DbSet<Image> imageProfiles = Context.Set<Image>();

            try
            {
                userAccount =
                    (from u in userProfiles
                     where u.userId == userId
                     select u).Single();
            }
            catch (Exception e)
            {
                throw new InstanceNotFoundException(userId, typeof(UserAccount).FullName);
            }
            try
            {
                image =
                     (from u in imageProfiles
                      where u.UserAccounts.Contains(userAccount)
                      select u).Single();
            }
            catch (Exception e)
            {
                throw new InstanceNotFoundException(imageId, typeof(UserAccount).FullName);
            }

            //Console.Write("usuario " + user.loginName + " is been followed by " + follow.loginName + "\n");

            image.UserAccounts.Remove(userAccount);

            Context.SaveChanges();
        }

        /// <summary>
        /// Find a Like
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageId"></param>
        /// <returns>Bool about find like in the database</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public bool FindLikeDao(long userId, long imageId)
        {
            UserAccount userAccount;
            Image image;

            DbSet<UserAccount> userProfiles = Context.Set<UserAccount>();

            DbSet<Image> imageProfiles = Context.Set<Image>();

            try
            {
                userAccount =
                    (from u in userProfiles
                     where u.userId == userId
                     select u).Single();
            }
            catch (Exception e)
            {
                throw new InstanceNotFoundException(userId, typeof(UserAccount).FullName);
            }
            try
            {
                image =
                     (from u in imageProfiles
                      where u.UserAccounts.Contains(userAccount)
                      select u).Single();
            }
            catch (Exception e)
            {
                throw new InstanceNotFoundException(imageId, typeof(UserAccount).FullName);
            }

            //Console.Write("usuario " + user.loginName + " is been followed by " + follow.loginName + "\n");

            if (image.UserAccounts.Count >= 1)
            {
                return true;
            }
            else {
                return false;
            }

        }
        #endregion IImagenDao Members
    }
}