using BackendAPI.Models.Configuracion;
using BackendAPI.Models.Repositorio.IRepositorio;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace BackendAPI.Models.Repositorio
{
    public class LogPeticionRepositorio : ILogPeticionRepositorio
    {
        private readonly ConfiguracionConexion _conexion;

        public LogPeticionRepositorio(IOptions<ConfiguracionConexion> conexion)
        {
            _conexion = conexion.Value;
        }
        public void LogPeticion(string metodo, string ruta, bool exitosa, string mensajeError)
        {
            using SqlConnection connection = new(_conexion.CadenaSQL);
            connection.Open();

            string query = "INSERT INTO LogPeticion (FechaHora, Metodo, Ruta, Exitosa, MensajeError) " +
                                "VALUES (@FechaHora, @Metodo, @Ruta, @Exitosa, @MensajeError)";

            using SqlCommand cmd = new(query, connection);
            cmd.Parameters.AddWithValue("@FechaHora", DateTime.Now);
            cmd.Parameters.AddWithValue("@Metodo", metodo);
            cmd.Parameters.AddWithValue("@Ruta", ruta);
            cmd.Parameters.AddWithValue("@Exitosa", exitosa);
            cmd.Parameters.AddWithValue("@MensajeError", mensajeError);

            cmd.ExecuteNonQuery();
        }
    }
}
