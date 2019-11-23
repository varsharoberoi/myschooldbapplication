using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myschooldbapplication.Models;

namespace myschooldbapplication.Controllers
{
    public class StdsController : Controller
    {
        private readonly myschooldbContext _context;

        public StdsController(myschooldbContext context)
        {
            _context = context;
        }

        // GET: Stds
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            return View(await _context.Std.ToListAsync());
        }

        // GET: Stds/Details/5
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

            var std = await _context.Std
                .FirstOrDefaultAsync(m => m.StdId == id);
            if (std == null)
            {
                return NotFound();
            }

            return View(std);
        }

        // GET: Stds/Create
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            return View();
        }

        // POST: Stds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StdId,Stname")] Std std)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (ModelState.IsValid)
            {
                _context.Add(std);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(std);
        }

        // GET: Stds/Edit/5
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

            var std = await _context.Std.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
            return View(std);
        }

        // POST: Stds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StdId,Stname")] Std std)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (id != std.StdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(std);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StdExists(std.StdId))
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
            return View(std);
        }

        // GET: Stds/Delete/5
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

            var std = await _context.Std
                .FirstOrDefaultAsync(m => m.StdId == id);
            if (std == null)
            {
                return NotFound();
            }

            return View(std);
        }

        // POST: Stds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            var std = await _context.Std.FindAsync(id);
            _context.Std.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StdExists(int id)
        {
            return _context.Std.Any(e => e.StdId == id);
        }
    }
}
