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
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(

                // pw en ordre: enrikbernier1, lyliaamiour1
                new User { Id = 1, FirstName = "Enrik", LastName = "Bernier", Gender = "Homme", Email = "enrik@gmail.com", Phone = "123456789", Username = "enrikbernier", Password = "$2a$11$Ol4XWH2rNJNQxEY2WxjE3.aZZdRX/k6tc7YTJXGJpJ9SSvYO54Wpm", BirthDate = DateTime.Now, Country = "Canada", Role = "Vendeur" },
                new User { Id = 2, FirstName = "Lylia", LastName = "Amiour", Gender = "Femme", Email = "lylia@gmail.com", Phone = "123456789", Username = "lyliamiour", Password = "$2a$11$epDbhg4jSftdxdfx2VIX5OXQHG0Vj/1Gxli.qRnFoJvmClCBmgg9u", BirthDate = DateTime.Now, Country = "Algérie", Role = "Vendeur" }
            );
        }
    }
}
