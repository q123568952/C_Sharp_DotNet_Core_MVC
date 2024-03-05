using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dropcat.Models
{
    public class UserInfo
    {
        
    public int id {  get; set; }
       
    public string userAccount {  get; set; }
       
    public string? username {  get; set; }
      
    public string phonenumber {  get; set; }
      
    public string email {  get; set; }

    public string password {  get; set; }

     public DateTime? createtime {  get; set; }

     public DateTime? edittime {  get; set; }

    public string? usericon {  get; set; }

    public string? introduction {  get; set; }

    private int? gender {  get; set; }

    public string? resetToken {  get; set; }

    public DateTime? tokenTime {  get; set; }
    }
}