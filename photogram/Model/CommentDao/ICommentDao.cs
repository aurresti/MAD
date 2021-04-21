using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.Photogram.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, long>
    {
        void AddCommentDao(long userId, long imageId, string description);

        void RemoveCommentDao(long userId, long imageId);

        /// <summary>
        /// Finds a ImagenProfile by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>The ImagenProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Comment> FindComments(int id);

        /// <summary>
        /// Finds a ImagenProfile by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>The ImagenProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Comment FindComment(long userId, long imageId);
    }
}
