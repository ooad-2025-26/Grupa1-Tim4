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
    public class AnketaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnketaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Anketa
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ankete.Include(a => a.korisnik);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Anketa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anketa = await _context.ankete
                .Include(a => a.korisnik)
                .FirstOrDefaultAsync(m => m.id == id);
            if (anketa == null)
            {
                return NotFound();
            }

            return View(anketa);
        }

        // GET: Anketa/Create
        public IActionResult Create()
        {
            ViewData["korisnikId"] = new SelectList(_context.korisnici, "id", "id");
            return View();
        }

        // POST: Anketa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,poeni,vrijemePopunjavanja,korisnikId")] Anketa anketa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anketa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["korisnikId"] = new SelectList(_context.korisnici, "id", "id", anketa.korisnikId);
            return View(anketa);
        }

        // GET: Anketa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anketa = await _context.ankete.FindAsync(id);
            if (anketa == null)
            {
                return NotFound();
            }
            ViewData["korisnikId"] = new SelectList(_context.korisnici, "id", "id", anketa.korisnikId);
            return View(anketa);
        }

        // POST: Anketa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,poeni,vrijemePopunjavanja,korisnikId")] Anketa anketa)
        {
            if (id != anketa.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anketa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnketaExists(anketa.id))
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
            ViewData["korisnikId"] = new SelectList(_context.korisnici, "id", "id", anketa.korisnikId);
            return View(anketa);
        }

        // GET: Anketa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anketa = await _context.ankete
                .Include(a => a.korisnik)
                .FirstOrDefaultAsync(m => m.id == id);
            if (anketa == null)
            {
                return NotFound();
            }

            return View(anketa);
        }

        // POST: Anketa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anketa = await _context.ankete.FindAsync(id);
            if (anketa != null)
            {
                _context.ankete.Remove(anketa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnketaExists(int id)
        {
            return _context.ankete.Any(e => e.id == id);
        }
    }
}
