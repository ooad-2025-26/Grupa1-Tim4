using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakuraWeb.Models
{
    public class ProizvodBenefit
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("Proizvod")]
        public int proizvodId { get; set; }

        [ForeignKey("Benefit")]
        public int benefitId { get; set; }

        public Proizvod Proizvod { get; set; }
        public Benefit Benefit { get; set; }
    }
}
