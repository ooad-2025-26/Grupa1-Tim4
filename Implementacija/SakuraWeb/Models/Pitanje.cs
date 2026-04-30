using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakuraWeb.Models
{
    public class Pitanje
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Anketa")]
        public int anketaId { get; set; }
        public string sadrzaj { get; set; }
        public List<string> odgovori { get; set; }
        public List<int> poeni { get; set; }


        public Pitanje()
        {
            odgovori = new List<string>();
            poeni = new List<int>();
        }

        public Pitanje(string sadrzaj, List<string> odgovori, List<int> poeni)
        {
            this.sadrzaj = sadrzaj;
            this.odgovori = odgovori;
            this.poeni = poeni;
        }
    }
}