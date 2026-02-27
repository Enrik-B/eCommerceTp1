namespace eCommerceTP1.Models
{
    public class Panier
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public ICollection<ProduitPanier> ProduitsPanier { get; set; } = new List<ProduitPanier>();

    }
}
