using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackendAPI.Models.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        
        public string User { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public List<GrupoFamiliar> GrupoFamiliars { get; set; }
    }
}