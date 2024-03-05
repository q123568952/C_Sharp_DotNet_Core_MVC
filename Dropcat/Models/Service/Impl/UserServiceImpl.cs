
using BCrypt.Net;
using Dropcat.Models.Dao;
using Dropcat.Models.Dao.Impl;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Dropcat.Models.Service.Impl
{
    public class UserServiceImpl : UserService
    {
        private readonly UserDao userDao;
        public UserServiceImpl(UserDao userDao)
        {
            this.userDao = userDao;
        }


        public UserInfo login(string identifier, string passwd)
        {
            UserInfo userInfo;
   
            if (identifier.Contains("@"))
            {
                userInfo = userDao.findByEmail(identifier);
       
            }
            else if (Regex.IsMatch(identifier, "^09\\d{8}$"))
            {
                userInfo = userDao.findByPhonenumber(identifier);
              
            }
            else
            {
                userInfo = userDao.findByUserAccount(identifier);
                
            }

            if (userInfo != null && validatePassword(passwd, userInfo.password, userInfo))
            {
                return userInfo;
            }


            return null;
        }

        public bool validatePassword(string inputPasswd, string storedPasswd, UserInfo user)
        {

            if (storedPasswd.StartsWith("$2a$"))
            {

                return BCrypt.Net.BCrypt.Verify(inputPasswd, storedPasswd);

            }
            else
            {
                return false;
            }
        }
    }
}