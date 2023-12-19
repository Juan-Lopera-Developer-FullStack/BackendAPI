using BackendAPI.Models.Entities;
using BackendAPI.Models.Repository.IRepository;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;
using BackendAPI.Models.Configuration;

namespace BackendAPI.Models.Repository
{
    public class JsonPlaceHolderPostRepository : IJsonPlaceHolderPostRepository
    {
        private readonly ConfigConnection _connection;

        public JsonPlaceHolderPostRepository(IOptions<ConfigConnection> connection)
        {
            _connection = connection.Value;
        }

        public async Task<bool> SavePostsJson(List<Post> posts)
        {
            string query = "INSERT INTO Posts (UserId, Id, Title, Body) " +
                                "VALUES (@UserId, @Id, @Title, @Body)";
            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                foreach (var item in posts)
                {
                    SqlCommand cmd = new(query, connection);

                    cmd.Parameters.AddWithValue("@UserId", item.UserId);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Title", item.Title);
                    cmd.Parameters.AddWithValue("@Body", item.Body);


                    await cmd.ExecuteNonQueryAsync();
                }

                return true;
            }
        }

        public async Task<List<Post>> GetPost()
        {
            List<Post> lista = new();
            string query = "select UserId, Id, Title, Body from Posts";

            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new(query, connection);
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

        public async Task<bool> EditPost(Post posts)
        {
            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new("sp_EditPost", connection);

                cmd.Parameters.AddWithValue("@UserId", posts.UserId);
                cmd.Parameters.AddWithValue("@Id", posts.Id);
                cmd.Parameters.AddWithValue("@Title", posts.Title);
                cmd.Parameters.AddWithValue("@Body", posts.Body);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();
                if (affectedRows > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> DeletePost(int id)
        {
            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new("sp_DeletePost", connection);

                cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();
                if (affectedRows > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> SavePost(Post posts)
        {
            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new("sp_SavePost", connection);

                cmd.Parameters.AddWithValue("@UserId", posts.UserId);
                cmd.Parameters.AddWithValue("@Id", posts.Id);
                cmd.Parameters.AddWithValue("@Title", posts.Title);
                cmd.Parameters.AddWithValue("@Body", posts.Body);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();
                if (affectedRows > 0)
                {
                    return true;
                }
                return false;
            }
        }

    }
}
