using BackendAPI.Models.Entidades;
using System.Data.SqlClient;
using System.Data;
using BackendAPI.Models.Configuracion;
using Microsoft.Extensions.Options;

namespace BackendAPI.Models.Repositorio
{
    public class GrupoFamiliarRepositorio : IGrupoFamiliarRepositorio
    {
        private readonly ConfiguracionConexion _conexion;

        public GrupoFamiliarRepositorio(IOptions<ConfiguracionConexion> conexion)
        {
            _conexion = conexion.Value;
        }

        public List<GrupoFamiliar> ObtenerGrupoFamiliar()
        {
            List<GrupoFamiliar> _lista = new();

            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListaGrupoFamiliar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while ( dr.Read())
                    {
                        _lista.Add(new GrupoFamiliar
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Usuario = dr["Usuario"].ToString(),
                            Cedula = dr["Cedula"].ToString(),
                            Nombres = dr["Nombres"].ToString(),
                            Apellidos = dr["Apellidos"].ToString(),
                            Genero = dr["Genero"].ToString(),
                            Parentesco = dr["Parentesco"].ToString(),
                            Edad = Convert.ToInt32(dr["Edad"]),
                            MenorEdad = Convert.ToBoolean(dr["MenorEdad"]),
                            FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Usuarios = new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                User = dr["Usuario"].ToString()
                            },
                        });
                    }
                }
            }

            return _lista;
        }

        public bool GuardarGrupoFamiliaro(GrupoFamiliar grupoFamiliar)
        {
            throw new NotImplementedException();
        }

        public bool EditarGrupoFamiliar(GrupoFamiliar grupoFamiliar)
        {
            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new("sp_EditarGrupoFamiliar", conexion);

                cmd.Parameters.AddWithValue("id", grupoFamiliar.Id);
                cmd.Parameters.AddWithValue("Usuario", grupoFamiliar.Usuario);
                cmd.Parameters.AddWithValue("Cedula", grupoFamiliar.Cedula);
                cmd.Parameters.AddWithValue("Nombres", grupoFamiliar.Nombres);
                cmd.Parameters.AddWithValue("Apellidos", grupoFamiliar.Apellidos);
                cmd.Parameters.AddWithValue("Genero", grupoFamiliar.Genero);
                cmd.Parameters.AddWithValue("Parentesco", grupoFamiliar.Parentesco);
                cmd.Parameters.AddWithValue("Edad", grupoFamiliar.Edad);
                cmd.Parameters.AddWithValue("MenorEdad", grupoFamiliar.MenorEdad);
                cmd.Parameters.AddWithValue("FechaNacimiento", grupoFamiliar.FechaNacimiento);
                cmd.Parameters.AddWithValue("IdUsuario", grupoFamiliar.IdUsuario);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = cmd.ExecuteNonQuery();
                if (filas_afectadas > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool EliminarGrupoFamiliar(string cedula)
        {
            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new("sp_EliminarGrupoFamiliar", conexion);

                cmd.Parameters.AddWithValue("Cedula", cedula);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = cmd.ExecuteNonQuery();
                if (filas_afectadas > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
