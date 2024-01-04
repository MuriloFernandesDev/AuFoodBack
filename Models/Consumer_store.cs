using Newtonsoft.Json;

namespace AuFood.Models
{
    public partial class Consumer_store
    {
        public int Store_id { get; set; }
        public int Consumer_id { get; set; }

        public virtual Consumer Consumer { get; set; } = null!;

        public virtual Store Store { get; set; } = null!;
    }
}
