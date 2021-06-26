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
        /// Checks if the specified Imagen exists.
        /// </summary>
        /// <param name="id"> Id Imagen. </param>
        /// <returns> Boolean to indicate if the imagen exists </returns>
        bool CategoryExists(long id);

        /// <summary>
        /// Upload a valid Imagen.
        /// </summary>
        /// <param name="loginName"> User loginName. </param>
        /// <param name="userProfileDetails"> Imagen profile. </param>
        /// <returns> Boolean to indicate if the imagen is upload </returns>
        long CreateCategory(String name);

        /// Search a Imagen for word key.
        /// </summary>
        /// <param name="title"> String of search. </param>
        /// <param name="category"> Search for categoty or not. </param>
        /// <returns> List of Image</returns>
        Category FindCategory(String texto);

        /// Search a Category for id.
        /// </summary>
        /// <param name="category"> Id category. </param>
        /// <returns> Name Category</returns>
        String FindCategoryName(long categoryId);

        /// Returns all Categories
        /// </summary>
        /// <returns> Name Category</returns>
        List<Category> FindCategories();
    }
}
