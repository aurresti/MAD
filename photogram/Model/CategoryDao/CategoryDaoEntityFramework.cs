using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.Photogram.Model.CategoryDao
{
    public class CategoryDaoEntityFramework : GenericDaoEntityFramework<Category, long>, ICategoryDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public CategoryDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ICategoryDao Members. Specific Operations

        /// <summary>
        /// Finds a Image by his title
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public Category FindById(long id)
        {
            Category categoryProfile = null;

            #region Option 1: Using Linq.

            DbSet<Category> categoryProfiles = Context.Set<Category>();

            var result =
                (from u in categoryProfiles
                 where u.categoryId == id
                 select u);

            categoryProfile = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            if (categoryProfile == null)
                throw new InstanceNotFoundException(id,
                    typeof(Category).FullName);

            return categoryProfile;
        }

        /// <summary>
        /// Finds a Image by his title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public Category FindByName(String name)
        {
            Category categoryProfile = null;

            #region Option 1: Using Linq.

            DbSet<Category> categoryProfiles = Context.Set<Category>();

            var result =
                (from u in categoryProfiles
                 where u.name == name
                 select u);

            categoryProfile = result.FirstOrDefault();

            #endregion Option 1: Using Linq.


            if (categoryProfile == null)
                throw new InstanceNotFoundException(name,
                    typeof(Category).FullName);

            return categoryProfile;
        }

        /// <summary>
        /// Finds a Image by his title
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public List<Category> FindAll()
        {
            List<Category> categoryProfile = null;

            #region Option 1: Using Linq.

            DbSet<Category> categoryProfiles = Context.Set<Category>();

            categoryProfile =
                (from u in categoryProfiles
                 select u).ToList();

            #endregion Option 1: Using Linq.

            return categoryProfile;
        }
        #endregion ICategoryDao Members
    }
}