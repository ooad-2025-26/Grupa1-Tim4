using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Usluga
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(maximumLength:50, MinimumLength =3, ErrorMessage ="Naziv usluge smije imati između 3 i 50 karaktera!")]
        [RegularExpression(@"[0-9| |a-z|A-Z|®]*", ErrorMessage = "Dozvoljeno je samo korištenje velikih i malih slova, brojeva i razmaka!")]
        public string naziv { get; set; }
        [StringLength(maximumLength:1000, MinimumLength =0, ErrorMessage ="Opis usluge mora imati ispod 1000 karaktera!")]
        [RegularExpression(@"[0-9| |a-z|A-Z|®]*", ErrorMessage = "Dozvoljeno je samo korištenje velikih i malih slova, brojeva i razmaka!")]
        public string opis { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage ="Cijena mora biti veća od 0.01 KM")]
        public double cijena { get; set; }
        [Required]
        public KategorijaUsluga kategorija { get; set; }
        [Required]
        [Range(5, 300, ErrorMessage="Usluga može trajati između 5 minuta i 5 sati.")]
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