namespace AuFood.Models
{
    public partial class City
    {
        public City() {
            Store = new HashSet<Store>();
            Consumer_address = new HashSet<Consumer_address>();
        }
    
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int State_id { get; set; }

        public virtual State? State { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Store> Store { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Consumer_address> Consumer_address { get; set; }
    }
}
