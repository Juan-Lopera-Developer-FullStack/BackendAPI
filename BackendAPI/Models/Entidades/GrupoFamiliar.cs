using System.Text.Json.Serialization;

namespace BackendAPI.Models.Entidades
{
    public class GrupoFamiliar
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Genero { get; set; }
        public string Parentesco { get; set; }
        public int Edad { get; set; }
        [JsonIgnore]
        public bool MenorEdad { get; set; }
        public string? FechaNacimiento { get; set; }
        public int IdUsuario { get; set; }

    }
}
