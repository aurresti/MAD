using System;

namespace Es.Udc.DotNet.Photogram.Model.ImageService
{
    public class ImageInfo
    {
        public ImageInfo(long imageId, string title, string description, DateTime date, long categoryId, string categoryName, long numLikes)
        {
            ImageId = imageId;
            Title = title;
            Description = description;
            Date = date;
            CategoryId = categoryId;
            CategoryName = categoryName;
            NumLikes = numLikes;
        }

        public long ImageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public System.DateTime Date { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long NumLikes { get; set; }
    }

}
