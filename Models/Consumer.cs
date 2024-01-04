using Newtonsoft.Json;

namespace AuFood.Models
{
    public partial class Consumer
    {
        public Consumer()
        {
            Consumer_address = new HashSet<Consumer_address>();
            Order = new HashSet<Order>();
            Consumer_store = new HashSet<Consumer_store>();
        }
        
        public int Id { get; set; }

        public string Phone { get; set; }

        public bool Phone_confirmed { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public string? Code { get; set; }

        public string? Email {get; set;}

        [JsonIgnore]
        public byte[]? Password { get; set; } = null!;

        public ICollection<Consumer_address> Consumer_address { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Order> Order { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Consumer_store> Consumer_store { get; set; }
    }
}
