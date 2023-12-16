namespace BackendAPI.Models.Repositorio
{
    public interface ILogPeticionRepositorio
    {
        void LogPeticion(string metodo, string ruta, bool exitosa, string mensajeError);
    }
}
