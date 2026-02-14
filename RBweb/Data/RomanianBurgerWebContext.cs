using Microsoft.EntityFrameworkCore;
using RBweb.Models;

namespace RomanianBurgerWeb.Data
{
    public class RomanianBurgerWebContext : DbContext
    {
        public RomanianBurgerWebContext(DbContextOptions<RomanianBurgerWebContext> options)
            : base(options)
        {
        }

        public DbSet<Meniu> Meniu { get; set; }

        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<MeniuCategorie> MeniuCategorie { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Comanda> Comanda { get; set; }

    }
}
