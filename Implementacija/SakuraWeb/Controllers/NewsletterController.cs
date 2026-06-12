using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using SakuraWeb.Data;
using SakuraWeb.Models;
using SakuraWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace SakuraWeb.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public NewsletterController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: Newsletter
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.poruke.ToListAsync());
        }

        // GET: Newsletter/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletter = await _context.newsletteri
                .FirstOrDefaultAsync(m => m.id == id);
            if (newsletter == null)
            {
                return NotFound();
            }

            return View(newsletter);
        }

        // GET: Newsletter/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Newsletter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("naziv,tekst")] Poruka poruka)
        {
            poruka.newsletterId = 1;
            poruka.newsletter = await _context.newsletteri.FindAsync(1);
            if (poruka.newsletter == null)
            {
                Console.WriteLine("Postavke za zadani newsletter nisu pronađene");
            } else
            {
                _context.Add(poruka);
                await _context.SaveChangesAsync();

                var newsletterCredentials = poruka.newsletter;//await _context.newsletteri.FindAsync(1);
                string? senderEmail = null;
                string? senderPassword = null;

                if (newsletterCredentials != null &&
                    !string.IsNullOrEmpty(newsletterCredentials.emailAdresa) &&
                    !string.IsNullOrEmpty(newsletterCredentials.lozinkaZaEmail))
                {
                    senderEmail = newsletterCredentials.emailAdresa;
                    senderPassword = newsletterCredentials.lozinkaZaEmail;
                }

                var subscribers = await _context.korisnici
                    .Where(k => k.jePretplacenNaNewsletter)
                    .Select(k => k.emailAdresa)
                    .Where(email => !string.IsNullOrEmpty(email))
                    .ToListAsync();

                Console.WriteLine($"Sender Email: {senderEmail}; Sender Password: {senderPassword}");

                foreach (var email in subscribers)
                {
                    if (_emailSender is EmailSender concreteSender)
                    {
                        await concreteSender.SendEmailAsync(
                            email,
                            poruka.naziv,
                            poruka.tekst,
                            senderEmail,
                            senderPassword);
                    }
                    else
                    {
                        await _emailSender.SendEmailAsync(email, poruka.naziv, poruka.tekst);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(poruka);
        }

        // GET: Newsletter/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletter = await _context.newsletteri.FindAsync(id);
            if (newsletter == null)
            {
                return NotFound();
            }
            return View(newsletter);
        }

        // POST: Newsletter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("id,emailAdresa,lozinkaZaEmail")] Newsletter newsletter)
        {
            if (id != newsletter.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsletter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsletterExists(newsletter.id))
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
            return View(newsletter);
        }

        // GET: Newsletter/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletter = await _context.newsletteri
                .FirstOrDefaultAsync(m => m.id == id);
            if (newsletter == null)
            {
                return NotFound();
            }

            return View(newsletter);
        }

        // POST: Newsletter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsletter = await _context.newsletteri.FindAsync(id);
            if (newsletter != null)
            {
                _context.newsletteri.Remove(newsletter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsletterExists(int id)
        {
            return _context.newsletteri.Any(e => e.id == id);
        }
    }
}
