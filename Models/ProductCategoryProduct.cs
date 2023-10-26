namespace AuFood.Models
{
    public partial class ProductCategoryProduct
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int IdProductCategory { get; set; }
        public virtual Product? Product { get; set; }
        public virtual ProductCategory? ProductCategory { get; set; }
    }
}
