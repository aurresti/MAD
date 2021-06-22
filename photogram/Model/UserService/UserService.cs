using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.UserService.Exceptions;
using Es.Udc.DotNet.Photogram.Model.UserService.Util;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using System.Collections.Generic;
using Es.Udc.DotNet.Photogram.Model.ImageDao;

namespace Es.Udc.DotNet.Photogram.Model.UserService
{
    public class UserService : IUserService
    {
        [Inject]
        public IUserDao UserDao { private get; set; }       
        public IImageDao ImageDao { private get; set; }


        #region IUsuarioService Members

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]

        public bool UserExists(string loginName)
        {

            try
            {
                UserAccount userProfile = UserDao.FindByLoginName(loginName);
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }

            return true;
        }

        public bool UserFollowExists(string loginName, string loginNameFollow)
        {
            ///Fachada no generica
            try
            {
                ///UserAccount userProfile = UserDao.FindByLoginName(loginName);
                ///UserAccount userProfileFollow = UserDao.FindByLoginName(loginNameFollow);
                UserDao.FollowExists(loginName, loginNameFollow);
                //Follow follow = UsuarioDao.FindFollowers(userProfile.Id, userProfileFollow.Id);
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }

            return true;
        }

        public long CreateUser(string loginName, string password,
            UserProfile userProfileDetails)
        {

            try
            {
                UserDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(UserAccount).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(password);

                UserAccount userProfile = new UserAccount();

                userProfile.loginName = loginName;
                userProfile.password = encryptedPassword;
                userProfile.firstName = userProfileDetails.FirstName;
                userProfile.lastName = userProfileDetails.Lastname;
                userProfile.email = userProfileDetails.Email;
                userProfile.language = userProfileDetails.Language;
                userProfile.country = userProfileDetails.Country;

                UserDao.Create(userProfile);

                return userProfile.userId;
            }
        }

        public void UpdateProfile(long userProfileId,
            UserProfile userProfileDetails)
        {
            UserAccount userProfile =
                UserDao.Find(userProfileId);

            userProfile.firstName = userProfileDetails.FirstName;
            userProfile.lastName = userProfileDetails.Lastname;
            userProfile.email = userProfileDetails.Email;
            userProfile.language = userProfileDetails.Language;
            userProfile.country = userProfileDetails.Country;
            UserDao.Update(userProfile);
        }

        public LoginResult ConectUser(string loginName, string password, bool passwordIsEncrypted)
        {
            
                UserAccount userProfile = UserDao.FindByLoginName(loginName);
                String storedPassword = userProfile.password;
                if (passwordIsEncrypted)
                {
                    if (!password.Equals(storedPassword))
                    {
                        throw new IncorrectPasswordException(loginName);
                    }
                }
                else
                {
                    if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                            storedPassword))
                    {
                        throw new IncorrectPasswordException(loginName);
                    }
                }
                return new LoginResult(userProfile.userId, userProfile.firstName,
                    storedPassword, userProfile.language, userProfile.country);
            
        }

        public bool ExitUser(string loginName)
        {
            try
            {
                UserAccount userProfile = UserDao.FindByLoginName(loginName);
                return true;
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }
        }


        public List<UserAccount> SeeFollowers(long userId)
        {
            try
            {

                List<UserAccount> followers = UserDao.FindFollowersById(userId);

                return followers;

            }
            catch (InstanceNotFoundException e)
            {
                return null;
            }
        }

        public List<UserAccount> SeeFollow(long userId)
        {
            try
            {

                List<UserAccount> follow = UserDao.FindFollowById(userId);

                return follow;

            }
            catch (InstanceNotFoundException e)
            {
                return null;
            }
        }

        public bool FollowUser(long userId, long followId)
        {
            try
            {
                UserDao.AddFollow(userId, followId);

                return true;
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }
        }

        public bool UnFollowUser(long userId, long unFollowId)
        {
            try
            {
                UserDao.RemoveFollow(userId, unFollowId);

                return true;
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }
        }

        public void ChangePassword(long userProfileId, string oldClearPassword,
    string newClearPassword)
        {
            UserAccount userProfile = UserDao.Find(userProfileId);
            String storedPassword = userProfile.password;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.loginName);
            }

            userProfile.password =
                PasswordEncrypter.Crypt(newClearPassword);

            UserDao.Update(userProfile);
        }

        public UserProfile FindUserProfileDetails(long userProfileId)
        {
            UserAccount userProfile = UserDao.Find(userProfileId);

            UserProfile userProfileDetails =
                new UserProfile(userProfile.firstName,
                    userProfile.lastName, userProfile.email,
                    userProfile.language, userProfile.country);

            return userProfileDetails;
        }

        #endregion IUserService Members
    }
}
