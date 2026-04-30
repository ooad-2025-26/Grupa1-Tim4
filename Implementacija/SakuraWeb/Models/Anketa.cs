using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Anketa
    {
        [Key]
        public int id { get; set; }

        public Anketa()
        {
            id = -1;
        }

        public Anketa(int id)
        {
            this.id = id;
        }

        public static void ucitajPitanja()
        {

        }

        public static int dajRezultatAnkete()
        {
            return 0;
        }
    }
}
