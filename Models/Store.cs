﻿namespace AuFood.Models
{
    public partial class Store
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }
        
        public string Description { get; set; }

        public string Whatsapp { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Street { get; set; }

        public string NumberAddress { get; set; }
        
        public string Address { get; set; }

        public int Cep { get; set; }

        public string Cnpj { get; set; }

        public string InstagramUrl { get; set; }

        public string FacebookUrl { get; set; }

        public string BackgroundImage { get; set; }

        //public double TimeDelivery { get; set; }

        public int CityId { get; set; }

        public virtual City? City { get; set; }
       
        public ICollection<StoreCategoryMapping> StoreCategoryStores { get; set; }
    }
}
