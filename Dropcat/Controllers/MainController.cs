using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Dropcat.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Dropcat.Controllers
{
    public class MainController : Controller
    {
        MySqlConnection conn;
        List<User> userlist;
        // GET: User
        public IActionResult ListUser()
        {
            String sql = "SELECT id, usericon, userAccount, email, username, createtime  FROM UserInfo";
            string connstr = "server=dropcatasia.c10mg2ikizc4.ap-northeast-3.rds.amazonaws.com;port=3306;user=admin;password=Dropcat666;database=Dropcat;Charset=utf8;";
            conn = new MySqlConnection(connstr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            userlist = new List<User>();
            while (reader.Read())
            {
                User user = new User();
                user.id = (int)reader["id"];
                user.icon = (string)reader["usericon"];
                user.userAccount = (string)reader["userAccount"];
                user.email = (string)reader["email"];
                user.username = (string)reader["username"];
                user.createtime = (DateTime)reader["createtime"];
                userlist.Add(user);
            }
            



            return View(userlist);
        }

        // 輸出Excel 表格的方法
        public IActionResult ExportExcel()
        {
            //取得欄位名稱
            var columnNameList = typeof(User).GetProperties().Select(c => c.Name).ToList();

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "exportUserDataList.xlsx";

            //建立Excel
            var workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Users");
            //設定標題列名稱與樣式
            for (int i = 1; i <= columnNameList.Count(); i++)
            {
                worksheet.Cell(1, i).Value = columnNameList[i - 1];
                worksheet.Cell(1, i).Style.Fill.SetBackgroundColor(XLColor.CadmiumYellow);
                worksheet.Cell(1, i).Style.Font.SetFontSize(12);
                worksheet.Cell(1, i).Style.Font.SetBold();
            }

            String sql = "SELECT id, usericon, userAccount, email, username, createtime  FROM UserInfo";
            string connstr = "server=dropcatasia.c10mg2ikizc4.ap-northeast-3.rds.amazonaws.com;port=3306;user=admin;password=Dropcat666;database=Dropcat;Charset=utf8;";
            conn = new MySqlConnection(connstr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            userlist = new List<User>();
            while (reader.Read())
            {
                User user = new User();
                user.id = (int)reader["id"];
                user.icon = (string)reader["usericon"];
                user.userAccount = (string)reader["userAccount"];
                user.email = (string)reader["email"];
                user.username = (string)reader["username"];
                user.createtime = (DateTime)reader["createtime"];
                userlist.Add(user);
            }

            List<User> allUserInfoDataList = userlist;



            for (int j = 1; j <= allUserInfoDataList.Count(); j++)
            {
                worksheet.Cell(j + 1, 1).Value = allUserInfoDataList[j - 1].id;
                worksheet.Cell(j + 1, 2).Value = allUserInfoDataList[j - 1].icon;
         
                worksheet.Cell(j + 1, 3).Value = allUserInfoDataList[j - 1].userAccount;
                worksheet.Cell(j + 1, 4).Value = allUserInfoDataList[j - 1].email;
      
                worksheet.Cell(j + 1, 5).Value = allUserInfoDataList[j - 1].gender;
                worksheet.Cell(j + 1, 6).Value = allUserInfoDataList[j - 1].username;
                worksheet.Cell(j + 1, 7).Value = allUserInfoDataList[j - 1].createtime;
              

            }

           
                worksheet.Columns(1,7).AdjustToContents();
            

            //寫入檔案
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, contentType, fileName);
            }
        }
    }
}