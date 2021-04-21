using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using System;
using System.Collections.Generic;
//using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.Photogram.Model.ImageDao
{
    public interface IImageDao : IGenericDao<Image, long>
    {
        ///// <summary>
        ///// Finds a ImagenProfile by title and description
        ///// </summary>
        ///// <param name="title">title</param>
        ///// <returns>The ImagenProfile</returns>
        ///// <exception cref="InstanceNotFoundException"/>
        //List<Image> FindByTitle(String title);

        ///// <summary>
        ///// Finds a ImagenProfile by title,description and category
        ///// </summary>
        ///// <param name="title">title</param>
        ///// <param name="category">category</param>
        ///// <returns>The ImagenProfile</returns>
        ///// <exception cref="InstanceNotFoundException"/>
        //Image FindByCategory(string title, string category);

        List<ImageInfo> FindImages(String[] palabrasClave, long? categoriaId, int inidiceInicial, int cuenta);
    }

}
