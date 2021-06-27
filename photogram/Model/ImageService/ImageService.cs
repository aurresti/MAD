using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using System.Collections.Generic;
using Castle.Core;

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
                var a = ImageDao.FindByTitle(title);
                Console.WriteLine(a.title);
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
                imagenProfile.imageView = imageProfileDetails.File;

                ImageDao.Create(imagenProfile);

                return imagenProfile.imageId;


            }
        }

        public List<Image> FindImageByUserId(long userId)
        {

            try
            {
                List<Image> imageProfile = ImageDao.FindByUserId(userId);
                return imageProfile;
            }
            catch (InstanceNotFoundException e)
            {
                return null;
            }

        }

        public Image UploadImage(Image image) ///MAL
        {
            ImageDao.Create(image);

            return image;
        }

        public ImageBlock FindImages(String texto, String category, Boolean categoryB, int startIndex, int count)
        {
            try
            {

                List<ImageInfo> list = null;
                if (categoryB)
                {
                    list = ImageDao.FindByCategory(texto, category, startIndex, count+1);
                }
                else
                {
                    list = ImageDao.FindByText(texto, startIndex, count+1);
                }
                ///check if there are more obj to show
                bool existMoreAccounts = (list.Count == count + 1);

                if (existMoreAccounts)
                    list.RemoveAt(count);

                return new ImageBlock(list, existMoreAccounts);
            }
            catch (InstanceNotFoundException e)
            {
                return null;
            }

        }

        public bool AddLike(long userId, long imageId)
        {
            try
            {
                ImageDao.AddLikeDao(userId, imageId);

                return true;
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }
        }

        public bool RemoveLike(long userId, long imageId)
        {
            try
            {
                ImageDao.RemoveLikeDao(userId, imageId);

                return true;
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }
        }

        public bool FindLike(long userId, long imageId)
        {
            try
            {
                return ImageDao.FindLikeDao(userId, imageId);
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }
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
                    imageProfile.exifInfo, imageProfile.categoryId, imageProfile.userId, imageProfile.UserAccounts.Count, imageProfile.imageView);

            return imageProfileDetails;
        }
    }
}