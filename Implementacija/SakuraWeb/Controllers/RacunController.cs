using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SakuraWeb.Data;
using SakuraWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakuraWeb.Controllers
{
    public class RacunController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public RacunController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Racun
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.korisnici.ToListAsync());
        }

        // GET: Racun/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.korisnici
                .FirstOrDefaultAsync(m => m.Id == id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // GET: Racun/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Racun/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("korisnickoIme,emailAdresa,lozinka,ulogaKorisnika")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                // Sync custom fields to IdentityUser fields
                korisnik.UserName = korisnik.korisnickoIme;
                korisnik.Email = korisnik.emailAdresa;
                korisnik.EmailConfirmed = true; // Employee emails don't need confirmation
                korisnik.jePretplacenNaNewsletter = false; // Employees don't subscribe to newsletter

                // Create user with hashed password using UserManager
                var result = await _userManager.CreateAsync(korisnik, korisnik.lozinka);

                if (result.Succeeded)
                {
                    // Fetch the user back from DB to get the actual hashed password
                    var createdUser = await _userManager.FindByIdAsync(korisnik.Id);
                    if (createdUser != null)
                    {
                        // Copy the hashed password from DB to lozinka field
                        createdUser.lozinka = createdUser.PasswordHash;
                        await _userManager.UpdateAsync(createdUser);
                    }

                    // Assign "Frizer" role (if the role exists)
                    if (!string.IsNullOrEmpty(korisnik.ulogaKorisnika.ToString()))
                    {
                        await _userManager.AddToRoleAsync(korisnik, korisnik.ulogaKorisnika.ToString());
                    }

                    return RedirectToAction(nameof(Index));
                }

                // Add error messages if user creation failed
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(korisnik);
        }

        // GET: Racun/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.korisnici.FindAsync(id);
            if (korisnik == null)
            {
                return NotFound();
            }
            return View(korisnik);
        }

        // POST: Racun/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id, [Bind("id,korisnickoIme,emailAdresa,lozinka,jePretplacenNaNewsletter,ulogaKorisnika")] Korisnik korisnik)
        {
            if (id != korisnik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korisnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!KorisnikExists(korisnik.id))
                    if(!korisnik.Id.IsNullOrEmpty())
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
            return View(korisnik);
        }

        // GET: Racun/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.korisnici
                .FirstOrDefaultAsync(m => m.Id == id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // POST: Racun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var korisnik = await _context.korisnici.FindAsync(id);
            if (korisnik != null)
            {
                _context.korisnici.Remove(korisnik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorisnikExists(string id)
        {
            return _context.korisnici.Any(e => e.Id == id);
        }
    }
}
