namespace BackendAPI.Models.Entities
{
    public class LogPetition
    {
        public int IdLog { get; set; }
        public DateTime DataHour { get; set; }
        public string Method { get; set; }
        public string Route { get; set; }
        public bool Exito { get; set; }
        public string? MessageError { get; set; }
    }
}
