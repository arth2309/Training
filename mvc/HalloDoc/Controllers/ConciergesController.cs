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
    public class ConciergesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConciergesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Concierges
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Concierges.Include(c => c.Region);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Concierges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Concierges == null)
            {
                return NotFound();
            }

            var concierge = await _context.Concierges
                .Include(c => c.Region)
                .FirstOrDefaultAsync(m => m.ConciergeId == id);
            if (concierge == null)
            {
                return NotFound();
            }

            return View(concierge);
        }

        // GET: Concierges/Create
        public IActionResult Create()
        {
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "RegionId");
            return View();
        }

        // POST: Concierges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConciergeId,ConciergeName,Address,Street,City,State,ZipCode,CreatedDate,RegionId,RoleId")] Concierge concierge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concierge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "RegionId", concierge.RegionId);
            return View(concierge);
        }

        // GET: Concierges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Concierges == null)
            {
                return NotFound();
            }

            var concierge = await _context.Concierges.FindAsync(id);
            if (concierge == null)
            {
                return NotFound();
            }
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "RegionId", concierge.RegionId);
            return View(concierge);
        }

        // POST: Concierges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConciergeId,ConciergeName,Address,Street,City,State,ZipCode,CreatedDate,RegionId,RoleId")] Concierge concierge)
        {
            if (id != concierge.ConciergeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concierge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConciergeExists(concierge.ConciergeId))
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
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "RegionId", concierge.RegionId);
            return View(concierge);
        }

        // GET: Concierges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Concierges == null)
            {
                return NotFound();
            }

            var concierge = await _context.Concierges
                .Include(c => c.Region)
                .FirstOrDefaultAsync(m => m.ConciergeId == id);
            if (concierge == null)
            {
                return NotFound();
            }

            return View(concierge);
        }

        // POST: Concierges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Concierges == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Concierges'  is null.");
            }
            var concierge = await _context.Concierges.FindAsync(id);
            if (concierge != null)
            {
                _context.Concierges.Remove(concierge);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConciergeExists(int id)
        {
          return (_context.Concierges?.Any(e => e.ConciergeId == id)).GetValueOrDefault();
        }
    }
}
