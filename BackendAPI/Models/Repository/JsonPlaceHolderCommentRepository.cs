using BackendAPI.Models.Configuration;
using BackendAPI.Models.Entities;
using BackendAPI.Models.Repository.IRepository;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace BackendAPI.Models.Repository
{
    public class JsonPlaceHolderCommentRepository : IJsonPlaceHolderCommentRepository
    {
        private readonly ConfigConnection _connection;

        public JsonPlaceHolderCommentRepository(IOptions<ConfigConnection> connection)
        {
            _connection = connection.Value;
        }

        public async Task<bool> SaveCommentsJson(List<Comment> comment)
        {
            string query = "INSERT INTO Comments (Id, PostId, Name, Email, Body) " +
                                "VALUES (@Id, @PostId, @Name, @Email, @Body)";
            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                foreach (var item in comment)
                {
                    SqlCommand cmd = new(query, connection);

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

        public async Task<List<Comment>> GetComment()
        {
            List<Comment> lista = new();
            string query = "select Id, PostId, Name, Email, Body from Comments";

            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new(query, connection);
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

        public async Task<bool> EditComment(Comment comment)
        {
            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new("sp_EditComment", connection);

                cmd.Parameters.AddWithValue("@Id", comment.Id);
                cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                cmd.Parameters.AddWithValue("@Name", comment.Name);
                cmd.Parameters.AddWithValue("@Email", comment.Email);
                cmd.Parameters.AddWithValue("@Body", comment.Body);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();
                if (affectedRows > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> DeleteComment(int id)
        {
            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new("sp_DeleteComment", connection);

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

        public async Task<bool> SaveComment(Comment comment)
        {
            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new("sp_SaveComment", connection);

                cmd.Parameters.AddWithValue("@Id", comment.Id);
                cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                cmd.Parameters.AddWithValue("@Name", comment.Name);
                cmd.Parameters.AddWithValue("@Email", comment.Email);
                cmd.Parameters.AddWithValue("@Body", comment.Body);
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
