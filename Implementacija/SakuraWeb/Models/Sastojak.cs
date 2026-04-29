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
    }
}
