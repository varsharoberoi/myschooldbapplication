using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myschooldbapplication.Infrastructure;
using Rotativa.AspNetCore;

namespace myschooldbapplication.Controllers
{
   
    public class demoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            var model = new TestModel { Name = "Giorgio" };
            return new ViewAsPdf(model, ViewData);
        }

        public IActionResult sessiondemo()
        {
            var ob = new Product() { ProductID = 1, Name = "Pen", Price = 90 };
            HttpContext.Session.SetJson("Cart", ob);
            return RedirectToAction("First");
        }
        public IActionResult First()
        {
           var obj= HttpContext.Session.GetJson<Product>("Cart");
            return View(obj);

        }
    }
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}