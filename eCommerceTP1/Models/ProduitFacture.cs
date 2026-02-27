namespace eCommerceTP1.Models
{
    public class ProduitFacture
    {
        public int Id { get; set; }
        public int ProduitId { get; set; }
        public Produit Produit { get; set; }
        public int FactureId { get; set; }
        public Facture Facture { get; set; }
        public int Quantite { get; set; }
    }
}
