using Es.Udc.DotNet.Photogram.Model.ImageDao;
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

        public ImageBlock FindImages(string keys, int count, int startIndex, long? categoryId = null )
        {
            String[] keywords = null;
            if (keys != null && keys != "")
            {
                keywords = keys.Split(' ');
            }

            List<ImageInfo> images = ImageDao.FindImages(keywords, categoryId, startIndex, count + 1);

            bool existMoreEvents = (images.Count == count + 1);

            if (existMoreEvents)
                images.RemoveAt(count);

            return new ImageBlock(images, existMoreEvents);
        }

        public long CommentImage(long userId, long imageId, Comment comment)
        {
            throw new NotImplementedException();
        }

        public void GiveALike(long userId, long imageId)
        {
            throw new NotImplementedException();
        }

        public Image UploadImage(Image image)
        {
            if (image.userId != null)
            {
                //comprobar que el user existe 
            }
            ImageDao.Create(image);
            return image;

        }
    }
}
