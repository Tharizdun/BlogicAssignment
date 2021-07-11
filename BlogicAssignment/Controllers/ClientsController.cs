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
    public class ClientsController : Controller
    {
        private readonly BlogicAssignmentDbContext _context;

        public ClientsController(BlogicAssignmentDbContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string Search)
        {
            if (!String.IsNullOrEmpty(Search))
            {
                return View(await _context.Clients.Where(c => c.LastName.Contains(Search)).ToListAsync());
            }
            else return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(m => m.Contracts)
                .FirstOrDefaultAsync(m => m.ClientID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientID,FirstName,LastName,BirthNumber,Age,Phone,Email")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientID,FirstName,LastName,BirthNumber,Age,Phone,Email")] Client client)
        {
            if (id != client.ClientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientID))
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
            return View(client);
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientID == id);
        }

        // https://www.codingvila.com/2021/03/export-data-to-csv-file-using-aspnet-mvc.html
        public async Task<IActionResult> ExportToCSV()
        {
            var clients = await _context.Clients.ToListAsync();
            StringBuilder sb = new();
            sb.AppendLine("Client ID;First name;Last name;Birth number;Age;Phone number;Email address");
            foreach(Client client in clients)
            {
                sb.AppendLine($"{client.ClientID};{client.FirstName};{client.LastName};{client.BirthNumber};{client.Age};{client.Phone};{client.Email}");
            }
            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "Clients.csv");
        }
    }
}
