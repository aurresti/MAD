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

        #region ICommentDao Members. Specific Operations

        /// <summary>
        /// Finds a List of Comment by his ImageId
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns>List of Comment and their users</returns>
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
                 orderby u.date descending
                 select u);

            comentarios = result.ToList();

            var result2 =
                (from u in comentarioProfiles
                 from a in userProfiles
                 where u.imageId == imageId && a.userId == u.userId
                 orderby u.date descending
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
        /// Finds a Comment by his ImageId and UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageId"></param>
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
                 orderby u.date descending
                 select u);

            comentarioProfile = result.FirstOrDefault();

            #endregion Option 1: Using Linq.


            if (comentarioProfile == null)
                throw new InstanceNotFoundException(imageId,
                    typeof(Image).FullName);

            return comentarioProfile;
        }

        /// <summary>
        /// Add a Comment 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageId"></param>
        /// <param name="description"></param>
        /// <returns>Added Comment to database</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
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

        /// <summary>
        /// Remove a Comment 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageId"></param>
        /// <returns>Removed Comment to database</returns>
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
        public bool IsCommentsByUser(long commentId, long userId)
        {
            Comment comment;

            UserAccount userAccount;

            DbSet<UserAccount> userProfiles = Context.Set<UserAccount>();

            DbSet<Comment> commentProfiles = Context.Set<Comment>();

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
                comment =
                     (from u in commentProfiles
                      where u.commentId == commentId
                      select u).Single();
            }
            catch (Exception e)
            {
                throw new InstanceNotFoundException(commentId, typeof(Comment).FullName);
            }

            try
            {
                userAccount =
                    (from u in userProfiles
                     from l in u.Comments
                     where u.userId == userId && l.commentId == commentId
                     select u).Single();
            }
            catch (Exception e)
            {
                throw new InstanceNotFoundException(commentId, typeof(Comment).FullName);
            }

            //Console.Write("usuario " + user.loginName + " is been followed by " + follow.loginName + "\n");

            if (userAccount == null)
                throw new InstanceNotFoundException(userId,
                    typeof(UserAccount).FullName);

            return true;

        }
        #endregion ICommentDao Members
    }
}