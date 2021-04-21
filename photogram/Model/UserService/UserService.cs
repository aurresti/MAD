using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.UserService.Exceptions;
using Es.Udc.DotNet.Photogram.Model.UserService.Util;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Photogram.Model.UserService
{
    public class UserService : IUserService
    {
        [Inject]
        public IUserDao UsuarioDao { private get; set; }

        #region IUsuarioService Members

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]

        public bool UserExists(string loginName)
        {

            try
            {
                User userProfile = UsuarioDao.FindByLoginName(loginName);
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }

            return true;
        }

        public bool UserFollowExists(string loginName, string loginNameFollow)
        {

            try
            {
                User userProfile = UsuarioDao.FindByLoginName(loginName);
                User userProfileFollow = UsuarioDao.FindByLoginName(loginNameFollow);
                Follow follow = UsuarioDao.FindFollowers(userProfile.Id, userProfileFollow.Id);
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
                UsuarioDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(User).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(password);

                User userProfile = new User();

                userProfile.Login = loginName;
                userProfile.Password = encryptedPassword;
                userProfile.FirstName = userProfileDetails.FirstName;
                userProfile.LastName = userProfileDetails.Lastname;
                userProfile.Email = userProfileDetails.Email;
                userProfile.Language = userProfileDetails.Language;
                userProfile.Country = userProfileDetails.Country;

                UsuarioDao.Create(userProfile);

                return userProfile.Id;
            }
        }

        public void UpdateProfile(long userProfileId,
            UserProfile userProfileDetails)
        {
            User userProfile =
                UsuarioDao.Find(userProfileId);

            userProfile.FirstName = userProfileDetails.FirstName;
            userProfile.LastName = userProfileDetails.Lastname;
            userProfile.Email = userProfileDetails.Email;
            userProfile.Language = userProfileDetails.Language;
            userProfile.Country = userProfileDetails.Country;
            UsuarioDao.Update(userProfile);
        }

        public LoginResult ConectUser(string loginName, string password, bool passwordIsEncrypted)
        {
            
                User userProfile = UsuarioDao.FindByLoginName(loginName);
                String storedPassword = userProfile.Password;
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
                return new LoginResult(userProfile.Id, userProfile.FirstName,
                    storedPassword, userProfile.Language, userProfile.Country);
            
        }

        public bool ExitUser(string loginName)
        {
            try
            {
                User userProfile = UsuarioDao.FindByLoginName(loginName);
                return true;
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }
        }


        public List<Follow> SeeFollower(string loginName)
        {
            try
            {
                User userProfile = UsuarioDao.FindByLoginName(loginName);
                List<Follow> userFollow = UsuarioDao.FindFollowersByUserById(userProfile.Id);
                return userFollow;

            }
            catch (InstanceNotFoundException e)
            {
                return null;
            }
        }

        public List<Follow> SeeFollow(string loginName)
        {
            try
            {
                User userProfile = UsuarioDao.FindByLoginName(loginName);
                List<Follow> userFollow = UsuarioDao.FindFollowersById(userProfile.Id);
                return userFollow;

            }
            catch (InstanceNotFoundException e)
            {
                return null;
            }
        }

        public bool FollowUser(string loginName, string loginNameFollow)
        {
            try
            {
                User userProfile = UsuarioDao.FindByLoginName(loginName);
                User userProfileFollow = UsuarioDao.FindByLoginName(loginNameFollow);
                Follow follow = new Follow();
                follow.UserId = userProfile.Id;
                follow.FollowId = userProfileFollow.Id;
                if (!UserFollowExists(userProfile.Login, userProfileFollow.Login))
                {
                    UsuarioDao.AddFollow(userProfile.Id,follow);
                    return true;
                } else {
                    return false;
                }
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }
        }

        public void ChangePassword(long userProfileId, string oldClearPassword,
    string newClearPassword)
        {
            User userProfile = UsuarioDao.Find(userProfileId);
            String storedPassword = userProfile.Password;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.Login);
            }

            userProfile.Password =
                PasswordEncrypter.Crypt(newClearPassword);

            UsuarioDao.Update(userProfile);
        }

        public UserProfile FindUserProfileDetails(long userProfileId)
        {
            User userProfile = UsuarioDao.Find(userProfileId);

            UserProfile userProfileDetails =
                new UserProfile(userProfile.FirstName,
                    userProfile.LastName, userProfile.Email,
                    userProfile.Language, userProfile.Country);

            return userProfileDetails;
        }
        #endregion IUserService Members
    }
}
