using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.Photogram.Model.UserDao
{
    public interface IUserDao : IGenericDao<User, long>
    {
        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        User FindByLoginName(String loginName);

        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>The Followers of user</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Follow> FindFollowersById(long id);

        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>The user who follow the user</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Follow> FindFollowersByUserById(long id);

        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <param name="idf">Id of user follower</param>
        /// <returns>The user who follow the user</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Follow FindFollowers(long id, long idf);

        /// <summary>
        /// Follow a user
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="followId">Id of user follower</param>
        /// <returns>The user following the user follow</returns>
        /// <exception cref="InstanceNotFoundException"/>
        void AddFollow(long userId, Follow userFollow);

        /// <summary>
        /// Remove a follow of user
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="followId">Id of user follower</param>
        /// <returns>The user stop following the user follow</returns>
        /// <exception cref="InstanceNotFoundException"/>
        void RemoveFollow(long userId, Follow userFollow);
    }
}
