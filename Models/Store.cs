namespace AuFood.Models
{
    public partial class Store
    {
        public Store()
        {
            AvaliationsStories = new HashSet<AvaliationStore>();
            Cart = new HashSet<Cart>();
            StoreCategoryStores = new HashSet<StoreCategoryMapping>();
            ConsumerStore = new HashSet<ConsumerStore>();
        }
        
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

        public int ZipCode { get; set; }

        public string Cnpj { get; set; }

        public string InstagramUrl { get; set; }

        public string FacebookUrl { get; set; }

        public string BackgroundImage { get; set; }

        public string ColorPrimary { get; set; }

        public string ColorSecondary { get; set; }

        public string ColorBackground { get; set; }

        //public double TimeDelivery { get; set; }

        public int CityId { get; set; }

        public virtual City? City { get; set; }
       
        public ICollection<StoreCategoryMapping> StoreCategoryStores { get; set; }
        
        public ICollection<AvaliationStore> AvaliationsStories { get; set; }
        public ICollection<Cart> Cart { get; set; }

        public ICollection<ConsumerStore> ConsumerStore { get; set; } = null!;
    }
}
