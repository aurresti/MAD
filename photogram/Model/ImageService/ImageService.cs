using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Photogram.Model.ImageService
{
    public class ImageService : IImageService
    {
        [Inject]
        public IImageDao ImageDao { private get; set; }
        [Inject]
        public IUserDao UserDao { private get; set; }

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]

        public bool ImageExists(long id)
        {

            try
            {
                Image imageProfile = ImageDao.FindById(id);
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }

            return true;
        }

        public long CreateImage(String title, ImageProfile imageProfileDetails)
        {

            try
            {
                ImageDao.FindByTitle(title);

                throw new DuplicateInstanceException(title,
                    typeof(Image).FullName);
            }
            catch (InstanceNotFoundException)
            {

                Image imagenProfile = new Image();

                imagenProfile.title = title;
                imagenProfile.description = imageProfileDetails.Descripction;
                imagenProfile.date = imageProfileDetails.Date;
                imagenProfile.exifInfo = imageProfileDetails.Exif;
                imagenProfile.categoryId = imageProfileDetails.Category;
                imagenProfile.userId = imageProfileDetails.User;

                ImageDao.Create(imagenProfile);

                return imagenProfile.imageId;
            }
        }


        public Image UploadImage(Image image)
        {
            ImageDao.Create(image);

            return image;
        }

        public List<Image> FindImages(String texto, String category, Boolean categoryB)
        {

            try
            {
                List<Image> imageProfile = null;
                if (categoryB)
                {
                    imageProfile = ImageDao.FindByCategory(texto, category);
                }
                else
                {
                    imageProfile = ImageDao.FindByText(texto);
                }
                return imageProfile;
            }
            catch (InstanceNotFoundException e)
            {
                return null;
            }

        }

        /*public bool AddComment(long userId, string title, string description)
           {
                try
                {
                    Image imageProfile = ImageDao.FindByTitle(title);
                    UserAccount user = UserDao.Find(userId);
                    ImageDao.AddCommentDao(user,imageProfile.imageId,description);
                    return true;
                }
                catch (InstanceNotFoundException e)
                 {
                    return false;
                 }
        }

        public bool RemoveComment(long userId, string title)
        {
            try
            {
                Image imageProfile = ImageDao.FindByTitle(title);
                UserAccount user = UserDao.Find(userId);
                ImageDao.RemoveCommentDao(user, imageProfile.imageId);
                return true;
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }
        }

        public void UpdateProfile(long userId, string title, String description, DateTime date)
        {
            Comment commentProfile =
                UserDao.Find(userProfileId);

            commentProfile.comment1 = description;
            commentProfile.date = date;
            UserDao.Update(commentProfile);
        }*/

        public bool AddLike(long userId, long imageId)
        {
            UserAccount userProfile = UserDao.Find(userId);
            Image img = ImageDao.Find(imageId);
            if (userProfile != null && img != null)
            {
                if (!userProfile.Images1.Contains(img) && !img.UserAccounts.Contains(userProfile) )
                {
                    //añade la imagen a la coleccion de imagenes que le dio like 
                    userProfile.Images1.Add(img);
                    //añade el usuario a la coleccion de usuarios que le dieron like
                    img.UserAccounts.Add(userProfile);
                    return true;
                }
            }
            return false;
        }

        public List<Comment> SeeComments(String title)
        {

            try
            {
                Image imagenProfile = ImageDao.FindByTitle(title);
                List<Comment> comentarios = ImageDao.FindComments(imagenProfile.imageId);
                return comentarios;
            }
            catch (InstanceNotFoundException)
            {
                throw new InstanceNotFoundException(title,
                     typeof(Image).FullName);
            }
        }

        public ImageProfile FindImageProfileDetails(long imageId)
        {
            Image imageProfile = ImageDao.Find(imageId);

            ImageProfile imageProfileDetails =
                new ImageProfile(imageProfile.title,
                    imageProfile.description, imageProfile.date,
                    imageProfile.exifInfo, imageProfile.categoryId, imageProfile.userId, imageProfile.UserAccounts.Count);

            return imageProfileDetails;
        }
    }
}