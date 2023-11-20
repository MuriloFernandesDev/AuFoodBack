using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuFood.Models
{
    public partial class ClientLogin
    {
        public ClientLogin()
        {
            Client_ClientLogin = new HashSet<Client_ClientLogin>();
        }
        
        public int Id { get; set; }

        /// <summary>
        /// Name of Client Ex: Client
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Phone of Client Ex: 99999999999
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Email of Client Ex: client@client
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password of Client Ex: 123456
        /// </summary>
        [JsonIgnore]
        public byte[]? Password { get; set; } = null!;

        /// <summary>
        /// Password of Client Ex: 123456
        /// </summary>
        [NotMapped]
        public string? Pass { get; set; } = null!;

        public DateTime? Created { get; set; }

        /// <summary>
        /// Photo of Client Ex: https://www.google.com.br/photo.jpg
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Profile of Client Ex: Client, Admin, Deliveryman
        /// </summary>
        public string Profile { get; set; } = "Client";

        [NotMapped]
        public List<int> ListClientsId { get; set; }

        [NotMapped]
        public List<Client>? ListClients { get; set; }

        /// <summary>
        /// ID of Client Ex: 1
        /// </summary>
        public int ClientId { get; set; }


        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Client_ClientLogin> Client_ClientLogin { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Client? Client { get; set; }

    }
}
