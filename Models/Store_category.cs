namespace AuFood.Models
{
    public partial class Store_category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; } = "fas fa-utensils";
        
        public ICollection<Store_category_store>? Store_category_store { get; set; }
    }
}
