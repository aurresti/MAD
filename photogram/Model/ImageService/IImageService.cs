using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using Ninject;
using System.Collections.Generic;
using Es.Udc.DotNet.Photogram.Model.UserDao;

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
        /// Upload a valid Imagen.
        /// </summary>
        /// <param name="loginName"> User loginName. </param>
        /// <param name="userProfileDetails"> Imagen profile. </param>
        /// <returns> Boolean to indicate if the imagen is upload </returns>
        long CreateImage(string loginName, ImageProfile imageProfileDetails);

        Image UploadImage(Image image);

        /// Search a Imagen for word key.
        /// </summary>
        /// <param name="title"> String of search. </param>
        /// <param name="category"> Search for categoty or not. </param>
        /// <returns> List of Image</returns>
        List<Image> FindImages(String texto, String category, Boolean categoryB);

        /// <summary>
        /// See profile of a valid user.
        /// </summary>
        /// <param name="userProfileId"> User id. </param>
        /// <returns> The profile of user </returns>
        ImageProfile FindImageProfileDetails(long imageProfileId);

        /*bool AddComment(long userId, string title, string description);

        bool RemoveComment(long userId, string title);*/

        bool AddLike(long userId, long imageId);

        /// See comments of an Image.
        /// </summary>
        /// <param name="title"> String of search Image. </param>
        /// <returns> List of comments</returns>
        List<Comment> SeeComments(String title);
    }
}
