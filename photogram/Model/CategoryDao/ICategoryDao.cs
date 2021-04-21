using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.Photogram.Model.CategoryDao
{
    public interface ICategoryDao : IGenericDao<Category, long>
    {
        /// <summary>
        /// Finds a ImagenProfile by Id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>The ImagenProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Category FindById(long id);

        /// <summary>
        /// Finds a ImagenProfile by title
        /// </summary>
        /// <param name="title">title</param>
        /// <returns>The ImagenProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Category FindByName(String name);
    }
}
