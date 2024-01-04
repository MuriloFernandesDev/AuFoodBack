using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuFood.Models
{
    public partial class Store_login
    {
        public int Store_id { get; set; }
        
        public int Login_id { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Store Store { get; set; } = null!;

        public virtual Login Login { get; set; } = null!;
    }
}
