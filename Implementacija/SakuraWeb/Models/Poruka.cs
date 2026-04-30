using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakuraWeb.Models
{
    public class Poruka
    {
        [Key]
        int id { get; set; }
        [ForeignKey("Newsletter")]
        public int newsletterId { get; set; }
        public string naziv { get; set; }
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