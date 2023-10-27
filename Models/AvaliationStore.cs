namespace AuFood.Models
{
    public partial class AvaliationStore
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        //public string UserId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public virtual Store Store { get; set; }
    }
}
