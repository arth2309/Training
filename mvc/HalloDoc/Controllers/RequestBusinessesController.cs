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
    public class RequestBusinessesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestBusinessesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RequestBusinesses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RequestBusinesses.Include(r => r.Business).Include(r => r.Request);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RequestBusinesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RequestBusinesses == null)
            {
                return NotFound();
            }

            var requestBusiness = await _context.RequestBusinesses
                .Include(r => r.Business)
                .Include(r => r.Request)
                .FirstOrDefaultAsync(m => m.RequestBusinessId == id);
            if (requestBusiness == null)
            {
                return NotFound();
            }

            return View(requestBusiness);
        }

        // GET: RequestBusinesses/Create
        public IActionResult Create()
        {
            ViewData["BusinessId"] = new SelectList(_context.Businesses, "BusinessId", "BusinessId");
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId");
            return View();
        }

        // POST: RequestBusinesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestBusinessId,RequestId,BusinessId,Ip")] RequestBusiness requestBusiness)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requestBusiness);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessId"] = new SelectList(_context.Businesses, "BusinessId", "BusinessId", requestBusiness.BusinessId);
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId", requestBusiness.RequestId);
            return View(requestBusiness);
        }

        // GET: RequestBusinesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RequestBusinesses == null)
            {
                return NotFound();
            }

            var requestBusiness = await _context.RequestBusinesses.FindAsync(id);
            if (requestBusiness == null)
            {
                return NotFound();
            }
            ViewData["BusinessId"] = new SelectList(_context.Businesses, "BusinessId", "BusinessId", requestBusiness.BusinessId);
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId", requestBusiness.RequestId);
            return View(requestBusiness);
        }

        // POST: RequestBusinesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestBusinessId,RequestId,BusinessId,Ip")] RequestBusiness requestBusiness)
        {
            if (id != requestBusiness.RequestBusinessId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestBusiness);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestBusinessExists(requestBusiness.RequestBusinessId))
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
            ViewData["BusinessId"] = new SelectList(_context.Businesses, "BusinessId", "BusinessId", requestBusiness.BusinessId);
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId", requestBusiness.RequestId);
            return View(requestBusiness);
        }

        // GET: RequestBusinesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RequestBusinesses == null)
            {
                return NotFound();
            }

            var requestBusiness = await _context.RequestBusinesses
                .Include(r => r.Business)
                .Include(r => r.Request)
                .FirstOrDefaultAsync(m => m.RequestBusinessId == id);
            if (requestBusiness == null)
            {
                return NotFound();
            }

            return View(requestBusiness);
        }

        // POST: RequestBusinesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RequestBusinesses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RequestBusinesses'  is null.");
            }
            var requestBusiness = await _context.RequestBusinesses.FindAsync(id);
            if (requestBusiness != null)
            {
                _context.RequestBusinesses.Remove(requestBusiness);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestBusinessExists(int id)
        {
          return (_context.RequestBusinesses?.Any(e => e.RequestBusinessId == id)).GetValueOrDefault();
        }
    }
}
