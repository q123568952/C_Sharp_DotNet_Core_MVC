using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dropcat.Models.Dao
{
    public interface UserDao
    {
        UserInfo findByUserAccount(String userAccount);

        UserInfo findByPhonenumber(String phonenumber);

        UserInfo findByEmail(String email);

        UserInfo findByPassword(String password);
    }
}