using AuFood.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace AuFood.Auxiliary
{
    public class ProductAux
    {

        public static async Task<List<int>> GetAllProductOnStore(_DbContext _context, int StoreId)
        {
            var Products = _context.Product
                .Include(w => w.ProductStore)
                .Where(w => w.ProductStore.Count > 0)
                .AsQueryable();

            var productIds = new List<int>();

            foreach (var product in Products)
            {
                var listStoreId = product.ProductStore.Select(w => w.StoreId.ToString()).ToList();

                if (listStoreId.Contains(StoreId.ToString()))
                    productIds.Add(product.Id);
            }

            return productIds;
        }
    }
}
