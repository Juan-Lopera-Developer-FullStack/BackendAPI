namespace BackendAPI.Models.Repositorio.IRepositorio
{
    public interface ILogPeticionRepositorio
    {
        void LogPeticion(string metodo, string ruta, bool exitosa, string mensajeError);
    }
}
