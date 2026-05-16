using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakuraWeb.Models
{
    public class Anketa
    {
        [Key]
        public int id { get; set; }
        public int poeni { get; set; }
        public DateTime vrijemePopunjavanja { get; set; }

        public Anketa()
        {
            id = -1;
        }

        public Anketa(int id)
        {
            this.id = id;
        }


        [ForeignKey("Korisnik")]
        public string korisnikId { get; set; }
        public Korisnik korisnik { get; set; }
    }
}
