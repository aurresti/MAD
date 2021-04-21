using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.Photogram.Model.UserDao
{
    public class UserDaoEntityFramework : GenericDaoEntityFramework<UserAccount, long>, IUserDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public UserDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IUsuarioDao Members. Specific Operations

        /// <summary>
        /// Finds a UserProfile by his loginName
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public UserAccount FindByLoginName(string loginName)
        {
            UserAccount userProfile = null;

            #region Option 1: Using Linq.

            DbSet<UserAccount> userProfiles = Context.Set<UserAccount>();

            var result =
                (from u in userProfiles
                 where u.loginName == loginName
                 select u);

            userProfile = result.FirstOrDefault();

            #endregion Option 1: Using Linq.


            if (userProfile == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(UserAccount).FullName);

            return userProfile;
        }

        public List<UserAccount> FindFollowersById(long id)
        {
            List<UserAccount> followers = null;

            DbSet<UserAccount> userProfiles = Context.Set<UserAccount>();


            followers =
                (from u in userProfiles
                 from g in u.UserAccounts
                 where u.userId == id
                 orderby g.loginName
                 select g).ToList();

            return followers;
        }

        public List<UserAccount> FindFollowById(long id)
        {
            List<UserAccount> follow = null;

            DbSet<UserAccount> userProfiles = Context.Set<UserAccount>();

            follow =
                (from u in userProfiles
                 from g in u.UserAccount1
                 where u.userId == id
                 orderby g.loginName
                 select g).ToList();

            return follow;
        }

        public void AddFollow(long userId, long followId)
        {
            UserAccount user;
            UserAccount follow;

            DbSet<UserAccount> userProfiles = Context.Set<UserAccount>();

            try
            {
                user =
                    (from u in userProfiles
                     where u.userId == userId
                     select u).Single();
            }
            catch (Exception e)
            {
                throw new InstanceNotFoundException(userId, typeof(UserAccount).FullName);
            }
            try
            {
                follow =
                     (from u in userProfiles
                      where u.userId == followId
                      select u).Single();
            }
            catch (Exception e)
            {
                throw new InstanceNotFoundException(followId, typeof(UserAccount).FullName);
            }

            //Console.Write("usuario " + user.loginName + " is been followed by " + follow.loginName + "\n");

            user.UserAccounts.Add(follow);

            Context.SaveChanges();
        }

        public void RemoveFollow(long userId, long unFollowById)
        {
            UserAccount user;
            UserAccount unFollow;

            DbSet<UserAccount> userProfiles = Context.Set<UserAccount>();

            try
            {
                user =
                    (from u in userProfiles
                     where u.userId == userId
                     select u).Single();
            }
            catch (Exception e)
            {
                throw new InstanceNotFoundException(userId, typeof(UserAccount).FullName);
            }
            try
            {
                unFollow =
                     (from u in userProfiles
                      where u.userId == unFollowById
                      select u).Single();
            }
            catch (Exception e)
            {
                throw new InstanceNotFoundException(unFollowById, typeof(UserAccount).FullName);
            }

            //Console.Write("usuario " + user.loginName + ", is followed by" + unFollow.loginName + "\n");

            user.UserAccounts.Remove(unFollow);

            Context.SaveChanges();
        }

        #endregion IUsuarioDao Members. Specific Operations
    }
}
