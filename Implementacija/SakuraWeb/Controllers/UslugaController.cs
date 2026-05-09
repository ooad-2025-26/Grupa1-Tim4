using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SakuraWeb.Data;
using SakuraWeb.Models;

namespace SakuraWeb.Controllers
{
    public class UslugaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UslugaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Usluga
        public async Task<IActionResult> Index()
        {
            return View(await _context.usluge.ToListAsync());
        }

        // GET: Usluga/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluga = await _context.usluge
                .FirstOrDefaultAsync(m => m.id == id);
            if (usluga == null)
            {
                return NotFound();
            }

            return View(usluga);
        }

        // GET: Usluga/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usluga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,naziv,opis,cijena,kategorija,trajanje")] Usluga usluga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usluga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usluga);
        }

        // GET: Usluga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluga = await _context.usluge.FindAsync(id);
            if (usluga == null)
            {
                return NotFound();
            }
            return View(usluga);
        }

        // POST: Usluga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,naziv,opis,cijena,kategorija,trajanje")] Usluga usluga)
        {
            if (id != usluga.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usluga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UslugaExists(usluga.id))
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
            return View(usluga);
        }

        // GET: Usluga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluga = await _context.usluge
                .FirstOrDefaultAsync(m => m.id == id);
            if (usluga == null)
            {
                return NotFound();
            }

            return View(usluga);
        }

        // POST: Usluga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usluga = await _context.usluge.FindAsync(id);
            if (usluga != null)
            {
                _context.usluge.Remove(usluga);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UslugaExists(int id)
        {
            return _context.usluge.Any(e => e.id == id);
        }
    }
}
