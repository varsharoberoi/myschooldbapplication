using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using myschooldbapplication.Models;
using myschooldbapplication.Models.IRepository;
using myschooldbapplication.Models.ViewModel;
using myschooldbapplication.Services;
using Rotativa.AspNetCore;

namespace myschooldbapplication.Controllers
{
    public class AdmissionController : Controller
    {
        IStudent StudentRepository;
        IAdmission _admission;
        IAdmissionPay _admissionPay;
        IStandard _Standard;
        IHostingEnvironment env;
        IEmailService emailService;
        public AdmissionController(IStudent repository,IAdmission admission,IAdmissionPay admissionPay,IStandard standard, IHostingEnvironment env,IEmailService emailService)
        {
            
            StudentRepository = repository;
            _admission = admission;
            _admissionPay = admissionPay;
            _Standard = standard;
            this.env = env;
            this.emailService = emailService;
        }
       
        public IActionResult printreceiptdemo(int? ID1)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            List<AdPay> p1 = new List<AdPay>();
            if (ID1 != null)
            {
                var res = _admission.GetById(ID1.Value);


                ICollection<Admissiopay> admissiopays = _admissionPay.GetAll();
                var res1 = admissiopays.Where(d => d.AdId == res.AdId);

                AdPay adPay = new AdPay();
                adPay.Admission1 = res;
                adPay.ListAdmissiopay = res1;
                p1.Add(adPay);
               
                return View("printreceipt",p1);
                // return new ViewAsPdf(res);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public  IActionResult printreceipt(int? ID1)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            List<AdPay> p1 = new List<AdPay>();
            if (ID1!=null)
            {
               var res= _admission.GetById(ID1.Value);
               

                    ICollection<Admissiopay> admissiopays = _admissionPay.GetAll();
                    var res1 = admissiopays.Where(d => d.AdId == res.AdId);
                
                    AdPay adPay = new AdPay();
                    adPay.Admission1 = res;
                    adPay.ListAdmissiopay = res1;
                    p1.Add(adPay);
                string subject = "Greeting from Blossom Buds";
                string message = "Dear Sir/Ma'am, <br> admission done successfully";
               var resinfo= new ViewAsPdf(p1);
               Task<byte[]> bytes= resinfo.BuildFile(ControllerContext);
                System.IO.File.WriteAllBytes(env.WebRootPath + "\\info.pdf",bytes.Result);
                 emailService.SendEmail(adPay.Admission1.St.EmailId, subject, message);
                return new ViewAsPdf(p1);
            }
            else
            {
                return  View();
            }
        }
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            return View();
        }
        public IActionResult Student(Student student)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (ModelState.IsValid)
            {
                StudentRepository.Create(student);


                var routeValues = new RouteValueDictionary {
                { "ID",student.StId},
  { "name",student.StName
                }
};
                return RedirectToAction("Create", "Admission1", routeValues);
            }
          
