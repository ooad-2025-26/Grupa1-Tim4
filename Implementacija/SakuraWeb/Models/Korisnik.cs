using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Korisnik : IdentityUser
    {
        public string korisnickoIme { get; set; }
        public string emailAdresa { get; set; }
        public string lozinka { get; set; }
        public bool jePretplacenNaNewsletter { get; set; }
        public Uloga ulogaKorisnika { get; set; }

        public Korisnik(string korisnickoIme, string emailAdresa, string lozinka, bool jePretplacenNaNewsletter, Uloga ulogaKorisnika)
        {
            this.korisnickoIme = korisnickoIme;
            this.emailAdresa = emailAdresa;
            this.lozinka = lozinka;
            this.jePretplacenNaNewsletter = jePretplacenNaNewsletter;
            this.ulogaKorisnika = ulogaKorisnika;
        }

        public Korisnik()
        {
            this.korisnickoIme = string.Empty;
            this.emailAdresa = string.Empty;
            this.lozinka = string.Empty;
            this.jePretplacenNaNewsletter = false;
        }

        public void promjeniStanjePretplate(bool stanje)
        {
            jePretplacenNaNewsletter = stanje;
        }
    }
}
