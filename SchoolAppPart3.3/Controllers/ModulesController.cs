using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppPart3._3.Data;
using SchoolAppPart3._3.Models;

namespace SchoolAppPart3._3.Controllers
{

    public class ModulesController : Controller
    {
        public int noSemesterWeek;
        public DateTime startSemesterDate;
         
        private readonly ApplicationDbContext _context;

        public ModulesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: Modules
        public async Task<IActionResult> Index()
        {
              return _context.Modules != null ? 
                          View(await _context.Modules.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Modules'  is null.");
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var modules = await _context.Modules
                .FirstOrDefaultAsync(m => m.Code == id);
            if (modules == null)
            {
                return NotFound();
            }

            return View(modules);
        }

        // GET: Modules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name,NoCredits,Hours")] Modules modules)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modules);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modules);
        }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var modules = await _context.Modules.FindAsync(id);
            if (modules == null)
            {
                return NotFound();
            }
            return View(modules);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Name,NoCredits,Hours")] Modules modules)
        {
            if (id != modules.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modules);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModulesExists(modules.Code))
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
            return View(modules);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var modules = await _context.Modules
                .FirstOrDefaultAsync(m => m.Code == id);
            if (modules == null)
            {
                return NotFound();
            }

            return View(modules);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Modules == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Modules'  is null.");
            }
            var modules = await _context.Modules.FindAsync(id);
            if (modules != null)
            {
                _context.Modules.Remove(modules);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModulesExists(string id)
        {
          return (_context.Modules?.Any(e => e.Code == id)).GetValueOrDefault();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Storage(int NoSemesterWeek,DateTime StartDate)
        {
            noSemesterWeek = NoSemesterWeek;
            startSemesterDate = StartDate;
            TempData["noSemesterWeek"] = NoSemesterWeek;
            TempData["dateWeek"]=StartDate;
            return RedirectToAction("CalculateSelfStudy", "Operations");
        }
       
        public IActionResult ModReminder()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModReminder([Bind("Module,Date")] VMLogin vmLogin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vmLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vmLogin);
        }
      
    }
}
