using System;

namespace Es.Udc.DotNet.Photogram.Model.ImageService
{
    public class ImageInfo
    {
        public ImageInfo(long imageId, string title, string description, DateTime date,string exifInfo, long categoryId, string categoryName, int numLikes, long userId, string name, string url)
        {
            this.imageId = imageId;
            this.title = title;
            this.description = description;
            this.date = date;
            this.exifInfo = exifInfo;
            this.categoryId = categoryId;
            this.categoryName = categoryName;
            this.numLikes = numLikes;
            this.userId = userId;
            this.name = name;
            this.url = url;
        }

        public long imageId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public System.DateTime date { get; set; }
        public string exifInfo { get; set; }
        public long categoryId { get; set; }
        public string categoryName { get; set; }
        public int numLikes { get; set; }
        public long userId { get; private set; }
        public string name { get; private set; }
        public string url { get; private set; }

        public override String ToString()
        {
            String strImagenProfileDetails;

            strImagenProfileDetails =
                "[ imageId" + imageId + " | " +
                "title = " + title + " | " +
                "description = " + description + " | " +
                "date = " + date + " | " +
                "exif = " + exifInfo + " | " +
                "categoryId = " + categoryId + " | " +
                "category = " + categoryName + " | " +
                "numLikes = " + numLikes + " | " +
                "userId = " + userId + " | " +
                "name = " + name + " | " +
                "url = " + url + " ]";


            return strImagenProfileDetails;
        }

        public override bool Equals(object obj)
        {

            ImageInfo target = (ImageInfo)obj;

            return (this.imageId == target.imageId)
                  && (this.title == target.title)
                  && (this.description == target.description)
                  && (this.date == target.date)
                  && (this.exifInfo == target.exifInfo)
                  && (this.categoryId == target.categoryId)
                  && (this.categoryName == target.categoryName)
                  && (this.numLikes == target.numLikes)
                  && (this.userId == target.userId)
                  && (this.name == target.name);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the FirstName does not change.        
        public override int GetHashCode()
        {
            return this.title.GetHashCode();
        }
    }

}
