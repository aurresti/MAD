using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.Photogram.Model.CommentDao
{
    public class CommentDaoEntityFramework : GenericDaoEntityFramework<Comment, long>, ICommentDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public CommentDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IImagenDao Members. Specific Operations

        /// <summary>
        /// Finds a Image by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public List<Comment> FindComments(int imageId)
        {
            List<Comment> comentarioProfile = null;

            #region Option 1: Using Linq.

            DbSet<Comment> comentarioProfiles = Context.Set<Comment>();

            var result =
                (from u in comentarioProfiles
                 where u.imageId == imageId
                 orderby u.date
                 select u);

            comentarioProfile = result.ToList();

            #endregion Option 1: Using Linq.


            if (comentarioProfile == null)
                throw new InstanceNotFoundException(imageId,
                    typeof(Image).FullName);

            return comentarioProfile;
        }

        /// <summary>
        /// Finds a Image by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public Comment FindComment(long userId, long imageId)
        {
            Comment comentarioProfile = null;

            #region Option 1: Using Linq.

            DbSet<Comment> comentarioProfiles = Context.Set<Comment>();

            var result =
                (from u in comentarioProfiles
                 where u.imageId == imageId && u.userId == userId
                 orderby u.date
                 select u);

            comentarioProfile = result.FirstOrDefault();

            #endregion Option 1: Using Linq.


            if (comentarioProfile == null)
                throw new InstanceNotFoundException(imageId,
                    typeof(Image).FullName);

            return comentarioProfile;
        }

        public void AddCommentDao(long userId, long imageId, String description)
        {
            Comment c = FindComment(userId,imageId);
            if (c != null)
            {
                Comment comment = new Comment();
                comment.userId = userId;
                comment.imageId = imageId;
                comment.comment1 = description;
                comment.date = new DateTime(2008, 5, 1, 8, 30, 52);
                //Comment.Add(comment);
                String query = "INSERT INTO Comment(imageId, userId, comment1, date)" +
                     "VALUES(" + imageId + ", " + userId + ", " + description + ", " + comment.date + ")";
            }
            Update(c);
        }

        public void RemoveCommentDao(long userId, long imageId)
        {
            Comment c = FindComment(userId, imageId);
            if (c != null)
            {
                Comment comment = new Comment();
                comment.userId = userId;
                comment.imageId = imageId;
                //Comment.Remove(comment);
                //Suposicion, non sei como vai
                String query = "REMOVE INTO Comment(imageId, userId, comment1, date)" +
                     "VALUES(" + imageId + ", " + userId + ")";
            }
            Update(c);
        }

        #endregion IImagenDao Members
    }
}