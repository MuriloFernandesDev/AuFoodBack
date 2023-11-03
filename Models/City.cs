namespace AuFood.Models
{
    public partial class City
    {
        public City() {
            Stories = new HashSet<Store>();
            ConsumerAddress = new HashSet<ConsumerAddress>();
        }
    
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public int StateId { get; set; }

        public virtual State? State { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Store> Stories { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<ConsumerAddress> ConsumerAddress { get; set; }
    }
}
