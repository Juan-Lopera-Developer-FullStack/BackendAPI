namespace BackendAPI.Models.Entidades
{
    public class LogPeticion
    {
        public int IdLog { get; set; }
        public DateTime FechaHora { get; set; }
        public string Metodo { get; set; }
        public string Ruta { get; set; }
        public bool Exitosa { get; set; }
        public string? MensajeError { get; set; }
    }
}
