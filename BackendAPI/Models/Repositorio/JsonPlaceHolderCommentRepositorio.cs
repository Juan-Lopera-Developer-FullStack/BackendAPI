using BackendAPI.Models.Configuracion;
using BackendAPI.Models.Entidades;
using BackendAPI.Models.Repositorio.IRepositorio;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace BackendAPI.Models.Repositorio
{
    public class JsonPlaceHolderCommentRepositorio : IJsonPlaceHolderCommentRepositorio
    {
        private readonly ConfiguracionConexion _conexion;

        public JsonPlaceHolderCommentRepositorio(IOptions<ConfiguracionConexion> conexion)
        {
            _conexion = conexion.Value;
        }

        public async Task<bool> GuardarCommentsJson(List<Comment> comments)
        {
            string query = "INSERT INTO Comments (Id, PostId, Name, Email, Body) " +
                                "VALUES (@Id, @PostId, @Name, @Email, @Body)";
            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                foreach (var item in comments)
                {
                    SqlCommand cmd = new(query, conexion);

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@PostId", item.PostId);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    cmd.Parameters.AddWithValue("@Email", item.Email);
                    cmd.Parameters.AddWithValue("@Body", item.Body);


                    await cmd.ExecuteNonQueryAsync();
                }

                return true;
            }
        }

        public async Task<List<Comment>> ObtenerComment()
        {
            List<Comment> lista = new();
            string query = "select Id, PostId, Name, Email, Body from Comments";

            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new(query, conexion);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Comment()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            PostId = Convert.ToInt32(reader["PostId"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Body = reader["Body"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<bool> EditarComment(Comment comment)
        {
            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new("sp_EditarComment", conexion);

                cmd.Parameters.AddWithValue("@Id", comment.Id);
                cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                cmd.Parameters.AddWithValue("@Name", comment.Name);
                cmd.Parameters.AddWithValue("@Email", comment.Email);
                cmd.Parameters.AddWithValue("@Body", comment.Body);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();
                if (filas_afectadas > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> EliminarComment(int id)
        {
            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new("sp_EliminarComment", conexion);

                cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();
                if (filas_afectadas > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> GuardarComment(Comment comment)
        {
            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new("sp_GuardarComment", conexion);

                cmd.Parameters.AddWithValue("@Id", comment.Id);
                cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                cmd.Parameters.AddWithValue("@Name", comment.Name);
                cmd.Parameters.AddWithValue("@Email", comment.Email);
                cmd.Parameters.AddWithValue("@Body", comment.Body);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();
                if (filas_afectadas > 0)
                {
                    return true;
                }
                return false;
            }
        }

    }
}
