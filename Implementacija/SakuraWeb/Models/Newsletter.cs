using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Newsletter
    {
        [Key]
        public int id { get; set; }
        public string emailAdresa { get; set; }
        public string lozinkaZaEmail { get; set; }

        public Newsletter()
        {
            id = -1;
        }

        public Newsletter(int id)
        {
            this.id = id;
        }

    }
}
