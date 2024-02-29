using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Dropcat.Models
{
    public class User
    {

        public int id {  get; set; }
        public string? icon {  get; set; }
        public string userAccount {  get; set; }
        public string email {  get; set; }
        public int? gender {  get; set; }
        public string? username { get; set; }
        public DateTime? createtime {  get; set; }


    }
}