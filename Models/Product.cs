using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuFood.Models
{
    public partial class Product
    {
        public Product()
        {
            Product_price = new HashSet<Product_price>();
            Order_product = new HashSet<Order_product>();
            Product_store = new HashSet<Product_store>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Image { get; set; }

        public int Product_category_id { get; set; }

        [NotMapped]
        public List<int> List_store_id { get; set; }

        [NotMapped]
        public double Price { get; set; }
        
        public virtual Product_category? Product_category { get; set; }

        public virtual ICollection<Product_price>? Product_price { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Order_product>? Order_product { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Product_store> Product_store { get; set; } = null!;
    }
}
