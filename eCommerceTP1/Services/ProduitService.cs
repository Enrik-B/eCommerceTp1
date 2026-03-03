using eCommerceTP1.Models;

namespace eCommerceTP1.Services
{
    public class ProduitService
    {
        private readonly eCommerceTP1DbContext _dbContext;
        public ProduitService(eCommerceTP1DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Produit? GetProduitById(int id) 
        {
            Produit? produit = _dbContext.Produits.Find(id);
            return produit;
        }
    }
}
