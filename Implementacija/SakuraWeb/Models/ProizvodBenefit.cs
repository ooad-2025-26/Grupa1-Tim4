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

        public Proizvod proizvod { get; set; }
        public Benefit benefit { get; set; }

        public ProizvodBenefit()
        {
            id = -1;
            proizvodId = 0;
            benefitId = 0;
            proizvod = null;
            benefit = null;
        }

        public ProizvodBenefit(int id, int proizvodId, int benefitId, Proizvod proizvod, Benefit benefit)
        {
            this.id = id;
            this.proizvodId = proizvodId;
            this.benefitId = benefitId;
            this.proizvod = proizvod;
            this.benefit = benefit;
        }
    }
}
