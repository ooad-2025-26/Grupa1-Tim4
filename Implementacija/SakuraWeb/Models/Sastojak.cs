using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Sastojak
    {
        [Key]
        public int id { get; set; }
        public required string naziv { get; set; }
        public required string opis { get; set; }
        public required string URLSlike { get; set; }

        public Sastojak()
        {
            id = -1;
            naziv = string.Empty;
            opis = string.Empty;
            URLSlike = string.Empty;
        }
        public Sastojak(int id, string naziv, string opis, string URLSlike)
        {
            this.id = id;
            this.naziv = naziv;
            this.opis = opis;
            this.URLSlike = URLSlike;
        }
    }
}
