using ClosedXML;
using Dropcat.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

namespace Dropcat.Models.Dao.Impl
{
    public class UserDaoImpl : UserDao
    {
        private readonly ApplicationDbContext appcontext;


        public UserDaoImpl(ApplicationDbContext context)
        {
            appcontext = context;

        }
         
       
        public UserInfo findByEmail(string email)
        {
            
                return appcontext.UserInfo.FirstOrDefault(u => u.email == email);
        }

        public UserInfo findByPassword(string password)
        {
            throw new NotImplementedException();
        }

        public UserInfo findByPhonenumber(string phonenumber)
        {

            return appcontext.UserInfo.FirstOrDefault(u => u.phonenumber == phonenumber);
        }

        public UserInfo findByUserAccount(string userAccount)
        {

            return appcontext.UserInfo.FirstOrDefault(u => u.userAccount == userAccount);
        }
    }
}