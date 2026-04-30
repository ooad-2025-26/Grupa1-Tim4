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

        public Proizvod proizvod { get; set; }
        public Sastojak sastojak { get; set; }

        public ProizvodSastojak()
        {
            id = -1;
            proizvodId = 0;
            sastojakId = 0;
            proizvod = null;
            sastojak = null;
        }

        public ProizvodSastojak(int id, int proizvodId, int sastojakId, Proizvod proizvod, Sastojak sastojak)
        {
            this.id = id;
            this.proizvodId = proizvodId;
            this.sastojakId = sastojakId;
            this.proizvod = proizvod;
            this.sastojak = sastojak;
        }
    }
}
