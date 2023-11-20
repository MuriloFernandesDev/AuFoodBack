using Newtonsoft.Json;

namespace AuFood.Models
{
    public partial class Client_ClientLogin
    {
        public int ClientId { get; set; }
        
        public int ClientLoginId { get; set; }


        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Client Client { get; set; } = null!;
        
        public ClientLogin ClientLogin { get; set; } = null!;
    }
}
