using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.Photogram.Model.UserDao
{
    public interface IUserDao : IGenericDao<UserAccount, long>
    {
        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        UserAccount FindByLoginName(String loginName);

        /// <summary>
        /// Finds Followers by userId
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>The Followers of user</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<UserAccount> FindFollowersById(long userid);

        /// <summary>
        /// Finds Followeds by userId
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>The Followeds of user</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<UserAccount> FindFollowById(long userId);

        /// <summary>
        /// Follow a user
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="followById">Id of user follower</param>
        /// <returns>The user following the user</returns>
        /// <exception cref="InstanceNotFoundException"/>
        void AddFollow(long userId, long followById);

        /// <summary>
        /// Remove a follow of user
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="unFollowById">Id of user follower</param>
        /// <returns>The user stop following the user</returns>
        /// <exception cref="InstanceNotFoundException"/>
        void RemoveFollow(long userId, long unFollowById);

        /// <summary>
        /// Checks if the specified follow exists.
        /// </summary>
        /// <param name="loginName"> User id. </param>
        /// <param name="loginNameFollow"> User id Follow. </param>
        /// <returns> Boolean to indicate if the follow exists </returns>
        bool FollowExists(long userId, long userIdFollow);
    }
}
