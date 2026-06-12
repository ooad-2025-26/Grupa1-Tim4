using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SakuraWeb.Data;
using SakuraWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SakuraWeb.Controllers
{
    public class RezervacijaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezervacijaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rezervacija
        [Authorize(Roles = "Administrator, Klijent, Zaposlenik")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.rezervacije.Include(r => r.korisnik).Include(r => r.usluga);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rezervacija/Details/5
        [Authorize(Roles = "Administrator, Klijent, Zaposlenik")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.rezervacije
                .Include(r => r.korisnik)
                .Include(r => r.usluga)
                .FirstOrDefaultAsync(m => m.id == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // GET: Rezervacija/Create
        [Authorize(Roles = "Klijent")]
        public async Task<IActionResult> Create()
        {
            var usluge = await _context.usluge
                .OrderBy(u => u.kategorija)
                .ThenBy(u => u.naziv)
                .ToListAsync();

            ViewBag.Usluge = usluge;
            return View();
        }

        // POST: Rezervacija/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Klijent")]
        public async Task<IActionResult> Create(DateTime datumRezervacije, TimeSpan pocetnoVrijeme, int uslugaId)
        {
            var usluga = await _context.usluge.FindAsync(uslugaId);
            if (usluga == null)
            {
                ModelState.AddModelError("", "Odabrana usluga ne postoji.");
                return await VratiCreateView();
            }

            int brojTermina = (int)Math.Ceiling(usluga.trajanje / 30.0);

            // Provjeri da svi potrebni termini nisu zauzeti
            var potrebniTermini = new List<TimeSpan>();
            for (int i = 0; i < brojTermina; i++)
            {
                potrebniTermini.Add(pocetnoVrijeme.Add(TimeSpan.FromMinutes(30 * i)));
            }

            // Posljednji termin ne smije preci radno vrijeme (16:30 zadnji slot)
            var krajRadnogVremena = new TimeSpan(16, 30, 0);
            if (potrebniTermini.Last() > krajRadnogVremena)
            {
                ModelState.AddModelError("", "Odabrana usluga ne staje u radno vrijeme za odabrani termin.");
                return await VratiCreateView();
            }

            var pocetakDana = datumRezervacije.Date;
            var krajDana = pocetakDana.AddDays(1);

            var zauzetiTermini = await _context.rezervacije
                .Where(r => !r.otkazana
                         && r.datumRezervacije >= pocetakDana
                         && r.datumRezervacije < krajDana)
                .Include(r => r.usluga)
                .ToListAsync();

            var zauzetaVremena = new HashSet<TimeSpan>();
            foreach (var rez in zauzetiTermini)
            {
                int brTermina = (int)Math.Ceiling(rez.usluga.trajanje / 30.0);
                for (int i = 0; i < brTermina; i++)
                {
                    zauzetaVremena.Add(rez.vrijemeTermina.TimeOfDay.Add(TimeSpan.FromMinutes(30 * i)));
                }
            }

            if (potrebniTermini.Any(t => zauzetaVremena.Contains(t)))
            {
                ModelState.AddModelError("", "Odabrani termin je zauzet. Molimo odaberite drugi termin.");
                return await VratiCreateView();
            }

            var korisnikId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rezervacija = new Rezervacija
            {
                datumRezervacije = datumRezervacije.Date,
                vrijemeTermina = datumRezervacije.Date.Add(pocetnoVrijeme),
                otkazana = false,
                ocjena = 0,
                korisnikId = korisnikId,
                uslugaId = uslugaId
            };

            _context.Entry(rezervacija).Property(r => r.id).IsTemporary = true;

            _context.Add(rezervacija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> VratiCreateView()
    
        {
            var usluge = await _context.usluge
                .OrderBy(u => u.kategorija)
                .ThenBy(u => u.naziv)
                .ToListAsync();

            ViewBag.Usluge = usluge;
            return View();
        }

        // GET: Rezervacija/Edit/5
        [Authorize(Roles = "Administrator, Klijent")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.rezervacije.FindAsync(id);
            if (rezervacija == null)
            {
                return NotFound();
            }
            ViewData["korisnikId"] = new SelectList(_context.korisnici, "id", "id", rezervacija.korisnikId);
            ViewData["uslugaId"] = new SelectList(_context.usluge, "id", "id", rezervacija.uslugaId);
            return View(rezervacija);
        }

        // POST: Rezervacija/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,datumRezervacije,vrijemeTermina,otkazana,ocjena,korisnikId,uslugaId")] Rezervacija rezervacija)
        {
            if (id != rezervacija.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervacijaExists(rezervacija.id))
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
            ViewData["korisnikId"] = new SelectList(_context.korisnici, "id", "id", rezervacija.korisnikId);
            ViewData["uslugaId"] = new SelectList(_context.usluge, "id", "id", rezervacija.uslugaId);
            return View(rezervacija);
        }

        // GET: Rezervacija/Delete/5
        [Authorize(Roles = "Administrator, Klijent")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.rezervacije
                .Include(r => r.korisnik)
                .Include(r => r.usluga)
                .FirstOrDefaultAsync(m => m.id == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // POST: Rezervacija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervacija = await _context.rezervacije.FindAsync(id);
            if (rezervacija != null)
            {
                _context.rezervacije.Remove(rezervacija);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Rezervacija/ZauzetiTermini?datum=2026-06-15
        [HttpGet]
        public async Task<IActionResult> ZauzetiTermini(DateTime datum)
        {
            var pocetakDana = datum.Date;
            var krajDana = pocetakDana.AddDays(1);

            var rezervacije = await _context.rezervacije
                .Where(r => !r.otkazana
                         && r.datumRezervacije >= pocetakDana
                         && r.datumRezervacije < krajDana)
                .Include(r => r.usluga)
                .ToListAsync();

            var zauzeto = new HashSet<string>();
            foreach (var rez in rezervacije)
            {
                int brTermina = (int)Math.Ceiling(rez.usluga.trajanje / 30.0);
                for (int i = 0; i < brTermina; i++)
                {
                    var vrijeme = rez.vrijemeTermina.TimeOfDay.Add(TimeSpan.FromMinutes(30 * i));
                    zauzeto.Add(vrijeme.ToString(@"hh\:mm"));
                }
            }

            return Json(zauzeto);
        }

        private bool RezervacijaExists(int id)
        {
            return _context.rezervacije.Any(e => e.id == id);
        }
    }
}
