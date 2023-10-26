namespace AuFood.Models
{
    public partial class State
    {

        public State()
        {
            Cities = new HashSet<City>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Uf { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<City> Cities { get; set; }
    }
}
