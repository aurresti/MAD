using System;
using System.Collections.Generic;
using Castle.Core;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.Photogram.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, long>
    {
        /// <summary>
        /// Add a Comment 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageId"></param>
        /// <param name="description"></param>
        /// <returns>Added Comment to database</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        void AddCommentDao(long userId, long imageId, string description);

        /// <summary>
        /// Remove a Comment 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageId"></param>
        /// <returns>Removed Comment to database</returns>
        void RemoveCommentDao(long userId, long imageId);

        /// <summary>
        /// Finds a List of Comment by his ImageId
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns>List of Comment and their users</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        List<Pair<Comment, UserAccount>> FindComments(long id);

        /// <summary>
        /// Finds a Comment by userId an imageId
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="ImageId">ImageId</param>
        /// <returns>The Comment</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Comment FindComment(long userId, long imageId);
    }
}
