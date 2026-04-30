using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Benefit
    {
        [Key]
        public int id { get; set; }
        public required string opis { get; set; }
        public required string URLSlike { get; set; }

        public Benefit()
        {
            id = -1;
            opis = string.Empty;
            URLSlike = string.Empty;
        }

        public Benefit(int id, string opis, string URLSlike)
        {
            this.id = id;
            this.opis = opis;
            this.URLSlike = URLSlike;
        }
    }
}
