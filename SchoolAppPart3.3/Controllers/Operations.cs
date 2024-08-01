using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAppPart3._3 .Data;
using SchoolAppPart3._3.Models;
using System.Reflection;

namespace SchoolAppPart3._3.Controllers
{
    public class Operations : Controller
    {
        ModulesController mod;
       
        private readonly ApplicationDbContext _context;
        public Operations(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CalculateSelfStudy()
        {
            return View();
        }
       
        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CalculateSelfStudy( [Bind("Code,Name")] Modules modules,int noSemester)
        {
            TempData.Keep("noSemesterWeek");

            if (ModelState.IsValid)
            {
                var module = _context.Modules.Find(modules.Code);
                var work = _context.Work.Find(modules.Code);
                TempData["selfStuHours"] = (module.NoCredits * 10 / Convert.ToInt32(noSemester) - module.Hours);
                
                return RedirectToAction(nameof(CalculateSelfStudy));
                
            }
            return View(modules);
        }
        public IActionResult CalculateRemainingHours()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CalculateRemainingHours([Bind("Code,Name")] Modules modules, int noSemester, Work work)
        {
    

          
                var module = _context.Modules.Find(modules.Code);
                var works = _context.Work.Find(modules.Code);
                TempData["RemainingHours"] = (module.NoCredits * 10 / Convert.ToInt32(noSemester) - module.Hours)-works.WorkHours;

                return RedirectToAction(nameof(CalculateRemainingHours));

            
        }
    }
}