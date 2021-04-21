using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Photogram.Model.UserService
{
    public class UsuarioSeguir
    {
        #region Properties Region

        public int UserId { get; private set; }

        public int FollowId { get; private set; }


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileDetails"/>
        /// class.
        /// </summary>
        /// <param name="userId">The user's id.</param>
        /// <param name="followId">The follow's id.</param>
        public UsuarioSeguir(int userId, int followId)
        {
            this.UserId = userId;
            this.FollowId = followId;
        }

        public override bool Equals(object obj)
        {

            UsuarioSeguir target = (UsuarioSeguir)obj;

            return (this.UserId == target.UserId)
                  && (this.FollowId == target.FollowId);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the FirstName does not change.        
        public override int GetHashCode()
        {
            return this.UserId.GetHashCode();
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
            String strUserProfileDetails;

            strUserProfileDetails =
                "[ firstName = " + UserId + " | " +
                "lastName = " + FollowId + " | " ;


            return strUserProfileDetails;
        }
    }
}
