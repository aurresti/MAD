using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Es.Udc.DotNet.Photogram.Model.CategoryService
{
    public class CategoryService : ICategoryService
    {
        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        #region ICategoryService Members

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]

        public bool CategoryExists(long id)
        {

            try
            {
                Category categoryProfile = CategoryDao.FindById(id);
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }

            return true;
        }

        public long CreateCategory(String name)
        {

            try
            {
                CategoryDao.FindByName(name);

                throw new DuplicateInstanceException(name,
                    typeof(Category).FullName);
            }
            catch (InstanceNotFoundException)
            {

                Category categoryProfile = new Category();

                categoryProfile.name = name;
                //categoryProfile.categoryId = 1;

                CategoryDao.Create(categoryProfile);

                return categoryProfile.categoryId;
            }
        }

        public Category FindCategory(String texto)
        {
              
            Category categoryProfile = CategoryDao.FindByName(texto);
            return categoryProfile;

        }
        #endregion ICategoryService Members

        public String FindCategoryName(long categoryId)
        {
            try
            {
                Category categoryProfile = CategoryDao.FindById(categoryId);
                return categoryProfile.name;
            }
            catch (Exception e){
                return "";
            }

            
        }
    }
}