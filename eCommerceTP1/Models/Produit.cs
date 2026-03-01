using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceTP1.Models
{
    public class Produit
    {
        [Key]
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public Decimal Prix { get; set; }
        public string Categorie { get; set; }
        // Lien URL à l'image
        public string Image { get; set; }
        // Produit publié par un vendeur
        [ForeignKey("VendeurId")]
        public int VendeurId { get; set; }
        public User vendeur { get; set; }
        public ICollection<ProduitPanier> ProduitsPanier { get; set; } = new List<ProduitPanier>();
        public ICollection<ProduitFacture> ProduitsFacture { get; set; } = new List<ProduitFacture>();

    }
}
