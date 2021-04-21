using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.Photogram.Model.ImageDao
{
    public class ImagenDaoEntityFramework : GenericDaoEntityFramework<Image, long>, IImageDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public ImagenDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IImagenDao Members. Specific Operations

        /// <summary>
        /// Finds a Image by his title
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        //public Image FindByTitle(string title)
        //{
        //    Image imageProfile = null;

        //    #region Option 1: Using Linq.

        //    DbSet<Image> imageProfiles = Context.Set<Image>();

        //    var result =
        //        (from u in imageProfiles
        //         where u.Titulo == title
        //         select u);

        //    imageProfile = result.FirstOrDefault();

        //    #endregion Option 1: Using Linq.

        //    #region Option 2: Using eSQL over dbSet

        //    //string sqlQuery = "Select * FROM UserProfile where loginName=@loginName";
        //    //DbParameter loginNameParameter =
        //    //    new System.Data.SqlClient.SqlParameter("loginName", loginName);

        //    //userProfile = Context.Database.SqlQuery<UserProfile>(sqlQuery, loginNameParameter).FirstOrDefault<UserProfile>();

        //    #endregion Option 2: Using eSQL over dbSet

        //    #region Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

        //    //String sqlQuery =
        //    //    "SELECT VALUE u FROM MiniPortalEntities.UserProfiles AS u " +
        //    //    "WHERE u.loginName=@loginName";

        //    //ObjectParameter param = new ObjectParameter("loginName", loginName);

        //    //ObjectQuery<UserProfile> query =
        //    //  ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<UserProfile>(sqlQuery, param);

        //    //var result = query.Execute(MergeOption.AppendOnly);

        //    //try
        //    //{
        //    //    userProfile = result.First<UserProfile>();
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    userProfile = null;
        //    //}

        //    #endregion Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

        //    if (imageProfile == null)
        //        throw new InstanceNotFoundException(title,
        //            typeof(Image).FullName);

        //    return imageProfile;
        //}

        /// <summary>
        /// Finds a Image by his title
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        //public Image FindByCategory(string title, string category)
        //{
        //    Image imageProfile = null;

        //    Category categoryId = null;

        //    #region Option 1: Using Linq.

        //    DbSet<Category> categoria= Context.Set<Category>();

        //    var resultc =
        //        (from u in categoria
        //         where u.categoryName == category
        //         select u);

        //    categoryId = resultc.FirstOrDefault();

        //    DbSet<Image> imageProfiles = Context.Set<Image>();

        //    var result =
        //        (from u in imageProfiles
        //         where u.title == title && u.categoryId == categoryId.Id
        //         select u);

        //    imageProfile = result.FirstOrDefault();

        //    #endregion Option 1: Using Linq.

        //    if (imageProfile == null)
        //        throw new InstanceNotFoundException(title,
        //            typeof(Image).FullName);

        //    return imageProfile;
        //}
            //catch (Exception)
            //{
            //    userProfile = null;
            //}

        public List<ImageInfo> FindImages(String[] palabrasClave, long? categoryId, int indiceInicial, int cuenta)
        {
            DbSet<Image> imagenes = Context.Set<Image>();
            List<Image> result;

            if (categoryId == null)
            {
                if (palabrasClave == null)
                {
                    result =
                    (from img in imagenes
                     where img.date > DateTime.Now
                     orderby img.date
                     select img).Skip(indiceInicial).Take(cuenta).ToList();
                }
                else
                {
                    result =
                    (from img in imagenes
                     where palabrasClave.All(s => img.title.Contains(s)) ||
                           palabrasClave.All(s => img.description.Contains(s))
                     orderby img.date
                     select img).Skip(indiceInicial).Take(cuenta).ToList();
                }
            }
            else
            {
                if (palabrasClave == null)
                {
                    result =
                        (from img in imagenes
                         where img.categoryId == categoryId
                         orderby img.date
                         select img).Skip(indiceInicial).Take(cuenta).ToList();
                }
                else
                {
                    result =
                        (from img in imagenes
                         where palabrasClave.All(s => img.title.Contains(s)) && img.categoryId == categoryId
                         orderby img.date
                         select img).Skip(indiceInicial).Take(cuenta).ToList();
                }
            }

            List<ImageInfo> imagenesinfo = new List<ImageInfo>();
            ImageInfo ei;
            foreach (Image img in result)
            {
                ei = new ImageInfo(img.imageId, img.title, img.description, img.date, img.categoryId, img.Category.name, img.Comments.Count);
                imagenesinfo.Add(ei);
            }

            return imagenesinfo;
        }
        #endregion IImagenDao Members
    }

}