using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakuraWeb.Models
{
    public class Odgovor
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Pitanje")]
        public int pitanjeId { get; set; }

        public Pitanje pitanje { get; set; }

        [Required]
        public string sadrzaj { get; set; }

        [Range(0, 3, ErrorMessage = "Bodovi odgovora moraju biti između 0 i 3!")]
        public int poeni { get; set; }

        public Odgovor()
        {
            sadrzaj = string.Empty;
            poeni = 0;
        }
        public Odgovor(string sadrzaj, int poeni)
        {
            this.sadrzaj = sadrzaj;
            this.poeni = poeni;
        }
    }
}