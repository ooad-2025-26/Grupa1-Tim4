using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Korisnik
    {
        [Key]
        int id { get; set; }
        string korisnickoIme { get; set; }
        string emailAdresa { get; set; }
        string lozinka { get; set; }
        bool jePretplacenNaNewsletter { get; set; }
        Uloga ulogaKorisnika { get; set; }

        public Korisnik(int id, string korisnickoIme, string emailAdresa, string lozinka, bool jePretplacenNaNewsletter, Uloga ulogaKorisnika)
        {
            this.id = id;
            this.korisnickoIme = korisnickoIme;
            this.emailAdresa = emailAdresa;
            this.lozinka = lozinka;
            this.jePretplacenNaNewsletter = jePretplacenNaNewsletter;
            this.ulogaKorisnika = ulogaKorisnika;
        }

        public Korisnik()
        {
            this.id = -1;
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
