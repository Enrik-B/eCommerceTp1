using eCommerceTP1.Models;

namespace eCommerceTP1.Services
{
    public class PanierService
    {
        private readonly eCommerceTP1DbContext _dbContext;
        public PanierService(eCommerceTP1DbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public Panier? GetPanierById(int Id) 
        {
            Panier? panier = _dbContext.Paniers.Find(Id);
            return panier;
        }
        public void AddPanier(Panier panier) 
        {
            _dbContext.Paniers.Add(panier);
            _dbContext.SaveChanges();
        }
        public void ViderPanier(Panier panier) 
        {
            panier.ProduitsPanier.Clear();
            _dbContext.SaveChanges();
        }
        // Retourne null si le produit n'existe pas dans le Panier.
        public ProduitPanier? estDansPanier(Panier panier, int Id) 
        {
            foreach (ProduitPanier item in panier.ProduitsPanier)
            {
                if (item.Produit.Id == Id)
                {
                    return item;
                }
            }
            return null;
        }
        // Si le produit n'existe pas dans le Panier, on crée une instance ProduitPanier.
        public void AddProduitPanier(Panier panier, int Id) 
        {
            ProduitPanier? produitPanier = estDansPanier(panier, Id);
            if (produitPanier != null)
                produitPanier.Quantite++;
            else
            {
                panier.ProduitsPanier.Add(new ProduitPanier
                {
                    ProduitId = Id,
                    PanierId = panier.Id,
                    Quantite = 1
                });
            
            }
            _dbContext.SaveChanges();
        }
        // Retourne vrai si l'opération est un succès, sinon faux si le produit n'est pas trouvé.
        public bool SubstractProduitPanier(Panier panier, int ApiId) 
        {
            ProduitPanier? produitPanier = estDansPanier(panier, ApiId);
            if (produitPanier != null)
            {
                produitPanier.Quantite--;
                if (produitPanier.Quantite <= 0)
                {
                    panier.ProduitsPanier.Remove(produitPanier);
                }
                _dbContext.SaveChanges();
                return true;
            }
            else 
            {
                return false;
            }
        }

        // Ne doit être utilisé que lorsque le client associé n'existe plus.
        public void DeletePanier(Panier panier) 
        {
            _dbContext.Paniers.Remove(panier);
            _dbContext.SaveChanges();
        }
    }
}
