using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SakuraWeb.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
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
            modelBuilder.Entity<Models.Anketa>().ToTable("Ankete");
            modelBuilder.Entity<Models.Benefit>().ToTable("Benefiti");
            modelBuilder.Entity<Models.Korisnik>().ToTable("Korisnici");
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
        }
    }
}
