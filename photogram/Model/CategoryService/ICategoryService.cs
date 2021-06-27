using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Photogram.Model.CategoryService
{
    public interface ICategoryService
    {
        [Inject]
        ICategoryDao CategoryDao { set; }

        /// <summary>
        /// Checks if the specified Category exists.
        /// </summary>
        /// <param name="id"> Id Category. </param>
        /// <returns> Boolean to indicate if the category exists </returns>
        bool CategoryExists(long id);

        /// <summary>
        /// Create a valid Category.
        /// </summary>
        /// <param name="name"> Category name. </param>
        /// <returns> Id of Category created </returns>
        long CreateCategory(String name);

        /// <summary>
        /// Search a Category for name.
        /// </summary>
        /// <param name="text"> String of search. </param>
        /// <returns> Category</returns>
        Category FindCategory(String text);

        /// <summary>
        /// Search a Category name for id.
        /// </summary>
        /// <param name="categoryId"> Id category. </param>
        /// <returns> Name Category</returns>
        String FindCategoryName(long categoryId);

        /// <summary>
        /// Returns all Categories
        /// </summary>
        /// <returns> List of Category</returns>
        List<Category> FindCategories();
    }
}
