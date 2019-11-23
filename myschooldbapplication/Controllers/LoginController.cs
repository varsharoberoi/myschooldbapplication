using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myschooldbapplication.Models;
using myschooldbapplication.Models.IRepository;


namespace myschooldbapplication.Controllers
{
    public class LoginController : Controller
    {
       private ILogin _login;
        public LoginController(ILogin login)
        {
            _login = login;
        }
      public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(UserInfo uv)
        {
           int result= _login.ValidateUser(uv);
            if(result==0)
            {


                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
                {
                    HttpContext.Session.SetString("Session1", "Welcome " + uv.UserID);

                }
                return RedirectToAction("Index", "Home");
            }
            else if(result==1)
            {
                ViewBag.user = "User Not Available";
                return View();
            }
            else
            {
                ViewBag.Password = "Password Wrong";
                return View();
            }

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Session1");
            return RedirectToAction("UserLogin", "Login");
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}