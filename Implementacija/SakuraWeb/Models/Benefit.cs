using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Benefit
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(maximumLength:1000, MinimumLength =0, ErrorMessage ="Opis benefita mora imati ispod 1000 karaktera!")]
        [RegularExpression(@"[0-9| |a-z|A-Z|®]*", ErrorMessage = "Dozvoljeno je samo korištenje velikih i malih slova, brojeva i razmaka!")]
        public required string opis { get; set; }
        [Required]
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
