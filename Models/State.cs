namespace AuFood.Models
{
    public partial class State
    {

        public State()
        {
            City = new HashSet<City>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Uf { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<City> City { get; set; }
    }
}
