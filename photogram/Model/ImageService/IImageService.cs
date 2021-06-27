using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using Ninject;
using System.Collections.Generic;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Castle.Core;

namespace Es.Udc.DotNet.Photogram.Model.ImageService
{
    public interface IImageService
    {
        [Inject]
        IImageDao ImageDao { set; }
        [Inject]
        IUserDao UserDao { set; }


        /// <summary>
        /// Checks if the specified Imagen exists.
        /// </summary>
        /// <param name="id"> Id Imagen. </param>
        /// <returns> Boolean to indicate if the imagen exists </returns>
        bool ImageExists(long id);

        /// <summary>
        /// Create a valid Imagen.
        /// </summary>
        /// <param name="loginName"> User loginName. </param>
        /// <param name="imageProfileDetails"> Imagen profile. </param>
        /// <returns> Id of Image create </returns>
        long CreateImage(string loginName, ImageProfile imageProfileDetails);

        List<Image> FindImageByUserId(long userId);

        /// <summary>
        /// Upload a valid Imagen.
        /// </summary>
        /// <param name="image"> Image to Update. </param>
        /// <returns> Image upload </returns>
        Image UploadImage(Image image);

        /// <summary>
        /// Search a Imagen for word key.
        /// </summary>
        /// <param name="text"> String of search. </param>
        /// <param name="category"> Category Name. </param>
        /// <param name="categoryB"> Search for categoty or not. </param>
        /// <param name="startIndex"> Index to begin serach. </param>
        /// <param name="count"> Num max of index to search. </param>
        /// <returns> List of Image</returns>
        ImageBlock FindImages(String text, String category, Boolean categoryB, int startIndex, int count);

        /// <summary>
        /// Find profile of a valid image.
        /// </summary>
        /// <param name="imageProfileId"> Image id. </param>
        /// <returns> The profile of image </returns>
        ImageProfile FindImageProfileDetails(long imageProfileId);

        /// <summary>
        /// Add a Like to Image
        /// </summary>
        /// <param name="userId">UserId to like</param>
        /// <param name="imageId">ImageId to like</param>
        /// <returns>Added Like to database</returns>
        bool AddLike(long userId, long imageId);

        /// <summary>
        /// Remove a Like to Image
        /// </summary>
        /// <param name="userId">UserId to like</param>
        /// <param name="imageId">ImageId to like</param>
        /// <returns>Removed Like to database</returns>
        /// <exception cref="InstanceNotFoundException"/>
        bool RemoveLike(long userId, long imageId);

        /// <summary>
        /// Find a Like
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="imageId">ImageId</param>
        /// <returns>Bool of find Like in database</returns>
        bool FindLike(long userId, long imageId);

        ///<summary>
        /// See comments of an Image.
        /// </summary>
        /// <param name="title"> Title of Image. </param>
        /// <returns> List of comments</returns>
        List<Comment> SeeComments(String title);
    }
}
