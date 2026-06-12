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

        public Anketa anketa { get; set; }

        [Required]
        public string sadrzaj { get; set; }

        public ICollection<Odgovor> odgovori { get; set; } = new List<Odgovor>();
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