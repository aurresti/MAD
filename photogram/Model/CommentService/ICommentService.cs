using Es.Udc.DotNet.Photogram.Model.CommentDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using Ninject;
using System.Collections.Generic;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Castle.Core;

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

        /// <summary>
        /// Add a Comment to Image
        /// </summary>
        /// <param name="userId">UserId to comment</param>
        /// <param name="imageId">ImageId to comment</param>
        /// <param name="description">contents of comment</param>
        /// <returns>Added Comment to database</returns>
        long AddComment(long userId, long imageId, string description);

        /// <summary>
        /// Remove a Comment to Image
        /// </summary>
        /// <param name="userId">User to comment</param>
        /// <param name="imageId">ImageId to comment</param>
        /// <param name="commentId">CommentId to comment</param>
        /// <returns>Removed Comment to database</returns>
        /// <exception cref="InstanceNotFoundException"/>
        bool RemoveComment(long userId, long imageId, long commentId);

        /// <summary>
        /// Update a Comment to Image
        /// </summary>
        /// <param name="userId">User to comment</param>
        /// <param name="imageId">ImageId to comment</param>
        /// <param name="comment">CommentId to comment</param>
        /// <param name="description">contents of comment</param>
        /// <returns>Updated Comment to database</returns>
        void UpdateComment(long userId, long imageId, long comment, string description);

        /// <summary>
        /// Finds Comments and their Users by imageId
        /// </summary>
        /// <param name="imageId">imageId</param>
        /// <returns>List comments of Image</returns>
        List<Pair<Comment, UserAccount>> GetComments(long imageId);

        /// <summary>
        /// Finds if comment is made by user
        /// </summary>
        /// <param name="commentId">commentId</param>
        /// <param name="userId">userId</param>
        /// <returns>boolean</returns>
        bool IsCommentsByUser(long commentId, long userId);

        /// <summary>
        /// Finds comment
        /// </summary>
        /// <param name="commentId">commentId</param>
        /// <returns>Comment</returns>
        Comment FindComment(long commentId);

    }
}