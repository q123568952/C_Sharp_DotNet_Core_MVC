using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dropcat.Models
{
    internal interface UserService
    {
         UserInfo login(String identifier, String passwd);
         Boolean validatePassword(String inputPasswd, String storedPasswd, UserInfo user);
    }
}
