namespace AuFood.Models
{
    public partial class Store_category_store
    {
        public int Store_id { get; set; }
        public int Store_category_id { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Store? Store { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Store_category? Store_category { get; set; }
    }
}
