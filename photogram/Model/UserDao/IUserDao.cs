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
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>The Followers of user</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<UserAccount> FindFollowersById(long userid);

        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>Find who Id is following</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<UserAccount> FindFollowById(long userId);

        /// <summary>
        /// Follow a user
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="followId">Id of user follower</param>
        /// <returns>The user following the user</returns>
        /// <exception cref="InstanceNotFoundException"/>
        void AddFollow(long userId, long followById);

        /// <summary>
        /// Remove a follow of user
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="followId">Id of user follower</param>
        /// <returns>The user stop following the user</returns>
        /// <exception cref="InstanceNotFoundException"/>
        void RemoveFollow(long userId, long unFollowById);

        /// <summary>
        /// Checks if the specified follow exists.
        /// </summary>
        /// <param name="loginName"> User loginName. </param>
        /// <param name="loginNameFollow"> User loginName Follow. </param>
        /// <returns> Boolean to indicate if the follow exists </returns>
        bool FollowExists(string loginName, string loginNameFollow);
    }
}
