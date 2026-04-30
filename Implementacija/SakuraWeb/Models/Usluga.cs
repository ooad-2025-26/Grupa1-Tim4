using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Usluga
    {
        [Key]
        public int id { get; set; }
        public string naziv { get; set; }
        public string opis { get; set; }
        public double cijena { get; set; }
        public KategorijaUsluga kategorija { get; set; }
        public int trajanje { get; set; }
        public Usluga() { }
        public Usluga(string naziv, double cijena, KategorijaUsluga kategorija, int trajanje)
        {
            this.naziv = naziv;
            this.cijena = cijena;
            this.kategorija = kategorija;
            this.trajanje = trajanje;
        }

    }
}