using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using myschooldbapplication.Models;
using myschooldbapplication.Models.IRepository;
using myschooldbapplication.Models.ViewModel;
using myschooldbapplication.Services;

namespace myschooldbapplication.Controllers
{
    public class CollectionController : Controller
    {

         IFeeType _FeeType;
        IAdmissionPay _AdmissionPay;
        private readonly IEmailService _emailService;

        public CollectionController(IFeeType feeType,IAdmissionPay admissionPay, IEmailService  service)
        {
            _FeeType = feeType;
            _AdmissionPay = admissionPay;
            _emailService = service;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            var resyear = (from k in _FeeType.GetAll()
                           select k.FeeYear).Distinct();
            TempData["Feeyear"] = new SelectList(resyear);
            ViewData["Feetype"] = new SelectList(_FeeType.GetAll().ToList(), "FTId", "Feetype1");
            return View();
        }
        [HttpPost]
        public IActionResult Index(collectionsearchmodel collectionsearchmodel)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            var resyear = (from k in _FeeType.GetAll()
                           select k.FeeYear).Distinct();
            TempData["Feeyear"] = new SelectList(resyear);
            ViewData["Feetype"] = new SelectList(_FeeType.GetAll().ToList(), "FTId", "Feetype1",collectionsearchmodel.Type);
            var res = _AdmissionPay.GetMyviews(collectionsearchmodel.Type, collectionsearchmodel.TermID);
            int sum = 0;
            int totalcollectionexpected = res.Sum(d => d.TermWisePending);
            //int paidamt= res.Sum(d => d.PaidStatus.HasValue ? d.TermAmount:0);
            foreach (var item in res)
            {
                if(item.PaidStatus==null)
                {
                    sum += item.TermWisePending;
                }
            }
            ViewBag.sum = sum;
            //// ViewBag.paidamt = paidamt;
            //ViewBag.total = totalcollectionexpected;
            return View(res);
        }

        public async Task<IActionResult> MailSend(string Name,string Pending,string Term,string OverAllPending,string TotalFees,string Email)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            string subject = "Gentle Reminder to Pay due";
            string message ="Greeting from Blossom Buds !!! <br> Your dues as follows <br> Pending Installment <br> " + Term + "Amount Pending Till date " + Pending + "<br>Student Name  " + Name+" <br>Over All Pending "+OverAllPending+" <br> Total Fees "+TotalFees;
            await _emailService.SendEmail(Email, subject, message);
            return RedirectToAction("Index");
        }
     
    }
}