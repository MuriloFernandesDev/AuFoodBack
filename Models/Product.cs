using Microsoft.EntityFrameworkCore;

namespace AuFood.Models
{
    public partial class Product
    {

        public Product()
        {
            ProductsPrice = new HashSet<ProductPrice>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public int QtdPeopleServe { get; set; }
        
        public double TimeDelivery { get; set; }
        
        public string Image { get; set; }

        public string? ListStoreId { get; set; }

        public int IdProductCategory { get; set; }

        public int IdClient { get; set; }

        public virtual Client? Client { get; set; }

        public virtual ProductCategory? ProductCategory { get; set; }

        public virtual ICollection<ProductPrice>? ProductsPrice { get; set; }
    }
}
