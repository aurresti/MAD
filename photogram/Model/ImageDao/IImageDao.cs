using System;
using System.Collections.Generic;
using Castle.Core;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.Photogram.Model.ImageService;

namespace Es.Udc.DotNet.Photogram.Model.ImageDao
{
    public interface IImageDao : IGenericDao<Image, long>
    {
        /// <summary>
        /// Finds a ImagenProfile by Id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>The ImagenProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Image FindById(long id);

        /// <summary>
        /// Finds a ImagenProfile by title
        /// </summary>
        /// <param name="title">title</param>
        /// <returns>The ImagenProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Image FindByTitle(String title);

        /// <summary>
        /// Finds a list of ImagenProfile by title and description
        /// </summary>
        /// <param name="texto">title</param>
        /// <param name="startIndex">index to begin search</param>
        /// <param name="count">limi of search</param>
        /// <returns>List of ImageInfo</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<ImageInfo> FindByText(String texto, int startIndex, int count);

        /// <summary>
        /// Finds a list of ImagenProfile by userId
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>List of ImagenProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Image> FindByUserId(long userId);

        /// <summary>
        /// Finds a ImagenProfile by title,description and category
        /// </summary>
        /// <param name="text">title</param>
        /// <param name="category">category</param>
        /// <param name="startIndex">index to begin search</param>
        /// <param name="count">limi of search</param>
        /// <returns>List of ImageInfo</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<ImageInfo> FindByCategory(string text, string category, int startIndex, int count);

        /// <summary>
        /// Add a Comment to Image
        /// </summary>
        /// <param name="user">User to comment</param>
        /// <param name="imageId">ImageId to comment</param>
        /// <param name="description">contents of comment</param>
        /// <returns>Added Comment to database</returns>
        /// <exception cref="InstanceNotFoundException"/>
        void AddCommentDao(UserAccount user, long imageId, string description);

        /// <summary>
        /// Remove a Comment to Image
        /// </summary>
        /// <param name="user">User to comment</param>
        /// <param name="imageId">ImageId to comment</param>
        /// <returns>Removed Comment to database</returns>
        /// <exception cref="InstanceNotFoundException"/>
        void RemoveCommentDao(UserAccount user, long imageId);

        /// <summary>
        /// Add a Like to Image
        /// </summary>
        /// <param name="userId">UserId to like</param>
        /// <param name="imageId">ImageId to like</param>
        /// <returns>Added Like to database</returns>
        /// <exception cref="InstanceNotFoundException"/>
        void AddLikeDao(long userId, long imageId);

        /// <summary>
        /// Remove a Like to Image
        /// </summary>
        /// <param name="userId">UserId to like</param>
        /// <param name="imageId">ImageId to like</param>
        /// <returns>Removed Like to database</returns>
        /// <exception cref="InstanceNotFoundException"/>
        void RemoveLikeDao(long userId, long imageId);

        /// <summary>
        /// Find a Like
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="imageId">ImageId</param>
        /// <returns>Bool of find Like in database</returns>
        /// <exception cref="InstanceNotFoundException"/>
        bool FindLikeDao(long userId, long imageId);

        /// <summary>
        /// Finds Comments by imageId
        /// </summary>
        /// <param name="imageId">imageId</param>
        /// <returns>List comments of Image</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Comment> FindComments(long imageId);
    }
}
