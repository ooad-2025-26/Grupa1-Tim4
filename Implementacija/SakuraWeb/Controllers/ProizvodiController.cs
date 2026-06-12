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
        public async Task<IActionResult> Index()
        {
            return View(await _context.proizvodi.ToListAsync());
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

            if (proizvod == null)
            {
                return NotFound();
            }

            ViewData["ProizvodBenefiti"] = await _context.proizvodiBenefiti
                .Where(pb => pb.proizvodId == id)
                .Include(pb => pb.benefit)
                .Select(pb => pb.benefit)
                .ToListAsync();

            return View(proizvod);
        }

        // GET: Proizvodi/Create
        public IActionResult Create()
        {
            ViewData["Benefiti"] = _context.benefiti.ToList();
            ViewData["Sastojci"] = _context.sastojci.ToList();
            return View();
        }

        // POST: Proizvodi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,naziv,cijena,kategorija,volumen,slikaPutanja,poeniPr")] Proizvod proizvod, IFormFile? slika, int[]? benefitIds, int[]? sastojakIds)
        {
            if (ModelState.IsValid)
            {
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

                    proizvod.slikaPutanja = uniqueFileName;
                }
                _context.Add(proizvod);
                await _context.SaveChangesAsync();

                if (benefitIds != null)
                {
                    foreach (var bId in benefitIds)
                    {
                        _context.proizvodiBenefiti.Add(new ProizvodBenefit { proizvodId = proizvod.id, benefitId = bId });
                    }
                }
                if (sastojakIds != null)
                {
                    foreach (var sId in sastojakIds)
                    {
                        _context.proizvodiSastojci.Add(new ProizvodSastojak { proizvodId = proizvod.id, sastojakId = sId });
                    }
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["Benefiti"] = _context.benefiti.ToList();
            ViewData["Sastojci"] = _context.sastojci.ToList();
            return View(proizvod);
        }

        // GET: Proizvodi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var proizvod = await _context.proizvodi
                .Include(p => p.ProizvodSastojci)
                .FirstOrDefaultAsync(p => p.id == id);
        
            if (proizvod == null)
            {
                return NotFound();
            }
        
            ViewData["Benefiti"] = _context.benefiti.ToList();
            ViewData["Sastojci"] = _context.sastojci.ToList();
            ViewData["SelectedBenefitIds"] = await _context.proizvodiBenefiti
                .Where(pb => pb.proizvodId == id).Select(pb => pb.benefitId).ToListAsync();
            ViewData["SelectedSastojakIds"] = await _context.proizvodiSastojci
                .Where(ps => ps.proizvodId == id).Select(ps => ps.sastojakId).ToListAsync();
        
            return View(proizvod);
        }
        
        // POST: Proizvodi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,naziv,cijena,kategorija,volumen,poeniPr")] Proizvod proizvod, IFormFile? slika, int[]? benefitIds, int[]? sastojakIds)
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
        
                    // Update Benefiti veze
                    var postojeceBenefiti = _context.proizvodiBenefiti.Where(pb => pb.proizvodId == id);
                    _context.proizvodiBenefiti.RemoveRange(postojeceBenefiti);
                    await _context.SaveChangesAsync(); // flush removal, detach tracked entities
                    
                    if (benefitIds != null)
                    {
                        foreach (var bId in benefitIds)
                        {
                            _context.proizvodiBenefiti.Add(new ProizvodBenefit { proizvodId = id, benefitId = bId });
                        }
                    }
                    await _context.SaveChangesAsync();
                    
                    // Update Sastojci veze
                    var postojeciSastojci = _context.proizvodiSastojci.Where(ps => ps.proizvodId == id);
                    _context.proizvodiSastojci.RemoveRange(postojeciSastojci);
                    await _context.SaveChangesAsync(); // flush removal, detach tracked entities
                    
                    if (sastojakIds != null)
                    {
                        foreach (var sId in sastojakIds)
                        {
                            _context.proizvodiSastojci.Add(new ProizvodSastojak { proizvodId = id, sastojakId = sId });
                        }
                    }
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
        
            ViewData["Benefiti"] = _context.benefiti.ToList();
            ViewData["Sastojci"] = _context.sastojci.ToList();
            ViewData["SelectedBenefitIds"] = benefitIds?.ToList() ?? new List<int>();
            ViewData["SelectedSastojakIds"] = sastojakIds?.ToList() ?? new List<int>();
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
