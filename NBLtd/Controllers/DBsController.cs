using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBLtd.Data;
using NBLtd.Models;

namespace NBLtd.Controllers
{
    public class DBsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DBsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DBs
        public async Task<IActionResult> Index()
        {
              return _context.DB != null ? 
                          View(await _context.DB.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DB'  is null.");
        }

        // GET: DBs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DB == null)
            {
                return NotFound();
            }

            var dB = await _context.DB
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dB == null)
            {
                return NotFound();
            }

            return View(dB);
        }
        [Authorize]
        // GET: DBs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DBs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Location,category,Pice")] DB dB)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dB);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dB);
        }
        [Authorize]

        // GET: DBs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DB == null)
            {
                return NotFound();
            }

            var dB = await _context.DB.FindAsync(id);
            if (dB == null)
            {
                return NotFound();
            }
            return View(dB);
        }
        [Authorize]
        // POST: DBs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Location,category,Pice")] DB dB)
        {
            if (id != dB.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DBExists(dB.ID))
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
            return View(dB);
        }
        [Authorize]
        // GET: DBs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DB == null)
            {
                return NotFound();
            }

            var dB = await _context.DB
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dB == null)
            {
                return NotFound();
            }

            return View(dB);
        }
        [Authorize]
        // POST: DBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DB == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DB'  is null.");
            }
            var dB = await _context.DB.FindAsync(id);
            if (dB != null)
            {
                _context.DB.Remove(dB);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DBExists(int id)
        {
          return (_context.DB?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
