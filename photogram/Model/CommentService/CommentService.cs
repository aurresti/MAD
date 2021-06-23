using Es.Udc.DotNet.Photogram.Model.CommentDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using System.Collections.Generic;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Castle.Core;

namespace Es.Udc.DotNet.Photogram.Model.CommentService
{
    public class CommentService : ICommentService
    {
        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public IImageDao ImageDao { private get; set; }

        [Inject]
        public IUserDao userDao { private get; set; }

        #region ICommentService members 

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]

        public long AddComment(long userId, long imageId, string description)
        {
            Comment comment = null;
            if (userDao.Exists(userId) && ImageDao.Exists(imageId))
            {
                comment = new Comment();
                comment.userId = userId;
                comment.imageId = imageId;
                comment.comment1 = description;
                comment.date = DateTime.Now;
                CommentDao.Create(comment);
            }
            else
            {
                throw new InstanceNotFoundException("User",
                        typeof(UserAccount).FullName);
            }
            /*else if (!userDao.Exists(userId))
            {
                throw new InstanceNotFoundException("User",
                        typeof(UserAccount).FullName);
            }
            else if (!ImageDao.Exists(imageId))
            {
                throw new InstanceNotFoundException("Image",
                        typeof(Image).FullName);
            }*/
            return comment.commentId;

        }

        public bool RemoveComment(long userId, long imageId, long commentId)
        {
            UserAccount user = userDao.Find(userId);
            Comment comment = CommentDao.Find(commentId);
            Image image = ImageDao.Find(imageId);

            if (CommentDao.Exists(commentId) &&
                user.userId == comment.userId &&
                image.imageId == comment.imageId)
            {
                CommentDao.Remove(comment.commentId);
                return true;
            }

            return false;

        }

        public void UpdateComment(long userId, long imageId, long comment, string description)
        {
            UserAccount user = userDao.Find(userId);
            Comment comment1 = CommentDao.Find(comment);
            Image image = ImageDao.Find(imageId);

            if (CommentDao.Exists(comment1.commentId) &&
                user.userId == comment1.userId &&
                image.imageId == comment1.imageId)
            {
                comment1.comment1 = description;
                CommentDao.Update(comment1);
            }

        }

        public List<Pair<Comment, UserAccount>> GetComments(long imageId) 
        {
            List<Pair<Comment, UserAccount>> list = new List<Pair<Comment, UserAccount>>();
            try
            {
                list = CommentDao.FindComments(imageId);
            }
            catch (Exception e) /// sin commentarios
            {
                return list;
            }

            return list;
        }

        #endregion ICommentService Members
    }
}