using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.UserService.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Photogram.Model.UserService
{
    public interface IUserService 
    { 
        [Inject]
        IUserDao UserDao { set; }

        /// <summary>
        /// Checks if the specified loginName corresponds to a valid user.
         /// </summary>
        /// <param name="loginName"> User loginName. </param>
        /// <returns> Boolean to indicate if the loginName exists </returns>
        bool UserExists(string loginName);


        /// <summary>
        /// Checks if the specified follow exists.
        /// </summary>
        /// <param name="loginName"> User id. </param>
        /// <param name="loginNameFollow"> User id Follow. </param>
        /// <returns> Boolean to indicate if the follow exists </returns>
        bool UserFollowExists(long userId, long userIdFollow);

        /// <summary>
        /// Register a valid user.
        /// </summary>
        /// <param name="loginName"> User loginName. </param>
        /// <param name="password"> User password. </param>
        /// <param name="userProfileDetails"> User profile. </param>
        /// <returns> Id of new user </returns>
        long CreateUser(string loginName, string password, UserProfile userProfileDetails);

        /// <summary>
        /// Update a valid user.
        /// </summary>
        /// <param name="userProfileId"> User identification. </param>
        /// <param name="userProfileDetails"> User profile. </param>
        /// <returns> The details of profile user change </returns>
        void UpdateProfile(long userProfileId, UserProfile userProfileDetails);

        /// <summary>
        /// Conect a valid user.
        /// </summary>
        /// <param name="loginName"> User loginName. </param>
        /// <param name="password"> User password. </param>
        /// <param name="passwordIsEncrypted"> Password is encrypte or not. </param>
        /// <returns> Result of Login </returns>
        LoginResult ConectUser(string loginName, string password, bool passwordIsEncrypted);

        /// <summary>
        /// Show the followers of the user.
        /// </summary>
        /// <param name="userId"> User Identification. </param>
        /// <returns> List of followers of the user </returns>
        List<UserAccount> SeeFollowers(long userId);

        /// <summary>
        /// Show the follows of the user.
        /// </summary>
        /// <param name="userId"> User Identification. </param>
        /// <returns> List of the follows of the user </returns>
        List<UserAccount> SeeFollow(long userId);

        /// <summary>
        /// Exit a valid user.
        /// </summary>
        /// <param name="loginName"> User loginName. </param>
        /// <returns> Boolean to indicate if the user is exit </returns>
        bool ExitUser(string loginName);

        /// <summary>
        /// Follow a valid user.
        /// </summary>
        /// <param name="userId"> User Identification. </param>
        /// <param name="followedBy"> User Identification to follow. </param>
        /// <returns> Boolean to indicate if the user follow the other user </returns>
        bool FollowUser(long userId, long followedBy);

        /// <summary>
        /// UnFollow a valid user.
        /// </summary>
        /// <param name="userId"> User Identification. </param>
        /// <param name="unFollowedById"> User Identification to follow. </param>
        /// <returns> Boolean to indicate if the user unfollow the other user</returns>
        bool UnFollowUser(long userId, long unFollowedById);

        /// <summary>
        /// Change password of a valid user.
        /// </summary>
        /// <param name="userProfileId"> User id. </param>
        /// <param name="oldClearPassword"> User password old. </param>
        /// <param name="newClearPassword"> User password new. </param>
        /// <returns> The password of user change </returns>
        void ChangePassword(long userProfileId, string oldClearPassword,
string newClearPassword);

        /// <summary>
        /// See profile of a valid user.
        /// </summary>
        /// <param name="userProfileId"> User id. </param>
        /// <returns> The profile of user </returns>
        UserProfile FindUserProfileDetails(long userProfileId);

    }
}
