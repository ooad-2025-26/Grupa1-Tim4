using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public enum KategorijaProizvoda
    {
        [Display(Name = "Šampon")]
        Šampon,
        [Display(Name = "Regenerator")]
        Regenerator,
        [Display(Name = "Ulje")]
        Ulje,
        [Display(Name = "Maska")]
        Maska
    }
}
