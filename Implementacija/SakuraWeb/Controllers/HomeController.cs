using Microsoft.AspNetCore.Mvc;
using SakuraWeb.Models;
using SakuraWeb.Data;
using System.Linq;
using System.Diagnostics;

namespace SakuraWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context; // AI-assisted

        public HomeController(ApplicationDbContext context)
        {
            _context = context; // AI-assisted
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel // AI-assisted 
            {
                KorisniciCount = _context.korisnici.Count(),
                RezervacijeCount = _context.rezervacije.Count(),
                ProizvodiCount = _context.proizvodi.Count()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
