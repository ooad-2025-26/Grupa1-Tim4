using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakuraWeb.Models
{
    public class Proizvod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Naziv proizvoda smije imati između 3 i 50 karaktera!")]
        [DisplayName("naziv")]
        public required string naziv { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cijena mora biti veća od 0.01 KM")]
        [DisplayName("cijena")]
        public double cijena { get; set; }

        [EnumDataType(typeof(KategorijaProizvoda))]
        public KategorijaProizvoda kategorija { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Volumen mora biti iznad 0.01 ml")]
        [DisplayName("volumen")]
        public double volumen { get; set; }

        public string? slikaPutanja { get; set; }

        // NOVO POLJE
        [Range(0, 12, ErrorMessage = "Bodovi proizvoda moraju biti između 0 i 12!")]
        [DisplayName("poeniPr")]
        public int poeniPr { get; set; }

        public Proizvod()
        {
            naziv = string.Empty;
            cijena = 0;
            kategorija = default;
            volumen = 0;
            poeniPr = 0;
        }

        public Proizvod(string naziv, double cijena, KategorijaProizvoda kategorija, double volumen)
        {
            this.naziv = naziv;
            this.cijena = cijena;
            this.kategorija = kategorija;
            this.volumen = volumen;
        }
    }
}