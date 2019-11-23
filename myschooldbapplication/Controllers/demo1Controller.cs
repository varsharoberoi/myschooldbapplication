using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myschooldbapplication.Infrastructure;
using myschooldbapplication.Models;
using System.Drawing;
namespace myschooldbapplication.Controllers
{
    public class demo1Controller : Controller
    {
        private readonly myschooldbContext _context;

        public demo1Controller(myschooldbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var res1 = _context.Myviews;
          
            return View();
        }

       public IActionResult testdemo()
        {
            var obj = HttpContext.Session.GetJson<Product>("Cart");
            return View(obj);
        }

        [HttpGet]
        public IActionResult Indexlist()
        {
            
                List<Guid> iamgeIds = _context.Images.Select(m => m.Id).ToList();
                return View(iamgeIds);
            
        }

        [HttpPost]
        public IActionResult UploadImage(IList<IFormFile> files)
        {
            IFormFile uploadedImage = files.FirstOrDefault();
            if (uploadedImage == null || uploadedImage.ContentType.ToLower().StartsWith("image/"))
            {
               
                    MemoryStream ms = new MemoryStream();
                    uploadedImage.OpenReadStream().CopyTo(ms);

                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                    Models.Image imageEntity = new Models.Image()
                    {
                        Id = Guid.NewGuid(),
                        Name = uploadedImage.FileName,
                        Data = ms.ToArray(),
                        Width = image.Width,
                        Height = image.Height,
                        ContentType = uploadedImage.ContentType
                    };

                    _context.Images.Add(imageEntity);

                    _context.SaveChanges();
                }
            

            return RedirectToAction("Index");
        }

        [HttpGet]
        public FileStreamResult ViewImage(Guid id)
        {

            Models.Image image = _context.Images.FirstOrDefault(m => m.Id == id);

                MemoryStream ms = new MemoryStream(image.Data);

                return new FileStreamResult(ms, image.ContentType);
            
        }
    }
}