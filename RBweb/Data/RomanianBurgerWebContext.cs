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

        public DbSet<Meniu> Meniu { get; set; } = default!;
        public DbSet<Categorie> Categorie { get; set; } = default!;
        public DbSet<MeniuCategorie> MeniuCategorie { get; set; } = default!;
        public DbSet<Client> Client { get; set; } = default!;

        public DbSet<Comanda> Comanda { get; set; } = default!;
        public DbSet<ComandaItem> ComandaItem { get; set; } = default!;
        public DbSet<RecenzieDto> Recenzii { get; set; }
    }
}
