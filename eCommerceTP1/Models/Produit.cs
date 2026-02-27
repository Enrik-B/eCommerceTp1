namespace eCommerceTP1.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public float Prix { get; set; }
        public string Categorie { get; set; }
        // Lien URL à l'image
        public string Image { get; set; }
        // Produit publié par un vendeur
        public int VendeurId { get; set; }
        public Vendeur Vendeur { get; set; }
        public ICollection<ProduitPanier> ProduitsPanier { get; set; } = new List<ProduitPanier>();
        public ICollection<ProduitFacture> ProduitsFacture { get; set; } = new List<ProduitFacture>();

    }
}
