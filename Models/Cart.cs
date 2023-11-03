﻿using AuFood.Auxiliary;

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

        public virtual Consumer? Consumer { get; set; }

        public virtual ConsumerAddress? ConsumerAddress { get; set; }

        public virtual Store? Store { get; set; }
        
        public ICollection<CartProduct> CartProduct { get; set; }
    }
}