            else
            {
                return View(student);
            }
        }
        [HttpPost]
        public ActionResult Searchadmn(UserSearch usr)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if(usr.Ad_ID>0 &&usr.St_Name!=null&&usr.Standard!=null)
            {
                var routeValues = new RouteValueDictionary {
                { "ID",usr.Ad_ID},{"name",usr.St_Name },{ "standard",usr.Standard} };
                return RedirectToAction("AdmissionPay", routeValues);
            }
            else if(usr.Ad_ID>0 && usr.St_Name!=null)
            {
                var routeValues = new RouteValueDictionary {
                { "ID",usr.Ad_ID},{"name",usr.St_Name } };
                return RedirectToAction("AdmissionPay", routeValues);
            }
            else if (usr.Ad_ID > 0)
            {
                var routeValues = new RouteValueDictionary {
                { "ID",usr.Ad_ID} };
                return RedirectToAction("AdmissionPay", routeValues);
            }
            else if (usr.Ad_ID > 0&&usr.Standard!=null)
            {
                var routeValues = new RouteValueDictionary {
                { "ID",usr.Ad_ID},{ "standard",usr.Standard} };
                return RedirectToAction("AdmissionPay", routeValues);
            }
            else if(usr.St_Name!=null)
            {
                var routeValues = new RouteValueDictionary {
                {"name",usr.St_Name } };
                return RedirectToAction("AdmissionPay", routeValues);
            }
            else if (usr.Standard != null)
            {
                var routeValues = new RouteValueDictionary {
                {"standard",usr.Standard } };
                return RedirectToAction("AdmissionPay", routeValues);
            }
            /* if (usr.Ad_ID > 0)
             {
                 var routeValues = new RouteValueDictionary {
                 { "ID",usr.Ad_ID} };
               return  RedirectToAction("AdmissionPay",routeValues );
             }
             else if(usr.St_Name!=null)
             {
                 var routeValues = new RouteValueDictionary {
                 { "name",usr.St_Name} };
                 return 
                 RedirectToAction("AdmissionPay", routeValues);
             }
             else if(usr.Standard!=null)
             {
                 var routeValues = new RouteValueDictionary {
                 { "standard",usr.Standard} };
                 return RedirectToAction("AdmissionPay", routeValues);
             }*/
            return RedirectToAction("AdmissionPay");
        }
        List<AdPay> p = new List<AdPay>();
        public IActionResult AdmissionPay(int? ID,string name,int? standard )
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            TempData["StdId"] = new SelectList(_Standard.GetAll().ToList(), "StdId", "Stname");
            var res= _admission.GetAll();


            if(ID!=null && name!=null && standard!=null)
            {
                res = from k in res
                      where k.AdId == ID && k.St.StName.Equals(name) && k.StdId == standard
                      select k;
               //res = res.Where(d => d.AdId == ID).Where(d => d.St.Name.Equals(name)).Where(d => d.StdId == standard);
                               
            }
            else if (ID > 0 && name != null)
            {
                res = from k in res
                      where k.AdId == ID && k.St.StName.Equals(name) 
                      select k;
            }
            else if (ID > 0)
            {
                res = from k in res
                      where k.AdId == ID 
                      select k;
            }
            else if (ID > 0 && standard != null)
            {
                res = from k in res
                      where k.AdId == ID && k.StdId==standard
                      select k;
            }
            else if (name != null)
            {
                res = from k in res
                      where k.St.StName.Equals(name)
                      select k;
               
            }
            else if (standard != null)
            {
                res = from k in res
                      where k.StdId==standard
                      select k;
            }
            else
            {
                res = from k in res
                      where k.AdId != ID && !k.St.StName.Equals(name) && k.StdId != standard
                      select k;
            }
            ViewBag.ID = ID;
            TempData["StdId"] = new SelectList(_Standard.GetAll().ToList(), "StdId", "Stname", standard);
            /*
                if(ID!=null)
                {
                res=    res.Where(k => k.AdId == ID);
                    ViewBag.ID = ID;
                }
                if(name!=null)
                {
                  var st_data= StudentRepository.GetAll();
                    res =
       from k in st_data
       join k1 in res on k.StId equals k1.StId
       where k.StName.Contains(name )
       select k1;
                }
                if(standard!=null)
                {
                    res = res.Where(k => k.StdId == standard);
                    TempData["StdId"] = new SelectList(_Standard.GetAll().ToList(), "StdId", "Stname",standard);
                }*/
            foreach (var item in res)
            {
             
                ICollection<Admissiopay> admissiopays = _admissionPay.GetAll();
               var res1= admissiopays.Where(d => d.AdId == item.AdId);
                AdPay adPay = new AdPay();
                adPay.Admission1 = item;
                adPay.ListAdmissiopay = res1;
                p.Add(adPay);
            }
            ViewBag.name = name;
            return View(p);
           //List<Admissiopay> x = new List<Admissiopay>();
           // foreach (var item in adm)
           // {
           //  var res1= _admissionPay.GetByAdmissionId(item.AdId);
           //     foreach (var item2 in res1)
           //     {
           //         x.Add(item2);
           //     }
           // }

           // p.ListAdmissiopay = x;
           //IEnumerable<Admissiopay> listpay= _admissionPay.GetAll();
           // p.ListAdmissiopay = listpay;
           // return View(p);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("AdId,AdTId,PayDate,Paystatus")] Admissiopay admissiopay,int? ID)
        {
            if (id != admissiopay.AdId)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (ModelState.IsValid)
            {
                try
                {
                    _admissionPay.Update(admissiopay);
                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!AdmissiopayExists(admissiopay.AdId))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                        throw;
                    //}
                }
                var routeValues = new RouteValueDictionary {
                { "ID",admissiopay.AdId} };

                return RedirectToAction("AdmissionPay", "Admission", routeValues);
            }
            
            else
            {
                return View();
            }
           // return View(admissiopay);
            //ViewData["AdId"] = new SelectList(p.a, "AdId", "AdId", admissiopay.AdId);
            //ViewData["AdTId"] = new SelectList(_context.AdTypeFee, "AdTId", "AdTId", admissiopay.AdTId);
            //return View(admissiopay);
        }

    }
}