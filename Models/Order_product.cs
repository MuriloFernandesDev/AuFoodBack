using Newtonsoft.Json;

namespace AuFood.Models
{
    public partial class Order_product
    {
        public int Id { get; set; }

        public double Price { get; set; }

        public string? Observation { get; set; }

        public int Order_id { get; set; }

        public int Product_id { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Order Order { get; set; } = null!;
        
        public virtual Product Product { get; set; } = null!;
    }
}
