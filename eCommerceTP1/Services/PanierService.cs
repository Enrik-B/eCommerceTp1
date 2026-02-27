using eCommerceTP1.Models;
using Microsoft.EntityFrameworkCore;

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
            _dbContext.Add(panier);
            _dbContext.SaveChanges();
        }
        public void ViderPanier(Panier panier) 
        {
            panier.ProduitsPanier.Clear();
            _dbContext.SaveChanges();
        }
        // Retourne null si le produit n'existe pas dans le panier.
        public ProduitPanier? estDansPanier(Panier panier, Produit produit) 
        {
            foreach (ProduitPanier item in panier.ProduitsPanier)
            {
                if (item.Produit.Id == produit.Id)
                {
                    return item;
                }
            }
            return null;
        }
        // Si le produit n'existe pas dans le panier, on crée une instance ProduitPanier.
        public void AddProduitPanier(Panier panier, Produit produit) 
        {
            ProduitPanier? produitPanier = estDansPanier(panier, produit);
            if (produitPanier != null)
                produitPanier.Quantite++;
            else
            {
                panier.ProduitsPanier.Add(new ProduitPanier
                {
                    ProduitId = produit.Id,
                    PanierId = panier.Id,
                    Quantite = 1
                });
            }
            _dbContext.SaveChanges();
        }
        // Retourne vrai si l'opération est un succès, sinon faux si le produit n'est pas trouvé.
        public bool SubstractProduitPanier(Panier panier, Produit produit) 
        {
            ProduitPanier? produitPanier = estDansPanier(panier, produit);
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
