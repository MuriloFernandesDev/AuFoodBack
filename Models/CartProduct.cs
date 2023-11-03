using Newtonsoft.Json;

namespace AuFood.Models
{
    public partial class CartProduct
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Cart Cart { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
