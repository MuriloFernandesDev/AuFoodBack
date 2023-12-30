using Newtonsoft.Json;

namespace AuFood.Models
{
    public partial class OrderProduct
    {
        public int Quantity { get; set; }

        public double Price { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Order Order { get; set; } = null!;
        
        public virtual Product Product { get; set; } = null!;
    }
}
