using eCommerceTP1.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTP1.Services
{
    public class UserService
    {
        private readonly eCommerceTP1DbContext _dbContext;
        public UserService(eCommerceTP1DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Cette fonction charge également le panier de l'utilisateur (ou rien s'il s'agit d'un vendeur)
        public User? GetUserById(int id) 
        {
            User? user = _dbContext.Users.Include(u => u.Panier).FirstOrDefault(u => u.Id == id);
            return user;
        }
    }
}
