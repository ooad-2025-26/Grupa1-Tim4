using Microsoft.AspNetCore.Identity;

namespace SakuraWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool jePretplacenNaNewsletter { get; set; }
        public Uloga ulogaKorisnika { get; set; }
    }
}
