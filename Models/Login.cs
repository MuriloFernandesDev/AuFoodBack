using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuFood.Models
{
    public partial class Login
    {
        public Login()
        {
            Store_login = new HashSet<Store_login>();
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

        /// <summary>
        /// Password of Client Ex: 123456
        /// </summary>
        [NotMapped]
        public List<int>? List_store_id { get; set; }

        [NotMapped]
        public string? accessToken { get; set; }

        public DateTime? Created { get; set; }

        /// <summary>
        /// Photo of Client Ex: https://www.google.com.br/photo.jpg
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Profile of Client Ex: Client, Admin, Deliveryman
        /// </summary>
        public string Profile { get; set; } = "Client";

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Store_login> Store_login { get; set; }
    }
}
