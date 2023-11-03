using Newtonsoft.Json;

namespace AuFood.Models
{
    public partial class CartProduct
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }


        [JsonIgnore]
        public Cart Cart { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
