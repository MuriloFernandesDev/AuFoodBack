using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuFood.Models
{
    public partial class Store
    {
        public Store()
        {
            Order = new HashSet<Order>();
            Store_category_store = new HashSet<Store_category_store>();
            Consumer_store = new HashSet<Consumer_store>();
            Product_store = new HashSet<Product_store>();
            Store_login = new HashSet<Store_login>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }
        
        public string Description { get; set; }

        public string Whatsapp { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; }

        public string Number_address { get; set; }

        public string Cnpj { get; set; }

        public string Background_image { get; set; }

        public string Color_primary { get; set; }

        public string Color_secondary { get; set; }

        public string Color_background { get; set; }

        public string Zip { get; set; }

        public string Street { get; set; }

        public string Neighborhood { get; set; }

        public int City_id { get; set; }

        public virtual City? City { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Store_login> Store_login { get; set; }

        public ICollection<Store_category_store> Store_category_store { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Order> Order { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Consumer_store> Consumer_store { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Product_store> Product_store { get; set; }
    }
}
