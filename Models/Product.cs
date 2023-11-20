using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AuFood.Models
{
    public partial class Product
    {

        public Product()
        {
            ProductsPrice = new HashSet<ProductPrice>();
            OrderProduct = new HashSet<OrderProduct>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public int QtdPeopleServe { get; set; }
        
        public double TimeDelivery { get; set; }
        
        public string Image { get; set; }

        public string? ListStoreId { get; set; }

        public int ProductCategoryId { get; set; }

        public int ClientId { get; set; }

        public virtual Client? Client { get; set; }

        public virtual ProductCategory? ProductCategory { get; set; }

        public virtual ICollection<ProductPrice>? ProductsPrice { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<OrderProduct>? OrderProduct { get; set; }
    }
}
