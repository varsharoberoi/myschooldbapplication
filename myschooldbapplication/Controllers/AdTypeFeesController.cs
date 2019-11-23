using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myschooldbapplication.Models;

namespace myschooldbapplication.Controllers
{
    public class student
    {
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
    }
    public class AdTypeFeesController : Controller
    {
        private readonly myschooldbContext _context;

        public AdTypeFeesController(myschooldbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetFee(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            var res=_context.AdTypeFee.Where(k => k.FTId == id.Value);
          // string[] mes = { "Varsha", "Meghna" };
         JsonResult jsonResult =  Json(res.ToList<AdTypeFee>());

            return  (jsonResult) ;
        }
       

        [HttpGet]
        public IActionResult getstudent()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            var res = (from k in _context.Student
                      select new { FirstName = k.StName, LastName = k.ParentName });
            // string[] mes = { "Varsha", "Meghna" };
            List<student> ls = new List<student>();
            foreach (var item in res)
            {
                student student = new student();
                student.FirstName = item.FirstName;
                student.LastName = item.LastName;
                  ls.Add(student);
            }
            JsonResult jsonResult = Json(ls);

            return (jsonResult);
        }
        // GET: AdTypeFees
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            return View(await _context.AdTypeFee.ToListAsync());
        }

        // GET: AdTypeFees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (id == null)
            {
                return NotFound();
            }

            var adTypeFee = await _context.AdTypeFee
                .FirstOrDefaultAsync(m => m.AdTId == id);
            if (adTypeFee == null)
            {
                return NotFound();
            }

            return View(adTypeFee);
        }
        public int FTId { get; set; }
        public string Feetype1 { get; set; }
        public int? FeeYear { get; set; }

        // GET: AdTypeFees/Create
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            var res1 = from k in _context.FeeType
                       
                       select new
                       {
                           Text = k.FeeYear + "  " + k.Feetype1,
                           Value = k.FTId
                       };
            ViewBag.feetype = new SelectList(res1, "Value", "Text");
            return View();
        }

        // POST: AdTypeFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdTId,Term,TermAmount,TermDate,AdYear,FTId")] AdTypeFee adTypeFee)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (ModelState.IsValid)
            {
                _context.Add(adTypeFee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adTypeFee);
        }

        // GET: AdTypeFees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (id == null)
            {
                return NotFound();
            }

            var adTypeFee = await _context.AdTypeFee.FindAsync(id);
            if (adTypeFee == null)
            {
                return NotFound();
            }
            return View(adTypeFee);
        }

        // POST: AdTypeFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdTId,Term,TermAmount,TermDate,AdYear,FTId")] AdTypeFee adTypeFee)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (id != adTypeFee.AdTId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adTypeFee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdTypeFeeExists(adTypeFee.AdTId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(adTypeFee);
        }

        // GET: AdTypeFees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (id == null)
            {
                return NotFound();
            }

            var adTypeFee = await _context.AdTypeFee
                .FirstOrDefaultAsync(m => m.AdTId == id);
            if (adTypeFee == null)
            {
                return NotFound();
            }

            return View(adTypeFee);
        }

        // POST: AdTypeFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            var adTypeFee = await _context.AdTypeFee.FindAsync(id);
            _context.AdTypeFee.Remove(adTypeFee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdTypeFeeExists(int id)
        {
            return _context.AdTypeFee.Any(e => e.AdTId == id);
        }
    }
}
