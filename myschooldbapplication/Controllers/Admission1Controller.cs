using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using myschooldbapplication.Models;
using ReflectionIT.Mvc.Paging;

namespace myschooldbapplication.Controllers
{
    public class Admission1Controller : Controller
    {
        private readonly myschooldbContext _context;

        public Admission1Controller(myschooldbContext context)
        {
            _context = context;
        }

        // GET: Admission1
        public async Task<IActionResult> Index(int page=1)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }

            var query = _context.Admission1.AsNoTracking()
                .Include(a => a.FT)
                .Include(a => a.St)
                .Include(a => a.Std)
                .AsQueryable()
                .OrderBy(d => d.AdId);
            var model = await PagingList.CreateAsync(query, 10, page);
            return View( model);
        }

     
        // GET: Admission1/Details/5
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

            var admission1 = await _context.Admission1
                .Include(a => a.FT)
                .Include(a => a.St)
                .Include(a => a.Std)
                .FirstOrDefaultAsync(m => m.AdId == id);
            if (admission1 == null)
            {
                return NotFound();
            }

            return View(admission1);
        }




     
        // GET: Admission1/Create
        public IActionResult Create(string name,int? ID)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (name != null)
            {
                ViewData["FTId"] = new SelectList(_context.FeeType, "FTId", "Feetype1");
                ViewData["StId1"] = name;
                ViewData["StId2"] = ID.Value;
                ViewData["StdId"] = new SelectList(_context.Std, "StdId", "Stname");
                var resyear = (from k in _context.FeeType
                               select k.FeeYear).Distinct();
                ViewData["Feeyear"] = new SelectList(resyear);
            }
            else
            {
               var res= _context.Admission1.Include(a => a.FT).Include(a => a.St).Include(a => a.Std);
                var res1 = from k in res orderby k.Std.Stname
                          select new
                          {
                              Text = k.Std.Stname + "  " + k.AdId+" "+k.St.StName,
                              Value = k.StId
                          };
                ViewData["FTId"]  = new SelectList(_context.FeeType, "FTId", "Feetype1");
                ViewData["StId"]  = new SelectList(res1, "Value", "Text");
                ViewData["StdId"] = new SelectList(_context.Std, "StdId", "Stname");

                var resyear = (from k in _context.FeeType
                           select k.FeeYear).Distinct();
                ViewData["Feeyear"] = new SelectList(resyear);
            }
            return View();
        }

        // POST: Admission1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdId,StId,StdId,FTId,FeeYear,PaidAmount,Totalfees,Pendingamt")] Admission1 admission1)

        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (ModelState.IsValid)
            {
                //if(ViewBag.StId1 != null && admission1.StId==null)
                //{
                //    admission1.StdId=
                //}
                _context.Add(admission1);
                await _context.SaveChangesAsync();

                var routeValues = new RouteValueDictionary {
                { "ID",admission1.AdId}

                
};
            return RedirectToAction("AdmissionPay", "Admission", routeValues);
             //   return RedirectToAction(nameof(Index));
            }

            ViewData["FTId"] = new SelectList(_context.FeeType, "FTId", "Feetype1");
            ViewData["StId"] = new SelectList(_context.Student, "StId", "StName", admission1.StId);
            ViewData["StdId"] = new SelectList(_context.Std, "StdId", "Stname", admission1.StdId);
            return View(admission1);
        }

        // GET: Admission1/Edit/5
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

            var admission1 = await _context.Admission1.FindAsync(id);
            if (admission1 == null)
            {
                return NotFound();
            }
            ViewData["FTId"] = new SelectList(_context.FeeType, "FTId", "Feetype1", admission1.FTId);
            ViewData["StId"] = new SelectList(_context.Student, "StId", "StName", admission1.StId);
            ViewData["StdId"] = new SelectList(_context.Std, "StdId", "Stname", admission1.StdId);
            return View(admission1);
        }

        // POST: Admission1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdId,StId,StdId,FTId,FeeYear,PaidAmount,Totalfees,Pendingamt")] Admission1 admission1)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (id != admission1.AdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var res = _context.Admissiopay.Where(k => k.AdId == admission1.AdId);
                    _context.Admissiopay.RemoveRange(res);
                    
                    _context.Update(admission1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Admission1Exists(admission1.AdId))
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
            ViewData["FTId"] = new SelectList(_context.FeeType, "FTId", "Feetype1", admission1.FTId);
            ViewData["StId"] = new SelectList(_context.Student, "StId", "StName", admission1.StId);
            ViewData["StdId"] = new SelectList(_context.Std, "StdId", "Stname", admission1.StdId);
            return View(admission1);
        }

        // GET: Admission1/Delete/5
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

            var admission1 = await _context.Admission1
                .Include(a => a.FT)
                .Include(a => a.St)
                .Include(a => a.Std)
                .FirstOrDefaultAsync(m => m.AdId == id);
            if (admission1 == null)
            {
                return NotFound();
            }

            return View(admission1);
        }

        // POST: Admission1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            var admission1 = await _context.Admission1.FindAsync(id);
            _context.Admission1.Remove(admission1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Admission1Exists(int id)
        {
           
            return _context.Admission1.Any(e => e.AdId == id);
        }
    }
}
