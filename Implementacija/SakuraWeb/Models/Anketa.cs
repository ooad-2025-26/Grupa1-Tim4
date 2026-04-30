using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Anketa
    {
        [Key]
        int id;
        //static List<Pitanje> pitanja { get; set; } // Trenutno van mojih mogucnosti
        public static void ucitajPitanja()
        {

        }

        public static int dajRezultatAnkete()
        {
            return 0;
        }
    }
}
