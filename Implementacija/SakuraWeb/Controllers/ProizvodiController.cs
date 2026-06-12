using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using Microsoft.EntityFrameworkCore;
using SakuraWeb.Data;
using SakuraWeb.Models;

namespace SakuraWeb.Controllers
{
    public class ProizvodiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProizvodiController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Proizvodi
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.proizvodi.ToListAsync());
        }*/

        public async Task<IActionResult> Index(string? sort, KategorijaProizvoda? kategorija)
        {
            IQueryable<Proizvod> query = _context.proizvodi;

            if (kategorija.HasValue && !kategorija.Equals("Sve"))
            { 
                query = query.Where(p => p.kategorija == kategorija);
            }

            switch (sort)
            {
                case "CijenaUzlazno":
                    query = query.OrderBy(p => p.cijena);
                    break;

                case "CijenaSilazno":
                    query = query.OrderByDescending(p => p.cijena);
                    break;

                case "NazivUzlazno":
                    query = query.OrderBy(p => p.naziv);
                    break;

                case "NazivSilazno":
                    query = query.OrderByDescending(p => p.naziv);
                    break;
            }

            return View(await query.ToListAsync());
        }

        // GET: Proizvodi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var proizvod = await _context.proizvodi
                .Include(p => p.ProizvodSastojci)
                    .ThenInclude(ps => ps.sastojak)
                .FirstOrDefaultAsync(p => p.id == id);
            foreach (var ps in proizvod.ProizvodSastojci)
            {
                Console.WriteLine(ps.sastojak.naziv);
            }

            //var proizvod = await _context.proizvodi
            //    .FirstOrDefaultAsync(m => m.id == id);
            if (proizvod == null)
            {
                return NotFound();
            }

            return View(proizvod);
        }

        // GET: Proizvodi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proizvodi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,naziv,cijena,kategorija,volumen,slikaPutanja,poeniPr")] Proizvod proizvod, IFormFile? slika)
        {
            /*Console.WriteLine("#Model IN");
            foreach (var kvp in ModelState)
            {
                foreach (var error in kvp.Value.Errors)
                {
                    Console.WriteLine($"{kvp.Key}: {error.ErrorMessage}");
                }
            }*/
            if (ModelState.IsValid)
            {
                if (slika != null && slika.Length > 0)
                {
                    string uploadsFolder = Path.Combine(
                        _environment.WebRootPath,
                        "img");

                    Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName =
                        Guid.NewGuid().ToString() +
                        Path.GetExtension(slika.FileName);

                    string filePath =
                        Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await slika.CopyToAsync(stream);
                    }

                    proizvod.slikaPutanja = uniqueFileName;
                }
                _context.Add(proizvod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proizvod);
        }

        // GET: Proizvodi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.proizvodi.FindAsync(id);
            if (proizvod == null)
            {
                return NotFound();
            }
            return View(proizvod);
        }

        // POST: Proizvodi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,naziv,cijena,kategorija,volumen,poeniPr")] Proizvod proizvod, IFormFile? slika)
        {
            if (id != proizvod.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var postojeci = await _context.proizvodi.FindAsync(id);
                    if (postojeci == null)
                    {
                        return NotFound();
                    }

                    postojeci.naziv = proizvod.naziv;
                    postojeci.cijena = proizvod.cijena;
                    postojeci.kategorija = proizvod.kategorija;
                    postojeci.volumen = proizvod.volumen;
                    postojeci.poeniPr = proizvod.poeniPr;

                    if (slika != null && slika.Length > 0)
                    {
                        string uploadsFolder = Path.Combine(_environment.WebRootPath, "img");
                        Directory.CreateDirectory(uploadsFolder);

                        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(slika.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await slika.CopyToAsync(stream);
                        }

                        postojeci.slikaPutanja = uniqueFileName;
                    }
                    // ako nema nove slike, postojeci.slikaPutanja ostaje nepromijenjen

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProizvodExists(proizvod.id))
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
            return View(proizvod);
        }

        // GET: Proizvodi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.proizvodi
                .FirstOrDefaultAsync(m => m.id == id);
            if (proizvod == null)
            {
                return NotFound();
            }

            return View(proizvod);
        }

        // POST: Proizvodi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proizvod = await _context.proizvodi.FindAsync(id);
            if (proizvod != null)
            {
                _context.proizvodi.Remove(proizvod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProizvodExists(int id)
        {
            return _context.proizvodi.Any(e => e.id == id);
        }
    }
}
