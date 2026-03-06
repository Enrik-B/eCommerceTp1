using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceTP1.Models
{
    public class Produit
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Decimal Price { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
        public List<string>? Images { get; set; }
        [ForeignKey("VendeurId")]
        public int VendeurId { get; set; }
        public User Vendeur { get; set; }
        public ICollection<ProduitPanier> ProduitsPanier { get; set; } = new List<ProduitPanier>();
        public ICollection<ProduitFacture> ProduitsFacture { get; set; } = new List<ProduitFacture>();
        public ICollection<CommandeProduit> ProduitsCommande { get; set; } = new List<CommandeProduit>();
    }
    public class ProduitResponse
    {
        public List<Produit>? Products { get; set; }
    }
}
