using BackendAPI.Models.Configuracion;
using BackendAPI.Models.Entidades;
using BackendAPI.Models.Repositorio.IRepositorio;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;

namespace BackendAPI.Models.Repositorio
{
    public class JsonPlaceHolderPostRepositorio : IJsonPlaceHolderPostRepositorio
    {
        private readonly ConfiguracionConexion _conexion;

        public JsonPlaceHolderPostRepositorio(IOptions<ConfiguracionConexion> conexion)
        {
            _conexion = conexion.Value;
        }

        public async Task<bool> GuardarPostsJson(List<Post> posts)
        {
            string query = "INSERT INTO Posts (UserId, Id, Title, Body) " +
                                "VALUES (@UserId, @Id, @Title, @Body)";
            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                foreach (var item in posts)
                {
                    SqlCommand cmd = new(query, conexion);

                    cmd.Parameters.AddWithValue("@UserId", item.UserId);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Title", item.Title);
                    cmd.Parameters.AddWithValue("@Body", item.Body);


                    await cmd.ExecuteNonQueryAsync();
                }

                return true;
            }
        }

        public async Task<List<Post>> ObtenerPost()
        {
            List<Post> lista = new();
            string query = "select UserId, Id, Title, Body from Posts";

            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new(query, conexion);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Post()
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Body = reader["Body"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<bool> EditarPost(Post posts)
        {
            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new("sp_EditarPost", conexion);

                cmd.Parameters.AddWithValue("@UserId", posts.UserId);
                cmd.Parameters.AddWithValue("@Id", posts.Id);
                cmd.Parameters.AddWithValue("@Title", posts.Title);
                cmd.Parameters.AddWithValue("@Body", posts.Body);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();
                if (filas_afectadas > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> EliminarPost(int id)
        {
            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new("sp_EliminarPost", conexion);

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

        public async Task<bool> GuardarPost(Post posts)
        {
            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new("sp_GuardarPost", conexion);

                cmd.Parameters.AddWithValue("@UserId", posts.UserId);
                cmd.Parameters.AddWithValue("@Id", posts.Id);
                cmd.Parameters.AddWithValue("@Title", posts.Title);
                cmd.Parameters.AddWithValue("@Body", posts.Body);
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
