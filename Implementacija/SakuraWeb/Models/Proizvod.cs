using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Proizvod
    {
        [Key]
        public int id { get; set; }
        public required string naziv { get; set; }
        public double cijena { get; set; }
        public KategorijaProizvoda kategorija { get; set; }
        private double volumen { get; set; }
    }
}
