using System;
using System.Collections.Generic;
using Castle.Core;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

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
        /// Finds a ImagenProfile by title and description
        /// </summary>
        /// <param name="title">title</param>
        /// <returns>The ImagenProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Pair<Image, Category>> FindByText(String texto);

        /// <summary>
        /// Finds a ImagenProfile by title,description and category
        /// </summary>
        /// <param name="title">title</param>
        /// <param name="category">category</param>
        /// <returns>The ImagenProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Pair<Image, Category>> FindByCategory(string title, string category);

        void AddCommentDao(UserAccount user, long imageId, string description);

        void RemoveCommentDao(UserAccount user, long imageId);

        void AddLikeDao(UserAccount user, long imageId);

        /// <summary>
        /// Finds a ImagenProfile by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>The ImagenProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Comment> FindComments(long id);
    }
}
