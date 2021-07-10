using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogicAssignment.Data;
using BlogicAssignment.Models;

namespace BlogicAssignment.Controllers
{
    public class AdvisorsController : Controller
    {
        private readonly BlogicAssignmentDbContext _context;

        public AdvisorsController(BlogicAssignmentDbContext context)
        {
            _context = context;
        }

        // GET: Advisors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Advisors.ToListAsync());
        }

        // GET: Advisors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advisor = await _context.Advisors
                .FirstOrDefaultAsync(m => m.AdvisorID == id);
            if (advisor == null)
            {
                return NotFound();
            }

            return View(advisor);
        }

        // GET: Advisors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Advisors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdvisorID,FirstName,LastName,BirthNumber,Age,Phone,Email")] Advisor advisor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advisor);
        }

        // GET: Advisors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advisor = await _context.Advisors.FindAsync(id);
            if (advisor == null)
            {
                return NotFound();
            }
            return View(advisor);
        }

        // POST: Advisors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvisorID,FirstName,LastName,BirthNumber,Age,Phone,Email")] Advisor advisor)
        {
            if (id != advisor.AdvisorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advisor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvisorExists(advisor.AdvisorID))
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
            return View(advisor);
        }

        // GET: Advisors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advisor = await _context.Advisors
                .FirstOrDefaultAsync(m => m.AdvisorID == id);
            if (advisor == null)
            {
                return NotFound();
            }

            return View(advisor);
        }

        // POST: Advisors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advisor = await _context.Advisors.FindAsync(id);
            _context.Advisors.Remove(advisor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvisorExists(int id)
        {
            return _context.Advisors.Any(e => e.AdvisorID == id);
        }
    }
}
