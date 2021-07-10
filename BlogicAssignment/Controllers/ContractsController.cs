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
    public class ContractsController : Controller
    {
        private readonly BlogicAssignmentDbContext _context;

        public ContractsController(BlogicAssignmentDbContext context)
        {
            _context = context;
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            var blogicAssignmentDbContext = _context.Contracts.Include(c => c.Client).Include(c => c.Supervisor);
            return View(await blogicAssignmentDbContext.ToListAsync());
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.Supervisor)
                .FirstOrDefaultAsync(m => m.ContractID == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "FullName");
            ViewData["SupervisorID"] = new SelectList(_context.Advisors, "AdvisorID", "FullName");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractID,EvidenceNumber,Institution,SupervisorID,ClientID,ContractEnterDate,ContractValidSinceDate,ContractEndDate")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "BirthNumber", contract.ClientID);
            ViewData["SupervisorID"] = new SelectList(_context.Advisors, "AdvisorID", "BirthNumber", contract.SupervisorID);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "FullName", contract.ClientID);
            ViewData["SupervisorID"] = new SelectList(_context.Advisors, "AdvisorID", "FullName", contract.SupervisorID);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractID,EvidenceNumber,Institution,SupervisorID,ClientID,ContractEnterDate,ContractValidSinceDate,ContractEndDate")] Contract contract)
        {
            if (id != contract.ContractID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.ContractID))
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
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "FullName", contract.ClientID);
            ViewData["SupervisorID"] = new SelectList(_context.Advisors, "AdvisorID", "FullName", contract.SupervisorID);
            return View(contract);
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.ContractID == id);
        }
    }
}
