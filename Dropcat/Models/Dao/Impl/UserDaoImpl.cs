using MySql.Data.MySqlClient;
using System.Data;

namespace Dropcat.Models.Dao.Impl
{
    public class UserDaoImpl : UserDao
    {
        MySqlConnection conn;
        string connstr = "server=dropcatasia.c10mg2ikizc4.ap-northeast-3.rds.amazonaws.com;port=3306;user=admin;password=Dropcat666;database=Dropcat;Charset=utf8;";

        public UserDaoImpl()
        {
            conn = new MySqlConnection(connstr);
            if(conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            
        }

       
        public UserInfo findByEmail(string email)
        {
            string sql = @"select email,password from UserInfo where email =@email";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", email);
            MySqlDataReader reader = cmd.ExecuteReader();
            UserInfo user = new UserInfo();
            while (reader.Read())
            {
               
                user.email = (string)reader["email"];
                user.password = (string)reader["password"];
            }

                return user;
        }

        public UserInfo findByPassword(string password)
        {
            throw new NotImplementedException();
        }

        public UserInfo findByPhonenumber(string phonenumber)
        {
            string sql = @"select phonenumber,password from UserInfo where phonenumber =@phonenumber";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@phonenumber", phonenumber);
            MySqlDataReader reader = cmd.ExecuteReader();
            UserInfo user = new UserInfo();
            while (reader.Read())
            {

                user.phonenumber = (string)reader["phonenumber"];
                user.password = (string)reader["password"];
            }

            return user;
        }

        public UserInfo findByUserAccount(string userAccount)
        {
            string sql = @"select userAccount,password from UserInfo where userAccount =@userAccount";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userAccount", userAccount);
            MySqlDataReader reader = cmd.ExecuteReader();
            UserInfo user = new UserInfo();
            while (reader.Read())
            {

                user.userAccount = (string)reader["userAccount"];
                user.password = (string)reader["password"];
            }

            return user;
        }
    }
}