using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackendAPI.Models.Entities
{
    public class User
    {
        public int IdUser { get; set; }
        
        public string UserName { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public List<FamilyGroup> GrupoFamiliars { get; set; }
    }
}