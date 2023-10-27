namespace AuFood.Models
{
    public partial class ProductPrice
    {

        /// <summary>
        /// PrimaryKey
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Price of product
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Day of week of price
        /// </summary>
        public DayOfWeek DayWeek { get; set; }

        /// <summary>
        /// Id of product
        /// </summary>
        public int ProductId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Product? Product { get; set; }
    }
}
