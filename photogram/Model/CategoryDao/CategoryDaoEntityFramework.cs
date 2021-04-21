using Es.Udc.DotNet.ModelUtil.Dao;
using System;

namespace Es.Udc.DotNet.Photogram.Model.CategoryDao
{
    public class CategoryDaoEntityFramework :
            GenericDaoEntityFramework<Category, long>, ICategoryDao
    {

    }
}
