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
    public class RequestWiseFilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestWiseFilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RequestWiseFiles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RequestWiseFiles.Include(r => r.Admin).Include(r => r.Physician).Include(r => r.Request);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RequestWiseFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RequestWiseFiles == null)
            {
                return NotFound();
            }

            var requestWiseFile = await _context.RequestWiseFiles
                .Include(r => r.Admin)
                .Include(r => r.Physician)
                .Include(r => r.Request)
                .FirstOrDefaultAsync(m => m.RequestWiseFileId == id);
            if (requestWiseFile == null)
            {
                return NotFound();
            }

            return View(requestWiseFile);
        }

        // GET: RequestWiseFiles/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Admins, "AdminId", "AdminId");
            ViewData["PhysicianId"] = new SelectList(_context.Physicians, "PhysicianId", "PhysicianId");
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId");
            return View();
        }

        // POST: RequestWiseFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestWiseFileId,RequestId,FileName,CreatedDate,PhysicianId,AdminId,DocType,IsFrontSide,IsCompensation,Ip,IsFinalize,IsDeleted,IsPatientRecords")] RequestWiseFile requestWiseFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requestWiseFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "AdminId", "AdminId", requestWiseFile.AdminId);
            ViewData["PhysicianId"] = new SelectList(_context.Physicians, "PhysicianId", "PhysicianId", requestWiseFile.PhysicianId);
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId", requestWiseFile.RequestId);
            return View(requestWiseFile);
        }

        // GET: RequestWiseFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RequestWiseFiles == null)
            {
                return NotFound();
            }

            var requestWiseFile = await _context.RequestWiseFiles.FindAsync(id);
            if (requestWiseFile == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "AdminId", "AdminId", requestWiseFile.AdminId);
            ViewData["PhysicianId"] = new SelectList(_context.Physicians, "PhysicianId", "PhysicianId", requestWiseFile.PhysicianId);
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId", requestWiseFile.RequestId);
            return View(requestWiseFile);
        }

        // POST: RequestWiseFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestWiseFileId,RequestId,FileName,CreatedDate,PhysicianId,AdminId,DocType,IsFrontSide,IsCompensation,Ip,IsFinalize,IsDeleted,IsPatientRecords")] RequestWiseFile requestWiseFile)
        {
            if (id != requestWiseFile.RequestWiseFileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestWiseFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestWiseFileExists(requestWiseFile.RequestWiseFileId))
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
            ViewData["AdminId"] = new SelectList(_context.Admins, "AdminId", "AdminId", requestWiseFile.AdminId);
            ViewData["PhysicianId"] = new SelectList(_context.Physicians, "PhysicianId", "PhysicianId", requestWiseFile.PhysicianId);
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId", requestWiseFile.RequestId);
            return View(requestWiseFile);
        }

        // GET: RequestWiseFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RequestWiseFiles == null)
            {
                return NotFound();
            }

            var requestWiseFile = await _context.RequestWiseFiles
                .Include(r => r.Admin)
                .Include(r => r.Physician)
                .Include(r => r.Request)
                .FirstOrDefaultAsync(m => m.RequestWiseFileId == id);
            if (requestWiseFile == null)
            {
                return NotFound();
            }

            return View(requestWiseFile);
        }

        // POST: RequestWiseFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RequestWiseFiles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RequestWiseFiles'  is null.");
            }
            var requestWiseFile = await _context.RequestWiseFiles.FindAsync(id);
            if (requestWiseFile != null)
            {
                _context.RequestWiseFiles.Remove(requestWiseFile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestWiseFileExists(int id)
        {
          return (_context.RequestWiseFiles?.Any(e => e.RequestWiseFileId == id)).GetValueOrDefault();
        }
    }
}
