using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakuraWeb.Models
{
    public class ProizvodSastojak
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("Proizvod")]
        public int proizvodId { get; set; }

        [ForeignKey("Sastojak")]
        public int sastojakId { get; set; }

        public Proizvod Proizvod { get; set; }
        public Sastojak Sastojak { get; set; }
    }
}
