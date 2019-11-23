using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public class EnrollmentController : Controller
    {
        private readonly myschooldbContext _context;
        private readonly IEmailService emailService;
        IAdmission _admission;
        IAdmissionPay _admissionPay;
        IStandard _Standard;
        IHostingEnvironment env;
        public EnrollmentController(myschooldbContext context, IEmailService emailService,IAdmission _admission,IAdmissionPay _adpay,IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.emailService = emailService;
            this._admission = _admission;
            this._admissionPay = _adpay;
            this.env = hostingEnvironment;
        }
        public IActionResult Index(int? id,string name)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (id.HasValue)
            {

                var res = _context.Admission1.Include(k => k.Std).Include(n => n.Admissiopay).Include(n=>n.FT).ToList<Models.Admission1>();
               res= res.Where(d => d.AdId == id).ToList<Admission1>();
               Admission1 admissiopay= res.First();
                
                return View(admissiopay);
            }

            return View();
            
            
        }
        [HttpPost]
        public ActionResult Searchadmn(ListModel usr)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            usr.ctrl1 = 1;
             if (usr.UserSearch.St_Name != null)
            {
                var routeValues = new RouteValueDictionary {
                { "name",usr.UserSearch.St_Name} };
                return RedirectToAction("Indexdemo", routeValues);
            }
            
            return View();
        }

        public ActionResult enrollupdatefee(ListModel data)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            data.EnrollModel.paystatusid = data.EnrollModel.paystatusid.Remove(data.EnrollModel.paystatusid.Length - 1, 1);
                        string[] paidid = data.EnrollModel.paystatusid.Split(",");
                    
                        foreach (var item1 in paidid)
                        {
                            string[] modes = item1.Split(".");
                            var res = _context.Admissiopay.Find(data.EnrollModel.AdId, Convert.ToInt32(modes[0]));
                            res.Paystatus = true;
                            res.PayDate = Convert.ToDateTime(modes[1]);
                            if (modes[2].ToString() == "1")
                            {
                                res.Pay_mode = "Cash";
                            }
                            else if (modes[2].ToString() == "2")
                            {
                                res.Pay_mode = "Cheque";
                                res.Chequeno = modes[3].ToString();
                                res.Chequedt = Convert.ToDateTime(modes[4]);
                                res.BankName = modes[5].ToString();
                                res.BankBranch = modes[6].ToString();

                }
                            else if (modes[2].ToString() == "3")
                            {
                                res.Pay_mode = "netbanking";
                                res.Transactionno = modes[3].ToString();
                            }
                            _context.SaveChanges();
                        }
                    
                        ViewBag.display = "div4";
                        ViewBag.ADID = data.EnrollModel.AdId;
                        return View("FeePay", data);
                    
        }

        [HttpPost]
        public ActionResult Searchadmnupdate(ListModel usr,string id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            usr.ctrl1 = 1;
            if (usr.UserSearch.St_Name != null)
            {
                var routeValues = new RouteValueDictionary {
                { "name",usr.UserSearch.St_Name} };
                if (id == "update")
                {
                    return RedirectToAction("updatestudent", routeValues);
                }
                else
                {
                    return RedirectToAction("FeePay", routeValues);
                }
            }
            else
            {
                if (id == "update")
                {
                    return RedirectToAction("updatestudent");
                }
                else
                {
                    return RedirectToAction("FeePay");

                }
            }
        }
        public IActionResult listview(string name)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (name!=null)
            {
               
                var res = _context.Admission1.Include(k => k.St).Include(k=>k.Std).Include(n => n.Admissiopay).Include(n => n.FT).ToList<Models.Admission1>();
                res = res.Where(d => d.St.StName ==name).ToList<Admission1>();
                ListModel listModel = new ListModel() { UserSearch = new UserSearch() { St_Name = name } };
                List<EnrollModel> list = new List<EnrollModel>();
                foreach (var item in res)
                {
                    list.Add(new EnrollModel() { AdId =item.AdId,Stname=item.St.StName,ParentName=item.St.ParentName,MotherName=item.St.MotherName});
                }
                listModel.EnrollModels = list;
                ViewBag.listdata = list;
                RouteValueDictionary keys = new RouteValueDictionary();
                keys.Add("listdata", listModel);
                return RedirectToAction("Indexdemo",keys);
            }
            return View();
        }
       
        public IActionResult Indexdemo(string name)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (name != null)
            {
                List<EnrollModel> list = Fill_Adlist(name);
                ListModel listModel = new ListModel() { UserSearch = new UserSearch() { St_Name = name } };
                listModel.EnrollModels = list;
                listModel.ctrl1 = 1;
                ViewBag.display = "div2";

                //   fillinitialdata();
                return View(listModel);
            }
            else
            {
                
                return View();
            }
        }

        private List<EnrollModel> Fill_Adlist(string name)
        {

            var res = _context.Admission1.Include(k => k.St).Include(k => k.Std).Include(n => n.Admissiopay).Include(n => n.FT).ToList<Models.Admission1>();
            res = res.Where(d => d.St.StName.ToLower() == name.ToLower()).ToList<Admission1>();

            List<EnrollModel> list = new List<EnrollModel>();
            foreach (var item in res)
            {
                list.Add(new EnrollModel() { AdId = item.AdId, Stname = item.Std.Stname, StId = item.St.StId, StName = item.St.StName, ParentName = item.St.ParentName, MotherName = item.St.MotherName });
            }

            return list;
        }

        [HttpPost]
        public IActionResult Indexdemo(ListModel listModel)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (listModel.ctrl1==0)
            {
                ViewBag.ctrl = 0;
                ViewBag.display = "div1";
                fillinitialdata();
            }
            else if(listModel.ctrl1 == 1)
            {
                ViewBag.ctrl = 1;
                ViewBag.display = "div2";
            }
            return View(listModel);
        }
        public IActionResult createnew()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            fillinitialdata();

            return View();
        }
        public IActionResult Create(  )
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            fillinitialdata();

            return View();
        }

        private void fillinitialdata()
        {
           
            var res = _context.Admission1.Include(a => a.FT).Include(a => a.St).Include(a => a.Std);

            ViewData["FTId"] = new SelectList(_context.FeeType, "FTId", "Feetype1");
            ViewData["FTId"] = new SelectList(_context.FeeType, "FTId", "Feetype1");

            ViewData["StdId"] = new SelectList(_context.Std, "StdId", "Stname");

            var resyear = (from k in _context.FeeType
                           select new { k.FeeYear, k.Feeyeartext }).Distinct();
            ViewData["Feeyear"] = new SelectList(resyear,"FeeYear","Feeyeartext");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ListModel data,string ctrl)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Student student;
                        if (data.EnrollModel.StId.HasValue)
                        {
                            
                            if (data.EnrollModel.files != null)
                            {
                                IFormFile uploadedImage = null;
                                MemoryStream ms = null;
                                uploadedImage = data.EnrollModel.files;
                                System.Drawing.Image image = null;
                                if (uploadedImage != null || uploadedImage.ContentType.ToLower().StartsWith("image/"))
                                {

                                 ms = new MemoryStream();
                                    uploadedImage.OpenReadStream().CopyTo(ms);

                                     image = System.Drawing.Image.FromStream(ms);
                                }

                                data.ctrl1 = 1;
                                ViewBag.display = "div2";
                                student = _context.Student.Find(data.EnrollModel.StId);
                                student.Dob = data.EnrollModel.Dob;
                                student.EmailId = data.EnrollModel.EmailId;
                                student.Gender = data.EnrollModel.Gender;
                                student.MotherName = data.EnrollModel.MotherName;
                                student.MotherOccu = data.EnrollModel.MotherOccu;
                                student.MotherPhone = data.EnrollModel.MotherPhone;
                                student.MotherQualification = data.EnrollModel.MotherQualification;
                                student.MotherWhatsapp = data.EnrollModel.MotherWhatsapp;
                                student.Nationality = data.EnrollModel.Nationality;
                                student.Occupation = data.EnrollModel.Occupation;
                                student.ParentMobile = data.EnrollModel.ParentMobile;
                                student.ParentName = data.EnrollModel.ParentName;
                                student.ParentWhatsappNo = data.EnrollModel.ParentWhatsappNo;
                                student.POB = data.EnrollModel.POB;
                                student.Qualification = data.EnrollModel.Qualification;
                                student.StId = data.EnrollModel.StId.Value;
                                student.StName = data.EnrollModel.StName;
                                student.Address = data.EnrollModel.Address;
                                if (uploadedImage != null)
                                {
                                    student.Id = Guid.NewGuid();
                                    student.Name = uploadedImage.FileName;
                                    student.Data = ms.ToArray();
                                    student.Width = image.Width;
                                    student.Height = image.Height;
                                    student.ContentType = uploadedImage.ContentType;
                                }
                                _context.Entry(student).State = EntityState.Modified;
                                _context.SaveChanges();
                            }
                        }
                        else
                        {
                            data.ctrl1 = 0;
                            ViewBag.display = "div1";
                            IFormFile uploadedImage =data.EnrollModel.files;
                            if (uploadedImage != null || uploadedImage.ContentType.ToLower().StartsWith("image/"))
                            {

                                MemoryStream ms = new MemoryStream();
                                uploadedImage.OpenReadStream().CopyTo(ms);

                                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);


                                // fillinitialdata();
                                student = new Student()
                                {
                                    StName = data.EnrollModel.StName,
                                    ParentName = data.EnrollModel.ParentName,
                                    Dob = data.EnrollModel.Dob,
                                    EmailId = data.EnrollModel.EmailId,
                                    ParentMobile = data.EnrollModel.ParentMobile,
                                    Gender = data.EnrollModel.Gender,
                                    MotherName = data.EnrollModel.MotherName,
                                    MotherOccu = data.EnrollModel.MotherOccu,
                                    MotherPhone = data.EnrollModel.MotherPhone,
                                    MotherQualification = data.EnrollModel.MotherQualification,
                                    MotherWhatsapp = data.EnrollModel.MotherWhatsapp,
                                    Nationality = data.EnrollModel.Nationality,
                                    Occupation = data.EnrollModel.Occupation,
                                    ParentWhatsappNo = data.EnrollModel.ParentWhatsappNo,
                                    POB = data.EnrollModel.POB,
                                    Qualification = data.EnrollModel.Qualification,
                                    Address = data.EnrollModel.Address,
                                    Id = Guid.NewGuid(),
                                    Name = uploadedImage.FileName,
                                    Data = ms.ToArray(),
                                    Width = image.Width,
                                    Height = image.Height,
                                    ContentType = uploadedImage.ContentType
                                };
                                _context.Student.Add(student);
                                _context.SaveChanges();
                                data.EnrollModel.StId = student.StId;
                            }
                          
                           
                        }
                        Admission1 admission = new Admission1() { StId = data.EnrollModel.StId, StdId=data.EnrollModel.StdId,
                            FTId = data.EnrollModel.FTId, FeeYear = data.EnrollModel.FeeYear };
                        _context.Admission1.Add(admission);
                        _context.SaveChanges();
                        data.AdId = admission.AdId;
                        data.EnrollModel.paystatusid = data.EnrollModel.paystatusid.Remove(data.EnrollModel.paystatusid.Length - 1, 1);
                        string[] paidid = data.EnrollModel.paystatusid.Split(",");
                        //var res= _context.Admissiopay.Where(p => p.AdId == admission.AdId);
                        // foreach (var item in res)
                        // {
                        foreach (var item1 in paidid)
                        {
                            string[] modes = item1.Split(".");
                            var res = _context.Admissiopay.Find(admission.AdId, Convert.ToInt32(modes[0]));
                            res.Paystatus = true;
                            res.PayDate = Convert.ToDateTime(modes[1]);
                            if (modes[2].ToString() == "1")
                            {
                                res.Pay_mode = "Cash";
                            }
                            else if (modes[2].ToString() == "2")
                            {
                                res.Pay_mode = "Cheque";
                                res.Chequeno = modes[3].ToString();
                                res.Chequedt = Convert.ToDateTime(modes[4]);
                                res.BankName = modes[5].ToString();
                                res.BankBranch = modes[6].ToString();

                            }
                            else if (modes[2].ToString() == "3")
                            {
                                res.Pay_mode = "netbanking";
                                res.Transactionno = modes[3].ToString();
                            }
                            _context.SaveChanges();
                        }
                        transaction.Commit();
                        // TempData["emodel"] = data;
                        ViewBag.display = "div4";
                        ViewBag.ADID = data.AdId;
                       
                        return View("Indexdemo", data);
                        //return RedirectToAction("printreceiptdemo", "Admission", new { ID1 = data.AdId});
                    }
                    catch(SqlException e)
                    {
                        transaction.Rollback();
                        fillinitialdata();

                        return View("Indexdemo", data);
                    }
                    catch (Exception e1)
                    {
                       
                        
                        fillinitialdata();

                        return View("Indexdemo",data);

                    }
                   
                }
            }
            else
            {
                if (data.EnrollModel.StId.HasValue)
                {
                         
                    
           
                    List<EnrollModel> list = Fill_Adlist(data.EnrollModel.StName);
                    data.AdId = data.EnrollModel.AdId.Value;
                    data.UserSearch = new UserSearch() { St_Name = data.EnrollModel.StName };
                    data.EnrollModels = list;
                    fillinitialdata();
                    data.ctrl1 = 1;
                    var res1 = _context.FeeType.Where(d => d.FTId == data.EnrollModel.FTId);
                    ViewData["FTId"] = new SelectList(res1, "FTId", "Feetype1");
                    ViewBag.display = "div2";
                    ViewBag.studentdiv = "div3";
                    //  return View("Indexdemo", data);
                }
                else
                {
                    data.ctrl1 = 0;
                    ViewBag.display = "div1";
                    var res1 = _context.FeeType.Where(d => d.FTId == data.EnrollModel.FTId);
                    ViewData["FTId"] = new SelectList(res1, "FTId", "Feetype1");
                    fillinitialdata();
                }
               
                ViewBag.domdata = data.EnrollModel.divdata;
                return View("Indexdemo", data);
            }
            }
        [HttpPost]
        public IActionResult selectstudent(ListModel listModel)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            Admission1 admission = _context.Admission1.Find(listModel.AdId);
            Student student = _context.Student.Find(admission.StId);
          
             listModel.EnrollModel = new EnrollModel() { AdId = admission.AdId, StId = student.StId, POB = student.POB, StName = student.StName, Dob = student.Dob, EmailId = student.EmailId, Gender = student.Gender, MotherName = student.MotherName, MotherOccu = student.MotherOccu, MotherPhone = student.MotherPhone, MotherQualification = student.MotherQualification, MotherWhatsapp = student.MotherWhatsapp, Nationality = student.Nationality, Occupation = student.Occupation, ParentMobile = student.ParentMobile, ParentName = student.ParentName, ParentWhatsappNo = student.ParentWhatsappNo, Qualification = student.Qualification, Address = student.Address,Pic=student.Id };
            ViewBag.enrolldata = listModel.EnrollModel;
            ViewBag.display = "div2";
            ViewBag.studentdiv = "div3";
            List<EnrollModel> list = Fill_Adlist(student.StName);

            listModel.UserSearch = new UserSearch() { St_Name = student.StName };
            listModel.EnrollModels = list;

            fillinitialdata();

            return View("Indexdemo", listModel);
        }
        [HttpGet]
        public FileStreamResult ViewImage(int? id)
        {
            Models.Student image = _context.Student.FirstOrDefault(m => m.StId==id);

            MemoryStream ms = new MemoryStream(image.Data);

            FileStreamResult streamResult = new FileStreamResult(ms, image.ContentType);
            return streamResult;
        }

        [HttpGet]
        public IActionResult GetFee(int? id,int? ADID)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }

            var res= _context.Admissiopay.Include(d => d.AdT);
            var res1 = (res.Where(k => k.AdId == ADID && k.AdT.FTId == id));
            List<object> ls = new List<object>();
            
            foreach (var item in res1)
            {
                
                var x = new { adTId=item.AdTId, AdId = item.AdId,FTId= item.AdT.FTId, term=item.AdT.Term, termAmount=item.AdT.TermAmount, termDate=item.AdT.TermDate, adYear=item.AdT.AdYear,TermID = item.AdT.AdTId, Paystatus = item.Paystatus, Pay_mode=item.Pay_mode, Chequeno= item.Chequeno, PayDate= item.PayDate,ChequeDate=item.Chequedt,BankName=item.BankName,BankBranch=item.BankBranch };
                ls.Add(x);
            }
            //var res = (_context.AdTypeFee.ToList<AdTypeFee>().Where(k => k.FTId == id.Value));
            // string[] mes = { "Varsha", "Meghna" };
            JsonResult jsonResult = Json(ls);

            return (jsonResult);
        }
        public IActionResult updatestudent(string name)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (name != null)
            {
                List<EnrollModel> list = Fill_Adlist(name);
                ListModel listModel = new ListModel() { UserSearch = new UserSearch() { St_Name = name } };
                listModel.EnrollModels = list;
                listModel.ctrl1 = 1;
                ViewBag.display = "div2";
                   fillinitialdata();
                return View(listModel);
            }
            else
            {
                ListModel list = new ListModel();
                return View(list);
            }
        }

        [HttpPost]
        public IActionResult updatesubmit(ListModel data, string ctrl)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Student student=null;
                        IFormFile uploadedImage = data.EnrollModel.files;
                        if (data.EnrollModel.StId.HasValue)
                        {
                            if (uploadedImage == null || uploadedImage.ContentType.ToLower().StartsWith("image/"))
                            {

                                MemoryStream ms = new MemoryStream();
                                uploadedImage.OpenReadStream().CopyTo(ms);

                                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                                data.ctrl1 = 1;
                                ViewBag.display = "div2";
                                student = _context.Student.Find(data.EnrollModel.StId);
                                student.Dob = data.EnrollModel.Dob;
                                student.EmailId = data.EnrollModel.EmailId;
                                student.Gender = data.EnrollModel.Gender;
                                student.MotherName = data.EnrollModel.MotherName;
                                student.MotherOccu = data.EnrollModel.MotherOccu;
                                student.MotherPhone = data.EnrollModel.MotherPhone;
                                student.MotherQualification = data.EnrollModel.MotherQualification;
                                student.MotherWhatsapp = data.EnrollModel.MotherWhatsapp;
                                student.Nationality = data.EnrollModel.Nationality;
                                student.Occupation = data.EnrollModel.Occupation;
                                student.ParentMobile = data.EnrollModel.ParentMobile;
                                student.ParentName = data.EnrollModel.ParentName;
                                student.ParentWhatsappNo = data.EnrollModel.ParentWhatsappNo;
                                student.POB = data.EnrollModel.POB;
                                student.Qualification = data.EnrollModel.Qualification;
                                student.StId = data.EnrollModel.StId.Value;
                                student.StName = data.EnrollModel.StName;
                                student.Address = data.EnrollModel.Address;
                                if (uploadedImage != null)
                                {
                                    student.Id = Guid.NewGuid();
                                    student.Name = uploadedImage.FileName;
                                    student.Data = ms.ToArray();
                                    student.Width = image.Width;
                                    student.Height = image.Height;
                                    student.ContentType = uploadedImage.ContentType;
                                }
                                _context.Entry(student).State = EntityState.Modified;
                                _context.SaveChanges();
                            }
                        }
                        transaction.Commit();
                        // TempData["emodel"] = data;
                        ViewBag.display = "div4";
                        ViewBag.student = student;
                        return View("updatestudent", data);
                    }
                      
                                      
                    catch (Exception e1)
                    {
                        transaction.Rollback();

                        fillinitialdata();

                        return View("updatestudent", data);

                    }

                }
            }
            else
            {
                if (data.EnrollModel.StId.HasValue)
                {



                    List<EnrollModel> list = Fill_Adlist(data.EnrollModel.StName);
                    data.AdId = data.EnrollModel.AdId.Value;
                    data.UserSearch = new UserSearch() { St_Name = data.EnrollModel.StName };
                    data.EnrollModels = list;
                    fillinitialdata();
                    data.ctrl1 = 1;
                    var res1 = _context.FeeType.Where(d => d.FTId == data.EnrollModel.FTId);
                    ViewData["FTId"] = new SelectList(res1, "FTId", "Feetype1");
                    ViewBag.display = "div2";
                    ViewBag.studentdiv = "div3";
                    //  return View("Indexdemo", data);
                }
                else
                {
                    data.ctrl1 = 0;
                    ViewBag.display = "div1";
                    var res1 = _context.FeeType.Where(d => d.FTId == data.EnrollModel.FTId);
                    ViewData["FTId"] = new SelectList(res1, "FTId", "Feetype1");
                    fillinitialdata();
                }

                ViewBag.domdata = data.EnrollModel.divdata;
                return View("Indexdemo", data);
            }
        }
        [HttpPost]
        public IActionResult selectadmission(ListModel listModel,string id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }

            Admission1 admission = _context.Admission1.Find(listModel.AdId);
            Student student = _context.Student.Find(admission.StId);
           var res= _context.Admissiopay.Where(d => d.AdId == admission.AdId);
               listModel.EnrollModel = new EnrollModel() { StdId = admission.StdId, FTId = admission.FTId, FeeYear = admission.FeeYear, AdId = admission.AdId, StId = student.StId, POB = student.POB, StName = student.StName, Dob = student.Dob, EmailId = student.EmailId, Gender = student.Gender, MotherName = student.MotherName, MotherOccu = student.MotherOccu, MotherPhone = student.MotherPhone, MotherQualification = student.MotherQualification, MotherWhatsapp = student.MotherWhatsapp, Nationality = student.Nationality, Occupation = student.Occupation, ParentMobile = student.ParentMobile, ParentName = student.ParentName, ParentWhatsappNo = student.ParentWhatsappNo, Qualification = student.Qualification,Address=student.Address};
            listModel.EnrollModel.paystatusid = "";
            foreach (var item in res)
            {
             
               if(item.Paystatus==true)
                {
                    listModel.EnrollModel.paystatusid += item.AdTId + "." + item.PayDate;
                    if(item.Pay_mode=="Cash")
                    {
                        listModel.EnrollModel.paystatusid += ".1";
                    }
                    else if(item.Pay_mode=="Cheque")
                    {
                        listModel.EnrollModel.paystatusid += ".2";
                        listModel.EnrollModel.paystatusid += "." + item.Chequeno;
                        listModel.EnrollModel.paystatusid += "." + item.Chequedt.ToString();
                        listModel.EnrollModel.paystatusid += "." + item.BankName;
                        listModel.EnrollModel.paystatusid += "." + item.BankBranch;

                    }
                    else if(item.Pay_mode=="netbanking")
                    {
                        listModel.EnrollModel.paystatusid += ".3";
                        listModel.EnrollModel.paystatusid += "." + item.Transactionno;
                    }
                    listModel.EnrollModel.paystatusid += ",";
                }
            }


            ViewBag.enrolldata = listModel.EnrollModel;
            ViewBag.display = "div2";
            ViewBag.studentdiv = "div3";
            List<EnrollModel> list = Fill_Adlist(student.StName);

            listModel.UserSearch = new UserSearch() { St_Name = student.StName };
            listModel.EnrollModels = list;

            fillinitialdata();
            if (id == "update")
            {
                return View("UpdateStudent", listModel);
            }
            else
            {
                return View("FeePay", listModel);
            }
        }

        public IActionResult FeePay(string name)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (name != null)
            {
                List<EnrollModel> list = Fill_Adlist(name);
                ListModel listModel = new ListModel() { UserSearch = new UserSearch() { St_Name = name } };
                listModel.EnrollModels = list;
                listModel.ctrl1 = 1;
                ViewBag.display = "div2";

                //   fillinitialdata();
                return View(listModel);
            }
            else
            {
                ListModel list = new ListModel();
                return View(list);
            }
            
        }


        }


    }

