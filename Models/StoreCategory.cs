namespace AuFood.Models
{
    public partial class StoreCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; } = "fas fa-utensils";
        
        public ICollection<StoreCategoryStore>? StoreCategoryStore { get; set; }
    }
}
