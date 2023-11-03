using AuFood.Auxiliary;

namespace AuFood.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartProduct = new HashSet<CartProduct>();
        }

        public int Id { get; set; }
        
        public double TotalPrice { get; set; }

        public DateTime Date { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public int ConsumerId { get; set; }

        public int StoreId { get; set; }
        public int ConsumerAddressId { get; set; }

        public virtual Consumer Consumer { get; set; } = null!;
        public virtual ConsumerAddress ConsumerAddress { get; set; } = null!;

        public virtual Store Store { get; set; } = null!;
        
        public ICollection<CartProduct> CartProduct { get; set; } = null!;
    }
}
