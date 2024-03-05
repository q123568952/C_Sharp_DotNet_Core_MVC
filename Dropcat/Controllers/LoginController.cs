using Dropcat.Data;
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
        private readonly ILogger<LoginController> _logger;
        private readonly ApplicationDbContext dbContext;
        private readonly UserService userService;
        public LoginController(ILogger<LoginController> logger, ApplicationDbContext context, UserService userService)
        {
            _logger = logger;
            dbContext = context;
            this.userService = userService;
        }



        [HttpPost]
        public IActionResult Login( [FromBody] loginData loginData)
        {
          
            String identifier = loginData.username;
     
            String password = loginData.password;
            Console.WriteLine(identifier + password);
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