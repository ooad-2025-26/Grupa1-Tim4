using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Proizvod
    {
        [Key]
        public int id { get; set; }
        public required string naziv { get; set; }
        public double cijena { get; set; }
        public KategorijaProizvoda kategorija { get; set; }
        public double volumen { get; set; }

        public Proizvod()
        {
            id = -1;
            naziv = string.Empty;
            cijena = 0;
            kategorija = default;
            volumen = 0;
        }

        public Proizvod(int id, string naziv, double cijena, KategorijaProizvoda kategorija, double volumen)
        {
            this.id = id;
            this.naziv = naziv;
            this.cijena = cijena;
            this.kategorija = kategorija;
            this.volumen = volumen;
        }
    }
}
