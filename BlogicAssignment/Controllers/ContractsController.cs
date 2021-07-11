using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogicAssignment.Data;
using BlogicAssignment.Models;
using System.Text;

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
        public async Task<IActionResult> Index(string Search)
        {
            if(!String.IsNullOrEmpty(Search))
            {
                var blogicAssignmentDbContext = _context.Contracts
                    .Where(c => c.EvidenceNumber.Contains(Search) || c.Institution.Contains(Search))
                    .Include(c => c.Client)
                    .Include(c => c.Supervisor);
                return View(await blogicAssignmentDbContext.ToListAsync());
            }
            else
            {
                var blogicAssignmentDbContext = _context.Contracts.Include(c => c.Client).Include(c => c.Supervisor);
                return View(await blogicAssignmentDbContext.ToListAsync());
            }   
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

            // Find advisors overseeing this contract
            var advisorContract = await _context.AdvisorContracts
                .Include(c => c.Advisor)
                .Where(c => c.ContractID == id)
                .ToListAsync();
            List<Advisor> advisors = new();
            foreach(var ac in advisorContract)
            {
                advisors.Add(ac.Advisor);
            }
            if (advisors.Any())
            {
                ViewData["Advisors"] = advisors;
            }
            else ViewData["Advisors"] = null;

            // Find advisors not overseeing this contract
            List<Advisor> allAdvisors = await _context.Advisors.ToListAsync();
            List<Advisor> result = allAdvisors.Except(advisors).ToList();
            result.Remove(contract.Supervisor);
            if (result.Any())
            {
                ViewData["AvailableAdvisors"] = new SelectList(result, "AdvisorID", "FullName");
            }
            else ViewData["AvailableAdvisors"] = null;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdvisor(int id, int advisorId)
        {
            await _context.AdvisorContracts.AddAsync(new AdvisorContract { ContractID = id, AdvisorID = advisorId });
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Contracts", new { id });
        }

        public async Task<IActionResult> ExportToCSV()
        {
            var contracts = await _context.Contracts.Include(c => c.Client).Include(c => c.Supervisor).ToListAsync();
            StringBuilder sb = new();
            sb.AppendLine("Contract ID;Evidence number;Institution;Supervisor;Client;Start date;Valid date;End date;Additional advisors");
            
            foreach(Contract contract in contracts)
            {
                sb.Append($"{contract.ContractID};{contract.EvidenceNumber};{contract.Institution};{contract.Supervisor.FullName};{contract.Client.FullName};");
                sb.Append($"{contract.ContractEnterDate};{contract.ContractValidSinceDate};{contract.ContractEndDate}");
                // find advisors for this contract
                var advisorContract = await _context.AdvisorContracts
                .Include(c => c.Advisor)
                .Where(c => c.ContractID == contract.ContractID)
                .ToListAsync();

                if(advisorContract.Any())
                {
                    var lastItem = advisorContract.LastOrDefault();
                    sb.Append(';');
                
                    foreach (var ac in advisorContract)
                    {
                        if (ac == lastItem) 
                        {
                            sb.Append($"{ac.Advisor.FullName}");
                        }
                        else
                        {
                            sb.Append($"{ac.Advisor.FullName},");
                        }
                    }
                }
                sb.Append('\n');
            }
            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "Contracts.csv");
        }
    }
}
