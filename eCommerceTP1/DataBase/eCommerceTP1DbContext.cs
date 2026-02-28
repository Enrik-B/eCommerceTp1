using eCommerceTP1.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTP1
{
    public class eCommerceTP1DbContext : DbContext
    {
        // Constructeur pour EF Core + MySQL
        public eCommerceTP1DbContext(DbContextOptions<eCommerceTP1DbContext> options)
            : base(options)
        {
        }

        // DbSets

        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Produit> Produits { get; set; }
        public DbSet<Models.Panier> Paniers { get; set; }
        public DbSet<Models.ProduitPanier> ProduitPaniers { get; set; }
        public DbSet<Models.Facture> Factures { get; set; }
        public DbSet<Models.ProduitFacture> ProduitFactures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
