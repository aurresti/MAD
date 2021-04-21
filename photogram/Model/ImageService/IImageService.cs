using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;

namespace Es.Udc.DotNet.Photogram.Model.ImageService
{
    public interface IImageService
    {

        [Inject]
        IImageDao ImageDao { set; }

        [Transactional]
        Image UploadImage(Image image);

        [Transactional]
        ImageBlock FindImages(string keys, int count, int startIndex, long? categoryId);

        [Transactional]
        void GiveALike(long userId, long imageId);

        [Transactional]
        long CommentImage(long userId, long imageId, Comment comment);



    }
}