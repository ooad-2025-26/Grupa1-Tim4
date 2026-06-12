using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Sastojak
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(maximumLength:100, MinimumLength =0, ErrorMessage ="Naziv sastojka mora imati ispod 100 karaktera!")]
        [RegularExpression(@"[0-9| |a-z|A-Z|®]*", ErrorMessage = "Dozvoljeno je samo korištenje velikih i malih slova, brojeva i razmaka!")]
        public required string naziv { get; set; }
        [Required]
        [StringLength(maximumLength:100, MinimumLength =0, ErrorMessage ="Opis sastojka mora imati ispod 1000 karaktera!")]
        [RegularExpression(@"[0-9| |a-z|A-Z|®]*", ErrorMessage = "Dozvoljeno je samo korištenje velikih i malih slova, brojeva i razmaka!")]
        public required string opis { get; set; }
        [Required]
        public required string URLSlike { get; set; }

        public ICollection<ProizvodSastojak> ProizvodSastojci { get; set; }
        = new List<ProizvodSastojak>();

        public Sastojak()
        {
            id = -1;
            naziv = string.Empty;
            opis = string.Empty;
            URLSlike = string.Empty;
        }
        public Sastojak(int id, string naziv, string opis, string URLSlike)
        {
            this.id = id;
            this.naziv = naziv;
            this.opis = opis;
            this.URLSlike = URLSlike;
        }
    }
}
