using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.Photogram.Model.CategoryDao
{
    public interface ICategoryDao : IGenericDao<Category, long>
    {
        /// <summary>
        /// Finds a Category by Id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>The Category</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Category FindById(long id);

        /// <summary>
        /// Finds a Category by name
        /// </summary>
        /// <param name="name">Category name</param>
        /// <returns>The Category</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Category FindByName(String name);

        /// <summary>
        /// Finds all categories
        /// </summary>
        /// <returns>List of Category</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Category> FindAll();
    }
}
