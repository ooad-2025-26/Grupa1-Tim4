using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SakuraWeb.Models;

namespace SakuraWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<Korisnik>//IdentityDbContext(options)
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Models.Anketa> ankete { get; set; }
        public DbSet<Models.Benefit> benefiti { get; set; }
        public DbSet<Models.Korisnik> korisnici { get; set; }
        public DbSet<Models.Newsletter> newsletteri { get; set; }
        public DbSet<Models.Odgovor> odgovori { get; set; }
        public DbSet<Models.Pitanje> pitanja { get; set; }
        public DbSet<Models.Poruka> poruke { get; set; }
        public DbSet<Models.Proizvod> proizvodi { get; set; }
        public DbSet<Models.ProizvodBenefit> proizvodiBenefiti { get; set; }
        public DbSet<Models.ProizvodSastojak> proizvodiSastojci { get; set; }
        public DbSet<Models.Rezervacija> rezervacije { get; set; }
        public DbSet<Models.Sastojak> sastojci { get; set; }
        public DbSet<Models.Usluga> usluge { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);





            modelBuilder.Entity<Models.Anketa>().ToTable("Ankete");
            modelBuilder.Entity<Models.Benefit>().ToTable("Benefiti");
            modelBuilder.Entity<Models.Korisnik>().ToTable("Korisnici");
            modelBuilder.Entity<Korisnik>()
                .Property(e => e.korisnickoIme)
                .HasMaxLength(250);
            modelBuilder.Entity<Korisnik>()
                .Property(e => e.emailAdresa)
                .HasMaxLength(250);


            modelBuilder.Entity<Models.Newsletter>().ToTable("Newsletteri");
            modelBuilder.Entity<Models.Odgovor>().ToTable("Odgovori");
            modelBuilder.Entity<Models.Pitanje>().ToTable("Pitanja");
            modelBuilder.Entity<Models.Poruka>().ToTable("Poruke");
            modelBuilder.Entity<Models.Proizvod>().ToTable("Proizvodi");
            modelBuilder.Entity<Models.ProizvodBenefit>().ToTable("ProizvodiBenefiti");
            modelBuilder.Entity<Models.ProizvodSastojak>().ToTable("ProizvodiSastojci");
            modelBuilder.Entity<Models.Rezervacija>().ToTable("Rezervacije");
            modelBuilder.Entity<Models.Sastojak>().ToTable("Sastojci");
            modelBuilder.Entity<Models.Usluga>().ToTable("Usluge");


            // --- Pitanja ---
            modelBuilder.Entity<Pitanje>().HasData(
                new Pitanje { id = 1, sadrzaj = "Ključni problem tjemena?" },
                new Pitanje { id = 2, sadrzaj = "Ključni problem vrhova kose?" },
                new Pitanje { id = 3, sadrzaj = "Na koji način stilizujete?" },
                new Pitanje { id = 4, sadrzaj = "Koliko često koristiš proizvode za njegu?" }
            );

            // --- Odgovori ---
            modelBuilder.Entity<Odgovor>().HasData(
                // Pitanje 1 - tjeme
                new Odgovor { id = 1, pitanjeId = 1, sadrzaj = "masno", poeni = 0 },
                new Odgovor { id = 2, pitanjeId = 1, sadrzaj = "normalno", poeni = 1 },
                new Odgovor { id = 3, pitanjeId = 1, sadrzaj = "suho", poeni = 3 },

                // Pitanje 2 - vrhovi
                new Odgovor { id = 4, pitanjeId = 2, sadrzaj = "masna", poeni = 0 },
                new Odgovor { id = 5, pitanjeId = 2, sadrzaj = "oštećena", poeni = 2 },
                new Odgovor { id = 6, pitanjeId = 2, sadrzaj = "suha", poeni = 3 },

                // Pitanje 3 - stilizovanje
                new Odgovor { id = 7, pitanjeId = 3, sadrzaj = "sušenje", poeni = 1 },
                new Odgovor { id = 8, pitanjeId = 3, sadrzaj = "feniranje", poeni = 2 },
                new Odgovor { id = 9, pitanjeId = 3, sadrzaj = "pegla/figaro", poeni = 3 },

                // Pitanje 4 - učestalost njege
                new Odgovor { id = 10, pitanjeId = 4, sadrzaj = "ponekad", poeni = 1 },
                new Odgovor { id = 11, pitanjeId = 4, sadrzaj = "sedmično", poeni = 2 },
                new Odgovor { id = 12, pitanjeId = 4, sadrzaj = "svaki dan", poeni = 3 }
            );

        }
    }
}
