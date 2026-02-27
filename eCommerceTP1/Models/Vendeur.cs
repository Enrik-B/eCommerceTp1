namespace eCommerceTP1.Models
{
    public class Vendeur : User
    {
        // Factures dans User
        public ICollection<Produit> Produits { get; set; } = new List<Produit>();
    }
}
