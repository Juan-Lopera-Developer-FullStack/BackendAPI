using BackendAPI.Models.Configuracion;
using BackendAPI.Models.Entidades;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;

namespace BackendAPI.Models.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ConfiguracionConexion _conexion;

        public UsuarioRepositorio(IOptions<ConfiguracionConexion> conexion)
        {
            _conexion = conexion.Value;
        }

        public async Task<List<Usuario>> ObtenerUsuario()
        {
            List<Usuario> lista = new();
            string query = "select IdUsuario, Usuario, Contraseña from usuarios";

            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new(query, conexion);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                            User = reader["Usuario"].ToString(),
                            Password = reader["Contraseña"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<bool> GuardarUsuario(Usuario usuario)
        {
            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new("sp_GuardarUsuario", conexion);
                
                cmd.Parameters.AddWithValue("usuario", usuario.User);
                cmd.Parameters.AddWithValue("contraseña", usuario.Password);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();
                if(filas_afectadas > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> EditarUsuario(Usuario usuario)
        {
            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new("sp_EditarUsuario", conexion);

                cmd.Parameters.AddWithValue("idUsuario", usuario.IdUsuario);
                cmd.Parameters.AddWithValue("usuario", usuario.User);
                cmd.Parameters.AddWithValue("contraseña", usuario.Password);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();
                if (filas_afectadas > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new("sp_EliminarUsuario", conexion);

                cmd.Parameters.AddWithValue("idUsuario", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();
                if (filas_afectadas > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> ValidarUsuario(string usuario, string contraseña)
        {
            string query = "select Usuario, Contraseña from usuarios " +
                "where Usuario = @usuario and Contraseña = @Contraseña";

            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                SqlCommand cmd = new(query, conexion);
                cmd.Parameters.AddWithValue("@usuario", usuario.ToString());
                cmd.Parameters.AddWithValue("@contraseña", contraseña.ToString());

                try
                {
                    conexion.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    reader.Read();
                    Usuario oUsuario = new();
                    oUsuario.User = reader.GetString(0);
                    oUsuario.Password = reader.GetString(1);

                    reader.Close();
                    conexion.Close();

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
