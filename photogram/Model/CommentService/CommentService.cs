using Es.Udc.DotNet.Photogram.Model.CommentDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using System.Collections.Generic;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.UserDao;

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

        public Comment AddComment(long userId, long imageId, string description)
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
                return comment;
            
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

        public void UpdateComment(long userId, long imageId, Comment comment)
        {
            UserAccount user = userDao.Find(userId);
            Comment comment1 = CommentDao.Find(comment.commentId);
            Image image = ImageDao.Find(imageId);

            if (CommentDao.Exists(comment1.commentId) &&
                user.userId == comment.userId &&
                image.imageId == comment.imageId)
            {
                CommentDao.Update(comment);
            }

        }

        #endregion ICommentService Members
    }
}