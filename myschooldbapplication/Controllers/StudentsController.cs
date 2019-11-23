using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myschooldbapplication.Models;
using ReflectionIT.Mvc.Paging;

namespace myschooldbapplication.Controllers
{
    public class StudentsController : Controller
    {
        private readonly myschooldbContext _context;

        public StudentsController(myschooldbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(int page=1)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            var query= _context.Student.AsNoTracking().OrderBy(d => d.StName);
          var model= await PagingList.CreateAsync(query, 10, page);
            return View(model);
        }

        // GET: Students/Details/5
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

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.StId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StId,StName,Dob,ParentName,ParentMobile")] Student student)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
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

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StId,StName,Dob,ParentName,ParentMobile")] Student student)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            if (id != student.StId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StId))
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
            return View(student);
        }

        // GET: Students/Delete/5
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

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.StId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Session1")))
            {
                return RedirectToAction("UserLogin", "Login");

            }
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StId == id);
        }
    }
}
