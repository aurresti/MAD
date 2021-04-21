using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Photogram.Model.ImageService
{
    public class ImageProfile
    {
        #region Properties Region

        public String Title { get; private set; }

        public String Descripction { get; private set; }

        public DateTime Date { get; private set; }

        public string Exif { get; private set; }

        public long Category { get; private set; }

        public long? User { get; private set; }
        public long Likes { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileDetails"/>
        /// class.
        /// </summary>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="email">The user's email.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        /// <param name="conect">The conection.</param>
        /// <param name="mantener">The save.</param>
        public ImageProfile(String title, String description,
            DateTime date, String exif, long category, long? user, long likes)
        {
            this.Title = title;
            this.Descripction = description;
            this.Date = date;
            this.Exif = exif;
            this.Category = category;
            this.User = user;
            this.Likes = likes;
        }

        public override bool Equals(object obj)
        {

            ImageProfile target = (ImageProfile)obj;

            return (this.Title == target.Title)
                  && (this.Descripction == target.Descripction)
                  && (this.Date == target.Date)
                  && (this.Exif == target.Exif)
                  && (this.Category == target.Category)
                  && (this.User == target.User);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the FirstName does not change.        
        public override int GetHashCode()
        {
            return this.Title.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override String ToString()
        {
            String strImagenProfileDetails;

            strImagenProfileDetails =
                "[ title = " + Title + " | " +
                "description = " + Descripction + " | " +
                "date = " + Date + " | " +
                "exif = " + Exif + " | " +
                "category = " + Category + " ]" +
                "user = " + User + " | ";


            return strImagenProfileDetails;
        }
    }
}
