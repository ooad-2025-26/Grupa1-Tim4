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
        [StringLength(maximumLength:50, MinimumLength =3, ErrorMessage ="Naziv proizvoda smije imati između 3 i 50 karaktera!")]
        //[RegularExpression(@"[0-9| |a-z|A-Z|®]*", ErrorMessage = "Dozvoljeno je samo korištenje velikih i malih slova, brojeva i razmaka!")]
        [DisplayName("naziv")]
        public required string naziv { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage ="Cijena mora biti veća od 0.01 KM")]
        [DisplayName("cijena")]
        public double cijena { get; set; }


        [EnumDataType(typeof(KategorijaProizvoda))]     public KategorijaProizvoda kategorija { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Volumen mora biti iznad 0.01 ml")]
        [DisplayName("volumen")]
        public double volumen { get; set; }

        //[Required]
        public string? slikaPutanja { get; set; }

        public Proizvod()
        {
            //id = -1;
            naziv = string.Empty;
            cijena = 0;
            kategorija = default;
            volumen = 0;
        }

        public Proizvod(/*int id,*/ string naziv, double cijena, KategorijaProizvoda kategorija, double volumen)
        {
            //this.id = id;
            this.naziv = naziv;
            this.cijena = cijena;
            this.kategorija = kategorija;
            this.volumen = volumen;
        }
    }
}
