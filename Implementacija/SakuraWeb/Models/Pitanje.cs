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

        public Pitanje()
        {
            sadrzaj = string.Empty;
        }

        public Pitanje(string sadrzaj)
        {
            this.sadrzaj = sadrzaj;
        }
    }
}