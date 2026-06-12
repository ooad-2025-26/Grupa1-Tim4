using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public enum KategorijaUsluga
    {
        [Display(Name = "Šišanje")]
        Šišanje,
        [Display(Name = "Feniranje")]
        Feniranje,
        [Display(Name = "Farbanje")]
        Farbanje,
        [Display(Name = "Stilizovanje")]
        Stilizovanje
    }
}