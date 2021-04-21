using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
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
        /// Finds a Image by his title
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public List<Image> FindByText(String texto)
        {
            List<Image> imageProfile = null;

            #region Option 1: Using Linq.

            DbSet<Image> imageProfiles = Context.Set<Image>();

            var result =
                (from u in imageProfiles
                 where u.title == texto || u.description == texto
                 select u);

            imageProfile = result.ToList();

            #endregion Option 1: Using Linq.


            if (imageProfile == null)
                throw new InstanceNotFoundException(texto,
                    typeof(Image).FullName);

            return imageProfile;
        }

        /// <summary>
        /// Finds a Image by his title and description of a category
        /// </summary>
        /// <param name="title"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public List<Image> FindByCategory(string title, string category)
        {
            List<Image> imageProfile = null;

            Category categoryId = null;

            #region Option 1: Using Linq.

            DbSet<Category> categoria = Context.Set<Category>();

            var resultc =
                (from u in categoria
                 where u.name == category
                 select u);

            categoryId = resultc.FirstOrDefault();

            DbSet<Image> imageProfiles = Context.Set<Image>();

            var result =
                (from u in imageProfiles
                 where u.title == title || u.description == title && u.categoryId == categoryId.categoryId
                 select u);

            imageProfile = result.ToList();

            #endregion Option 1: Using Linq.


            if (imageProfile == null)
                throw new InstanceNotFoundException(title,
                    typeof(Image).FullName);

            return imageProfile;
        }

        /// <summary>
        /// Finds a Image by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public List<Comment> FindComments(long id)
        {
            List<Comment> comentarioProfile = null;

            #region Option 1: Using Linq.

            DbSet<Comment> comentarioProfiles = Context.Set<Comment>();

            var result =
                (from u in comentarioProfiles
                 where u.imageId == id orderby u.date
                 select u);

            comentarioProfile = result.ToList();

            #endregion Option 1: Using Linq.


            if (comentarioProfile == null)
                throw new InstanceNotFoundException(id,
                    typeof(Image).FullName);

            return comentarioProfile;
        }

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


        public void AddLikeDao(UserAccount user, long imageId)
        {
            Image c = Find(imageId);
            if (c != null && user != null)
            {
                /*Like comment = new Comment();
                comment.userId = user.userId;
                comment.imageId = imageId;*/
                //Comment.Add(comment);
                String query = "INSERT INTO Like(imageId, userId)" +
                     "VALUES(" + imageId + ", " + user.userId + ")";
            }
            Update(c);
        }
        #endregion IImagenDao Members
    }
}