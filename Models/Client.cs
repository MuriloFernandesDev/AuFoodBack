namespace AuFood.Models
{
    public partial class Client
    {

        public Client()
        {
            ClientsLogin = new HashSet<ClientLogin>();
            Products = new HashSet<Product>();
            Client_ClientLogin = new HashSet<Client_ClientLogin>();
        }

        ///<summary>
        ///ID of Client Ex: 1
        ///</summary>
        public int Id { get; set; }

        ///<summary>
        ///Name of Client Ex: Client
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///Phone of Client Ex: 99999999999
        ///</summary>
        public string Phone { get; set; }

        ///<summary>
        ///Whatsapp of Client Ex: 99999999999
        ///</summary>
        public string Whatsapp { get; set; }

        ///<summary>
        ///Email of Client Ex: client@client
        ///</summary>
        public string Email { get; set; }

        ///<summary>
        ///Password of Client Ex: 123456
        ///</summary>
        public string? Logo { get; set; }

        public DateTime? Created { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<ClientLogin> ClientsLogin { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Client_ClientLogin> Client_ClientLogin { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
