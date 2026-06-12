using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SakuraWeb.Data;
using SakuraWeb.Models;
using SakuraWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace SakuraWeb.Controllers
{
    public class RacunController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;
        private readonly IEmailSender _emailSender;

        public RacunController(ApplicationDbContext context, UserManager<Korisnik> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
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
        // Reset password action
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ResetPassword(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _userManager.FindByIdAsync(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            // Generate a random strong password
            var randomPassword = GenerateRandomPassword();

            // Reset the user's password
            var token = await _userManager.GeneratePasswordResetTokenAsync(korisnik);
            var result = await _userManager.ResetPasswordAsync(korisnik, token, randomPassword);

            if (result.Succeeded)
            {
                // Sync lozinka with the new password hash
                var updatedUser = await _userManager.FindByIdAsync(korisnik.Id);
                if (updatedUser != null)
                {
                    updatedUser.lozinka = updatedUser.PasswordHash;
                    await _userManager.UpdateAsync(updatedUser);
                }

                // Send email with new password
                var emailSubject = "[Sakura] Reset lozinke";
                var emailBody = $"Administrator je resetovao vašu lozinku i ona sada glasi: {randomPassword}. Molimo vas da se prijavite na stranicu i promijenite je.";
                await _emailSender.SendEmailAsync(korisnik.emailAdresa, emailSubject, emailBody);

                TempData["SuccessMessage"] = $"Lozinka za korisnika {korisnik.korisnickoIme} je uspješno resetovana i poslana na email {korisnik.emailAdresa}.";
                return RedirectToAction(nameof(Index));
            }

            // If reset failed, show error
            return RedirectToAction(nameof(Edit), new { id = id });
        }

        /// <summary>
        /// Generate a random strong password
        /// </summary>
        private string GenerateRandomPassword()
        {
            var options = _userManager.PasswordHasher;
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*";

            var random = new Random();
            var password = new System.Text.StringBuilder();

            // Add at least one character from each category
            password.Append(uppercase[random.Next(uppercase.Length)]);
            password.Append(lowercase[random.Next(lowercase.Length)]);
            password.Append(digits[random.Next(digits.Length)]);
            password.Append(special[random.Next(special.Length)]);

            // Fill the rest with random characters
            const string allChars = uppercase + lowercase + digits + special;
            for (int i = password.Length; i < 12; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Shuffle the password
            var passwordArray = password.ToString().ToCharArray();
            for (int i = passwordArray.Length - 1; i > 0; i--)
            {
                int randomIndex = random.Next(i + 1);
                var temp = passwordArray[i];
                passwordArray[i] = passwordArray[randomIndex];
                passwordArray[randomIndex] = temp;
            }

            return new string(passwordArray);
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
        public async Task<IActionResult> Delete(string id, string razlog)
        {
            var korisnik = await _context.korisnici.FindAsync(id);
            if (korisnik != null)
            {
                var email = korisnik.emailAdresa;
                var username = korisnik.korisnickoIme;

                // Remove the user
                _context.korisnici.Remove(korisnik);
                await _context.SaveChangesAsync();

                // Send deletion notification email
                var emailSubject = "[Sakura] Vaš račun je obrisan";
                var emailBody = $"Administrator je obrisao vaš račun. Razlog: {razlog}";
                await _emailSender.SendEmailAsync(email, emailSubject, emailBody);

                TempData["SuccessMessage"] = $"Račun za korisnika {username} je uspješno obrisan.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool KorisnikExists(string id)
        {
            return _context.korisnici.Any(e => e.Id == id);
        }
    }
}
