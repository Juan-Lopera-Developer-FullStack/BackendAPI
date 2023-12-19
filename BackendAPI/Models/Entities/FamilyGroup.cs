using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackendAPI.Models.Entities
{
    public class FamilyGroup
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required(ErrorMessage = "Usuario es requerido")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Cedula es requerido")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "Nombres es requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Apellidos es requerido")]
        public string Apellidos { get; set; }
        public string Genero { get; set; }
        public string Parentesco { get; set; }
        [Required(ErrorMessage = "Edad es requerido")]
        public int Edad { get; set; }
        [JsonIgnore]
        public bool MenorEdad { get; set; }
        public string? FechaNacimiento { get; set; }
        public int IdUser { get; set; }

    }
}
