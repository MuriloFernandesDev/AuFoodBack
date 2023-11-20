using Newtonsoft.Json;

namespace AuFood.Models
{
    public partial class Consumer
    {
        public Consumer()
        {
            ConsumerAddress = new HashSet<ConsumerAddress>();
            Order = new HashSet<Order>();
            ConsumerStore = new HashSet<ConsumerStore>();
        }
        
        public int Id { get; set; }

        public string Phone { get; set; }

        public bool PhoneConfirmed { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public string? Code { get; set; }

        public string? Email {get; set;}

        [JsonIgnore]
        public byte[]? Password { get; set; } = null!;

        public ICollection<ConsumerAddress> ConsumerAddress { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Order> Order { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<ConsumerStore> ConsumerStore { get; set; }
    }
}
