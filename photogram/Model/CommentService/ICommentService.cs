using Es.Udc.DotNet.Photogram.Model.CommentDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using Ninject;
using System.Collections.Generic;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.UserDao;

namespace Es.Udc.DotNet.Photogram.Model.CommentService
{
    public interface ICommentService
    {
        [Inject]
        ICommentDao CommentDao { set; }
        [Inject]
        IImageDao ImageDao { set; }

        [Inject]
        IUserDao userDao { set; }

        long AddComment(long userId, long imageId, string description);

        bool RemoveComment(long userId, long imageId, long commentId);

        void UpdateComment(long userId, long imageId, long comment, string description);

    }
}