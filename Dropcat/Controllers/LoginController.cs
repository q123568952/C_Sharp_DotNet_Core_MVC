using Dropcat.Models;
using Dropcat.Models.Service.Impl;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;


namespace Dropcat.Controllers
{
    public class LoginController : Controller
    {
       UserServiceImpl userService;
        //GET: Verify
        [HttpPost]
        public IActionResult Login( [FromBody] loginData loginData)
        {
          
             userService = new UserServiceImpl();
            String identifier = loginData.username;
     
            String password = loginData.password;

            UserInfo user = userService.login(identifier, password);

           

            if (user != null)
            {


                return StatusCode(200);

            }
            else
            {

                return StatusCode(400);

            }
        }
    }
}