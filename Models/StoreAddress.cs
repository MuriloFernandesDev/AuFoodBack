﻿namespace AuFood.Models
{
    public partial class StoreAddress
    {
        public StoreAddress() {
            Store = new HashSet<Store>();
        }
    
        public int Id { get; set; }
        
        public string Zip { get; set; }

        public string Street { get; set; }

        public string Neighborhood { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Store> Store { get; set; }
    }
}
