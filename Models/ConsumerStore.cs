namespace AuFood.Models
{
    public partial class ConsumerStore
    {
        public int StoreId { get; set; }
        public int ConsumerId { get; set; }

        public Consumer Consumer { get; set; } = null!;
        public Store Store { get; set; } = null!;
    }
}
