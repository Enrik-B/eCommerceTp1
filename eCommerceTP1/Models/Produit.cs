using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceTP1.Models
{
    public class Produit
    {
        [Key]
        public int Id { get; set; }
        // Id du produit dans l'API
        public int ApiId { get; set; }
        // Produit publié par un vendeur
        [ForeignKey("VendeurId")]
        public int VendeurId { get; set; }
        public User vendeur { get; set; }
        public ICollection<ProduitPanier> ProduitsPanier { get; set; } = new List<ProduitPanier>();
        public ICollection<ProduitFacture> ProduitsFacture { get; set; } = new List<ProduitFacture>();
        public ICollection<CommandeProduit> ProduitsCommande { get; set; } = new List<CommandeProduit>();

        public ProduitAPI? GetAPIProduit() 
        {
            return ProduitResponseGlobal.Products.Find(p => p.Id == ApiId);
        }
    }
}
