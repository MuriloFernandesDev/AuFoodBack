using AuFood.Auxiliary;

namespace AuFood.Models
{
    public partial class Order
    {
        public Order()
        {
            Order_product = new HashSet<Order_product>();
        }

        public int Id { get; set; }
        
        public double Total_price { get; set; }

        public DateTime Date { get; set; }

        public PaymentMethod Payment_method { get; set; }

        public DeliveryMethod Delivery_method { get; set; }

        public OrderStatus? Status { get; set; } = OrderStatus.Preparing;

        public int Consumer_id { get; set; }

        public int Store_id { get; set; }

        public int Consumer_address_id { get; set; }

        public virtual Consumer? Consumer { get; set; }

        public virtual Consumer_address? Consumer_address { get; set; }

        public virtual Store? Store { get; set; }
        
        public ICollection<Order_product> Order_product { get; set; }
    }
}
