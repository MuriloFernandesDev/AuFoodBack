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
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
