﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppPart3._3.Data;
using SchoolAppPart3._3.Models;

namespace SchoolAppPart3._2.Controllers
{
    public class WorksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorksController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: Works
        public async Task<IActionResult> Index()
        {
              return _context.Work != null ? 
                          View(await _context.Work.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Work'  is null.");
        }

        // GET: Works/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Work == null)
            {
                return NotFound();
            }

            var work = await _context.Work
                .FirstOrDefaultAsync(m => m.WorkCode == id);
            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }

        // GET: Works/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Works/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkCode,WorkDate,WorkHours")] Work work)
        {
            if (ModelState.IsValid)
            {
                _context.Add(work);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(work);
        }

        // GET: Works/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Work == null)
            {
                return NotFound();
            }

            var work = await _context.Work.FindAsync(id);
            if (work == null)
            {
                return NotFound();
            }
            return View(work);
        }

        // POST: Works/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("WorkCode,WorkDate,WorkHours")] Work work)
        {
            if (id != work.WorkCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(work);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkExists(work.WorkCode))
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
            return View(work);
        }

        // GET: Works/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Work == null)
            {
                return NotFound();
            }

            var work = await _context.Work
                .FirstOrDefaultAsync(m => m.WorkCode == id);
            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }

        // POST: Works/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Work == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Work'  is null.");
            }
            var work = await _context.Work.FindAsync(id);
            if (work != null)
            {
                _context.Work.Remove(work);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkExists(string id)
        {
          return (_context.Work?.Any(e => e.WorkCode == id)).GetValueOrDefault();
        }
    }
}
