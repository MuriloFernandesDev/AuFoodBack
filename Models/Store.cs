using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuFood.Models
{
    public partial class Store
    {
        public Store()
        {
            AvaliationsStories = new HashSet<AvaliationStore>();
            Order = new HashSet<Order>();
            StoreCategoryStore = new HashSet<StoreCategoryStore>();
            ConsumerStore = new HashSet<ConsumerStore>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }
        
        public string Description { get; set; }

        public string Whatsapp { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; }

        public string NumberAddress { get; set; }

        public string Cnpj { get; set; }

        public string? InstagramUrl { get; set; }

        public string? FacebookUrl { get; set; }

        public string BackgroundImage { get; set; }

        public string ColorPrimary { get; set; }

        public string ColorSecondary { get; set; }

        public string ColorBackground { get; set; }

        public string Zip { get; set; }

        public string Street { get; set; }

        public string Neighborhood { get; set; }

        public int CityId { get; set; }

        public virtual City? City { get; set; }

        public int ClientId { get; set; }

        public virtual Client? Client { get; set; }

        public ICollection<StoreCategoryStore> StoreCategoryStore { get; set; }
        
        public ICollection<AvaliationStore> AvaliationsStories { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Order> Order { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<ConsumerStore> ConsumerStore { get; set; } = null!;
    }
}
