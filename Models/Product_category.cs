namespace AuFood.Models
{
    public partial class Product_category
    {
        public Product_category()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; } = "fas fa-utensils";
        public string Image { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Product>? Product { get; set; }
    }
}
