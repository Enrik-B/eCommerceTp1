using System.ComponentModel.DataAnnotations;

namespace eCommerceTP1.Models
{
    public class CommandeProduit
    {
        [Key]
        public int Id { get; set; }
        public int ProduitId { get; set; }
        public Produit Produit { get; set; }
        public int CommandeId { get; set; }
        public Commande Commande { get; set; }
        public int Quantite { get; set; }
    }
}
