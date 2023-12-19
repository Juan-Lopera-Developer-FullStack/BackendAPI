using BackendAPI.Models.Entities;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;
using BackendAPI.Models.Repository.IRepository;
using BackendAPI.Models.Configuration;

namespace BackendAPI.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ConfigConnection _connection;

        public UserRepository(IOptions<ConfigConnection> connection)
        {
            _connection = connection.Value;
        }

        public async Task<List<User>> GetUser()
        {
            List<User> lista = new();
            string query = "select idUser, userName, password from users";

            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new(query, connection);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new User()
                        {
                            IdUser = Convert.ToInt32(reader["idUser"]),
                            UserName = reader["userName"].ToString(),
                            Password = reader["password"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<bool> SaveUser(User user)
        {
            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new("sp_SaveUser", connection);
                
                cmd.Parameters.AddWithValue("usuario", user.UserName);
                cmd.Parameters.AddWithValue("contraseña", user.Password);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();
                if(affectedRows > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> EditUser(User user)
        {
            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new("sp_EditUser", connection);

                cmd.Parameters.AddWithValue("idUser", user.IdUser);
                cmd.Parameters.AddWithValue("user", user.UserName);
                cmd.Parameters.AddWithValue("password", user.Password);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();
                if (affectedRows > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new("sp_DeleteUser", connection);

                cmd.Parameters.AddWithValue("idUser", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();
                if (affectedRows > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> ValidateUser(string userName, string password)
        {
            string query = "select username, password from users " +
                "where username = @username and password = @password";

            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                SqlCommand cmd = new(query, connection);
                cmd.Parameters.AddWithValue("@username", userName.ToString());
                cmd.Parameters.AddWithValue("@password", password.ToString());

                try
                {
                    connection.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    reader.Read();
                    User oUser = new();
                    oUser.UserName = reader.GetString(0);
                    oUser.Password = reader.GetString(1);

                    reader.Close();
                    connection.Close();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
