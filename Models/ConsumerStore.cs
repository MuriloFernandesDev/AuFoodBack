﻿using Newtonsoft.Json;

namespace AuFood.Models
{
    public partial class ProductStore
    {
        public int StoreId { get; set; }
        
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public Store Store { get; set; } = null!;
    }
}
