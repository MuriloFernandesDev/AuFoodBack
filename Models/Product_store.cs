using Newtonsoft.Json;

namespace AuFood.Models
{
    public partial class Product_store
    {
        public int Store_id { get; set; }
        
        public int Product_id { get; set; }

        public virtual Product Product { get; set; } = null!;

        public virtual Store Store { get; set; } = null!;
    }
}
