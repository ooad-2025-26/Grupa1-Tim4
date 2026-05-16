using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakuraWeb.Models
{
    public class Rezervacija
    {
        [Key]
        public int id { get; set; }
        public DateTime datumRezervacije { get; set; }
        public DateTime vrijemeTermina { get; set; }
        public bool otkazana { get; set; }
        public int ocjena { get; set; }

        [ForeignKey("Korisnik")]
        public string korisnikId { get; set; }
        public ApplicationUser korisnik { get; set; }

        [ForeignKey("Usluga")]
        public int uslugaId { get; set; }
        public Usluga usluga { get; set; }

        public Rezervacija()
        {
            id = -1;
            datumRezervacije = default;
            vrijemeTermina = default;
            otkazana = false;
            korisnikId = string.Empty;
            korisnik = null;
            uslugaId = 0;
            usluga = null;
        }

        public Rezervacija(int id, DateTime datumRezervacije, DateTime vrijemeTermina, bool otkazana, string korisnikId, ApplicationUser korisnik, int uslugaId, Usluga usluga)
        {
            this.id = id;
            this.datumRezervacije = datumRezervacije;
            this.vrijemeTermina = vrijemeTermina;
            this.otkazana = otkazana;
            this.korisnikId = korisnikId;
            this.korisnik = korisnik;
            this.uslugaId = uslugaId;
            this.usluga = usluga;
        }
    }
}
