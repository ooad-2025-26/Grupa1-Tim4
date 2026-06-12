using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakuraWeb.Models
{
    public class Rezervacija
    {
        [Key]
        public int id { get; set; }
        [Required]
        public DateTime datumRezervacije { get; set; }
        [Required]
        public DateTime vrijemeTermina { get; set; }
        public bool otkazana { get; set; }
        [Range(1, 5, ErrorMessage ="Ocjena mora biti između 1 i 5!")]
        public int ocjena { get; set; }

        [ForeignKey("klijent")]
        public string klijentId { get; set; }
        public Korisnik klijent { get; set; }

        [ForeignKey("frizer")]
        public string? frizerId { get; set; }
        public Korisnik? frizer { get; set; }

        [ForeignKey("Usluga")]
        public int uslugaId { get; set; }
        public Usluga usluga { get; set; }

        public Rezervacija()
        {
            id = -1;
            datumRezervacije = default;
            vrijemeTermina = default;
            otkazana = false;
            klijentId = string.Empty;
            klijent = null;
            frizerId = null;
            frizer = null;
            uslugaId = 0;
            usluga = null;
        }

        public Rezervacija(int id, DateTime datumRezervacije, DateTime vrijemeTermina, bool otkazana, string klijentId, Korisnik klijent, string? frizerId, Korisnik? frizer, int uslugaId, Usluga usluga)
        {
            this.id = id;
            this.datumRezervacije = datumRezervacije;
            this.vrijemeTermina = vrijemeTermina;
            this.otkazana = otkazana;
            this.klijentId = klijentId;
            this.klijent = klijent;
            this.frizerId = frizerId;
            this.frizer = frizer;
            this.uslugaId = uslugaId;
            this.usluga = usluga;
        }
    }
}
