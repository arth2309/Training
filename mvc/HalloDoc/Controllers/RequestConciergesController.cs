using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HalloDoc.DataContext;
using HalloDoc.DataModels;

namespace HalloDoc.Controllers
{
    public class RequestConciergesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestConciergesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RequestConcierges
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RequestConcierges.Include(r => r.Concierge).Include(r => r.Request);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RequestConcierges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RequestConcierges == null)
            {
                return NotFound();
            }

            var requestConcierge = await _context.RequestConcierges
                .Include(r => r.Concierge)
                .Include(r => r.Request)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestConcierge == null)
            {
                return NotFound();
            }

            return View(requestConcierge);
        }

        // GET: RequestConcierges/Create
        public IActionResult Create()
        {
            ViewData["ConciergeId"] = new SelectList(_context.Concierges, "ConciergeId", "ConciergeId");
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId");
            return View();
        }

        // POST: RequestConcierges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestId,ConciergeId,Ip")] RequestConcierge requestConcierge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requestConcierge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConciergeId"] = new SelectList(_context.Concierges, "ConciergeId", "ConciergeId", requestConcierge.ConciergeId);
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId", requestConcierge.RequestId);
            return View(requestConcierge);
        }

        // GET: RequestConcierges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RequestConcierges == null)
            {
                return NotFound();
            }

            var requestConcierge = await _context.RequestConcierges.FindAsync(id);
            if (requestConcierge == null)
            {
                return NotFound();
            }
            ViewData["ConciergeId"] = new SelectList(_context.Concierges, "ConciergeId", "ConciergeId", requestConcierge.ConciergeId);
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId", requestConcierge.RequestId);
            return View(requestConcierge);
        }

        // POST: RequestConcierges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestId,ConciergeId,Ip")] RequestConcierge requestConcierge)
        {
            if (id != requestConcierge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestConcierge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestConciergeExists(requestConcierge.Id))
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
            ViewData["ConciergeId"] = new SelectList(_context.Concierges, "ConciergeId", "ConciergeId", requestConcierge.ConciergeId);
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId", requestConcierge.RequestId);
            return View(requestConcierge);
        }

        // GET: RequestConcierges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RequestConcierges == null)
            {
                return NotFound();
            }

            var requestConcierge = await _context.RequestConcierges
                .Include(r => r.Concierge)
                .Include(r => r.Request)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestConcierge == null)
            {
                return NotFound();
            }

            return View(requestConcierge);
        }

        // POST: RequestConcierges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RequestConcierges == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RequestConcierges'  is null.");
            }
            var requestConcierge = await _context.RequestConcierges.FindAsync(id);
            if (requestConcierge != null)
            {
                _context.RequestConcierges.Remove(requestConcierge);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestConciergeExists(int id)
        {
          return (_context.RequestConcierges?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
