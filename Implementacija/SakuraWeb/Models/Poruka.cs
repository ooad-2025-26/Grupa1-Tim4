using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakuraWeb.Models
{
    public class Poruka
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Newsletter")]
        public int newsletterId { get; set; }

        public Newsletter newsletter { get; set; }
        [Required]
        [StringLength(maximumLength:100, MinimumLength =0, ErrorMessage ="Naziv poruke mora imati ispod 100 karaktera!")]
        public string naziv { get; set; }
        [Required]
        public string tekst { get; set; }

        public Poruka(string naziv, string tekst)
        {
            this.naziv = naziv;
            this.tekst = tekst;
        }

        public Poruka()
        {
            this.naziv = string.Empty;
            this.tekst = string.Empty;
        }
    }
}