using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakuraWeb.Models
{
    public class Poruka
    {
        [Key]
        int id { get; set; }
        [ForeignKey("Anketa")]
        int anketaId;
        string naziv { get; set; }
        string tekst { get; set; }

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