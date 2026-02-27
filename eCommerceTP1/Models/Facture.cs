namespace eCommerceTP1.Models
{
    public class Facture
    {
        public int Id { get; set; }
        public ICollection<ProduitFacture> ProduitsFacture { get; set; } = new List<ProduitFacture>();
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int VendeurId { get; set; }
        public Vendeur Vendeur { get; set; }
    }
}
