using Castle.Core;
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
        public List<Pair<Comment, UserAccount>> FindComments(long imageId)
        {
            List<Pair<Comment, UserAccount>> commentList = new List<Pair<Comment, UserAccount>>();

            List<Comment> comentarios = null;

            List<UserAccount> usuarios = null;

            #region Option 1: Using Linq.

            DbSet<Comment> comentarioProfiles = Context.Set<Comment>();

            DbSet<UserAccount> userProfiles = Context.Set<UserAccount>();

            var result =
                (from u in comentarioProfiles
                 where u.imageId == imageId
                 orderby u.date
                 select u);

            comentarios = result.ToList();

            var result2 =
                (from u in comentarioProfiles
                 from a in userProfiles
                 where u.imageId == imageId && a.userId == u.userId
                 orderby u.date
                 select a);

            usuarios = result2.ToList();

            #endregion Option 1: Using Linq.

            if (comentarios == null)
                throw new InstanceNotFoundException(imageId,
                    typeof(Image).FullName);

            for (int i = 0; i < comentarios.Count; i++)
                commentList.Add(new Pair<Comment, UserAccount>(comentarios[i], usuarios[i]));

            return commentList;
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