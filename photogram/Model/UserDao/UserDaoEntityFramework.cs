using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.Photogram.Model.UserDao
{
    public class UserDaoEntityFramework : GenericDaoEntityFramework<User, long>, IUserDao
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
        public User FindByLoginName(string loginName)
        {
            User userProfile = null;

            #region Option 1: Using Linq.

            DbSet<User> userProfiles = Context.Set<User>();

            var result =
                (from u in userProfiles
                 where u.Login == loginName
                 select u);

            userProfile = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            #region Option 2: Using eSQL over dbSet

            //string sqlQuery = "Select * FROM UserProfile where loginName=@loginName";
            //DbParameter loginNameParameter =
            //    new System.Data.SqlClient.SqlParameter("loginName", loginName);

            //userProfile = Context.Database.SqlQuery<UserProfile>(sqlQuery, loginNameParameter).FirstOrDefault<UserProfile>();

            #endregion Option 2: Using eSQL over dbSet

            #region Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            //String sqlQuery =
            //    "SELECT VALUE u FROM MiniPortalEntities.UserProfiles AS u " +
            //    "WHERE u.loginName=@loginName";

            //ObjectParameter param = new ObjectParameter("loginName", loginName);

            //ObjectQuery<UserProfile> query =
            //  ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<UserProfile>(sqlQuery, param);

            //var result = query.Execute(MergeOption.AppendOnly);

            //try
            //{
            //    userProfile = result.First<UserProfile>();
            //}
            //catch (Exception)
            //{
            //    userProfile = null;
            //}

            #endregion Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            if (userProfile == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(User).FullName);

            return userProfile;
        }

        public List<Follow> FindFollowersById(long id)
        {
            List<Follow> userProfile = null;

            #region Option 1: Using Linq.

            DbSet<Follow> followers = Context.Set<Follow>();

            var result =
                (from u in followers
                 where u.UserId == id
                 select u);

            userProfile = (List<Follow>)result;

            #endregion Option 1: Using Linq.

            #region Option 2: Using eSQL over dbSet

            //string sqlQuery = "Select * FROM UserProfile where loginName=@loginName";
            //DbParameter loginNameParameter =
            //    new System.Data.SqlClient.SqlParameter("loginName", loginName);

            //userProfile = Context.Database.SqlQuery<UserProfile>(sqlQuery, loginNameParameter).FirstOrDefault<UserProfile>();

            #endregion Option 2: Using eSQL over dbSet

            #region Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            //String sqlQuery =
            //    "SELECT VALUE u FROM MiniPortalEntities.UserProfiles AS u " +
            //    "WHERE u.loginName=@loginName";

            //ObjectParameter param = new ObjectParameter("loginName", loginName);

            //ObjectQuery<UserProfile> query =
            //  ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<UserProfile>(sqlQuery, param);

            //var result = query.Execute(MergeOption.AppendOnly);

            //try
            //{
            //    userProfile = result.First<UserProfile>();
            //}
            //catch (Exception)
            //{
            //    userProfile = null;
            //}

            #endregion Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            if (userProfile == null)
                throw new InstanceNotFoundException("No tienes seguidores",
                    typeof(User).FullName);

            return userProfile;
        }

        public List<Follow> FindFollowersByUserById(long id)
        {
            List<Follow> userProfile = null;

            #region Option 1: Using Linq.

            DbSet<Follow> followers = Context.Set<Follow>();

            var result =
                (from u in followers
                 where u.FollowId == id
                 select u);

            userProfile = (List<Follow>)result;

            #endregion Option 1: Using Linq.

            #region Option 2: Using eSQL over dbSet

            //string sqlQuery = "Select * FROM UserProfile where loginName=@loginName";
            //DbParameter loginNameParameter =
            //    new System.Data.SqlClient.SqlParameter("loginName", loginName);

            //userProfile = Context.Database.SqlQuery<UserProfile>(sqlQuery, loginNameParameter).FirstOrDefault<UserProfile>();

            #endregion Option 2: Using eSQL over dbSet

            #region Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            //String sqlQuery =
            //    "SELECT VALUE u FROM MiniPortalEntities.UserProfiles AS u " +
            //    "WHERE u.loginName=@loginName";

            //ObjectParameter param = new ObjectParameter("loginName", loginName);

            //ObjectQuery<UserProfile> query =
            //  ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<UserProfile>(sqlQuery, param);

            //var result = query.Execute(MergeOption.AppendOnly);

            //try
            //{
            //    userProfile = result.First<UserProfile>();
            //}
            //catch (Exception)
            //{
            //    userProfile = null;
            //}

            #endregion Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            if (userProfile == null)
                throw new InstanceNotFoundException("No tienes seguidores",
                    typeof(User).FullName);

            return userProfile;
        }

        public Follow FindFollowers(long id, long idf)
        {
            Follow userProfile = null;

            #region Option 1: Using Linq.

            DbSet<Follow> followers = Context.Set<Follow>();

            var result =
                (from u in followers
                 where u.FollowId == idf && u.UserId == id
                 select u);

            userProfile = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            #region Option 2: Using eSQL over dbSet

            //string sqlQuery = "Select * FROM UserProfile where loginName=@loginName";
            //DbParameter loginNameParameter =
            //    new System.Data.SqlClient.SqlParameter("loginName", loginName);

            //userProfile = Context.Database.SqlQuery<UserProfile>(sqlQuery, loginNameParameter).FirstOrDefault<UserProfile>();

            #endregion Option 2: Using eSQL over dbSet

            #region Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            //String sqlQuery =
            //    "SELECT VALUE u FROM MiniPortalEntities.UserProfiles AS u " +
            //    "WHERE u.loginName=@loginName";

            //ObjectParameter param = new ObjectParameter("loginName", loginName);

            //ObjectQuery<UserProfile> query =
            //  ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<UserProfile>(sqlQuery, param);

            //var result = query.Execute(MergeOption.AppendOnly);

            //try
            //{
            //    userProfile = result.First<UserProfile>();
            //}
            //catch (Exception)
            //{
            //    userProfile = null;
            //}

            #endregion Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            if (userProfile == null)
                throw new InstanceNotFoundException("No tienes seguidores",
                    typeof(User).FullName);

            return userProfile;
        }

        public void AddFollow(long userId, Follow userFollow)
        {
            User c = Find(userId);
            if (c != null)
            {
                c.Follow.Add(userFollow);
            }
            Update(c);
        }

        public void RemoveFollow(long userId, Follow userFollow)
        {
            User c = Find(userId);
            if (c != null)
            {
                c.Follow.Clear();
            }
            Update(c);
        }
        #endregion IUsuarioDao Members
    }
}
