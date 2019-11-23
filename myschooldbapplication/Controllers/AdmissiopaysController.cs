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

namespace myschooldbapplication.Controllers
{
    public class AdmissiopaysController : Controller
    {
        private readonly myschooldbContext _context;

        public AdmissiopaysController(myschooldbContext context)
        {
            _context = context;
        }

        // GET: Admissiopays
        public async Task<IActionResult> Index(int? ID)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (ID != null)
            {
                var myschooldbContext = _context.Admissiopay.Include(a => a.Ad).Include(a => a.AdT).Where(p=>p.AdId==ID);
                return View(await myschooldbContext.ToListAsync());
            }
            else
            {
                var myschooldbContext = _context.Admissiopay.Include(a => a.Ad).Include(a => a.AdT);
                return View(await myschooldbContext.ToListAsync());
            }
        }

        // GET: Admissiopays/Details/5
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

            var admissiopay = await _context.Admissiopay
                .Include(a => a.Ad)
                .Include(a => a.AdT)
                .FirstOrDefaultAsync(m => m.AdId == id);
            if (admissiopay == null)
            {
                return NotFound();
            }

            return View(admissiopay);
        }

        // GET: Admissiopays/Create
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            ViewData["AdId"] = new SelectList(_context.Admission1, "AdId", "AdId");
            ViewData["AdTId"] = new SelectList(_context.AdTypeFee, "AdTId", "AdTId");
            return View();
        }

        // POST: Admissiopays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdId,AdTId,PayDate,Paystatus")] Admissiopay admissiopay)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (ModelState.IsValid)
            {
                _context.Add(admissiopay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdId"] = new SelectList(_context.Admission1, "AdId", "AdId", admissiopay.AdId);
            ViewData["AdTId"] = new SelectList(_context.AdTypeFee, "AdTId", "AdTId", admissiopay.AdTId);
            return View(admissiopay);
        }

        // GET: Admissiopays/Edit/5
        public async Task<IActionResult> Edit(int? id,int? id1)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (id == null)
            {
                return NotFound();
            }

            var admissiopay = await _context.Admissiopay.FindAsync(new object[] { id,id1});
            if (admissiopay == null)
            {
                return NotFound();
            }
            ViewData["AdId"] = new SelectList(_context.Admission1, "AdId", "AdId", admissiopay.AdId);
            ViewData["AdTId"] = new SelectList(_context.AdTypeFee, "AdTId", "AdTId", admissiopay.AdTId);
            return View(admissiopay);
        }

        // POST: Admissiopays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdId,AdTId,PayDate,Paystatus")] Admissiopay admissiopay)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (id != admissiopay.AdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admissiopay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissiopayExists(admissiopay.AdId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
           
                return RedirectToAction("Index","Admission1");
            }
            ViewData["AdId"] = new SelectList(_context.Admission1, "AdId", "AdId", admissiopay.AdId);
            ViewData["AdTId"] = new SelectList(_context.AdTypeFee, "AdTId", "AdTId", admissiopay.AdTId);
            return View(admissiopay);
        }

        // GET: Admissiopays/Delete/5
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

            var admissiopay = await _context.Admissiopay
                .Include(a => a.Ad)
                .Include(a => a.AdT)
                .FirstOrDefaultAsync(m => m.AdId == id);
            if (admissiopay == null)
            {
                return NotFound();
            }

            return View(admissiopay);
        }

        // POST: Admissiopays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            var admissiopay = await _context.Admissiopay.FindAsync(id);
            _context.Admissiopay.Remove(admissiopay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmissiopayExists(int id)
        {
            return _context.Admissiopay.Any(e => e.AdId == id);
        }
    }
}
