using BackendAPI.Models.Configuration;
using BackendAPI.Models.Repository.IRepository;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace BackendAPI.Models.Repository
{
    public class LogPetitionRepository : ILogPetitionRepository
    {
        private readonly ConfigConnection _connection;

        public LogPetitionRepository(IOptions<ConfigConnection> connection)
        {
            _connection = connection.Value;
        }
        public void LogPetition(string method, string route, bool exito, string messageError)
        {
            using SqlConnection connection = new(_connection.StringSQL);
            connection.Open();

            string query = "INSERT INTO logPetition (dataHour, method, route, exito, messageError) " +
                                "VALUES (@dataHour, @method, @route, @exito, @messageError)";

            using SqlCommand cmd = new(query, connection);
            cmd.Parameters.AddWithValue("@dataHour", DateTime.Now);
            cmd.Parameters.AddWithValue("@method", method);
            cmd.Parameters.AddWithValue("@route", route);
            cmd.Parameters.AddWithValue("@exito", exito);
            cmd.Parameters.AddWithValue("@messageError", messageError);

            cmd.ExecuteNonQuery();
        }
    }
}
