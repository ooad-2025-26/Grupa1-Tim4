using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Korisnik : IdentityUser
    {
        //[Key]
        //public string id { get; set; }
        public string korisnickoIme { get; set; }
        public string emailAdresa { get; set; }
        public string lozinka { get; set; }
        public bool jePretplacenNaNewsletter { get; set; }
        public Uloga ulogaKorisnika { get; set; }

        public Korisnik(/*string id,*/ string korisnickoIme, string emailAdresa, string lozinka, bool jePretplacenNaNewsletter, Uloga ulogaKorisnika)
        {
            //this.id = id;
            this.korisnickoIme = korisnickoIme;
            this.emailAdresa = emailAdresa;
            this.lozinka = lozinka;
            this.jePretplacenNaNewsletter = jePretplacenNaNewsletter;
            this.ulogaKorisnika = ulogaKorisnika;
        }

        public Korisnik()
        {
            /*this.id = string.Empty;*/
            this.korisnickoIme = string.Empty;
            this.emailAdresa = string.Empty;
            this.lozinka = string.Empty;
            this.jePretplacenNaNewsletter = false;
            //this.ulogaKorisnika = ulogaKorisnika;
        }

        public void promjeniStanjePretplate(bool stanje)
        {
            jePretplacenNaNewsletter = stanje;
        }
    }
}
