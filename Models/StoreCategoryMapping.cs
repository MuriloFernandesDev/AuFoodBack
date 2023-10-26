namespace AuFood.Models
{
    public partial class StoreCategoryMapping
    {
        public int? StoreId { get; set; }
        public int StoreCategoryId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Store? Store { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public StoreCategory? StoreCategory { get; set; }
    }
}
