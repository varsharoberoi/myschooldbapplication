using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using myschooldbapplication.Models;
using myschooldbapplication.Models.IRepository;
using myschooldbapplication.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myschooldbapplication.Controllers
{
    public class FeeTermController : Controller
    {
        IFeeType feetype;
        IAdmissionType admissionType;
        public FeeTermController(IFeeType feeType,IAdmissionType admissionType)
        {
            this.feetype = feeType;
            this.admissionType = admissionType;
        }
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            return View(feetype.GetAll());
        }
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id )
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }

            if (id == null)
            {
                return NotFound();
            }

            var fee_Type = feetype.GetById(id.Value);
            FeeTermModel fmodel = new FeeTermModel();
       
             if (id == null)
                {
                    return NotFound();
                }

            fmodel.FeeTypes = fee_Type;

           ICollection<AdTypeFee> res= admissionType.GetRecordById(id);
         
            if(res==null)
            {
                return NotFound();
            }
            else
            {
                fmodel.adtypes =(List<AdTypeFee>)res;
            }
            return View(fmodel); 
          
   
        }
        [HttpPost]
        public IActionResult Edit(FeeTermModel feeTermModel)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");
            }
            try
            {
                var response = JsonConvert.DeserializeObject<List<AdTypeFee>>(feeTermModel.AdTypeFees);

                feetype.Update(feeTermModel.FeeTypes);

                foreach (var item in response)
                {
                    item.FTId = feeTermModel.FeeTypes.FTId;
                    admissionType.Update(item);
                }
            }
            catch (Exception E)
            {

            }
            var routeValues = new RouteValueDictionary {
                { "ID",feeTermModel.FeeTypes.FTId}};
            return RedirectToAction("Index", "FeeTerm", routeValues);


        }
        [HttpPost]
        public IActionResult Create(FeeTermModel feeTermModel)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            try
            {
                var response = JsonConvert.DeserializeObject<List<AdTypeFee>>(feeTermModel.AdTypeFees);

                feetype.Create(feeTermModel.FeeTypes);

                foreach (var item in response)
                {
                    item.FTId = feeTermModel.FeeTypes.FTId;
                    admissionType.Create(item);
                }
            }
            catch(Exception E)
            {
             
            }
            var routeValues = new RouteValueDictionary {
                { "ID",feeTermModel.FeeTypes.FTId}} ;
            return RedirectToAction("Index", "FeeTerm", routeValues);
        }
        [HttpGet]
        public ActionResult Terms(int? id)
        {
            ICollection<AdTypeFee>  list=null;
            if(id.HasValue)
            {
                list = admissionType.GetRecordById(id.Value);
            }
            else
            {
                list = admissionType.GetRecordById(id);
            }
            
            //var list= _context.AdTypeFee.Where(d => d.FTId == id);
            return View(list);
        }
    }
}