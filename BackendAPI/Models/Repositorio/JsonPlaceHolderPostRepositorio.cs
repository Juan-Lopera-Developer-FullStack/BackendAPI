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

        public async Task<bool> GuardarPostsJson(List<Posts> posts)
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

            public Task<List<Posts>> ObtenerPosts()
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditarPosts(Posts posts)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarPosts(int id)
        {
            throw new NotImplementedException();
        }
    }
}
