using Microsoft.EntityFrameworkCore;

namespace eCommerceTP1
{
    public class eCommerceTP1DbContext : DbContext
    {
        // Les DbSets pour chaque modele
        // public DbSet<Models.Client> Clients { get; set; }
        // public DbSet<Models.Vendeur> Vendeurs { get; set; }
        // public DbSet<Models.Produit> Produits { get; set; }
        // public DbSet<Models.Panier> Paniers { get; set; }
        // public DbSet<Models.Facture> Factures { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection_string = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string database_name = "eCommerceTP1DB";
            optionsBuilder.UseSqlServer($"{connection_string};Database={database_name};");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // initialisation de données
        }
    }
}
