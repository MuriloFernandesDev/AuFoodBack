using Newtonsoft.Json;

namespace AuFood.Models
{
    public partial class Consumer_address
    {
        public Consumer_address()
        {
            Order = new HashSet<Order>();
        }
        
        public int Id { get; set; }

        public string? Street { get; set; }

        public string? Number { get; set; }

        public string? Neighborhood { get; set; }

        public string? Complement { get; set; }
        
        public int? ZipCode { get; set; }
        
        public int? City_id { get; set; }

        public int Consumer_id { get; set; }

        public virtual City? City { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Consumer? Consumer { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Order> Order { get; set; }
    }
}
