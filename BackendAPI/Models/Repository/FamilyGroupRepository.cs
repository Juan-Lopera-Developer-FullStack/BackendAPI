using BackendAPI.Models.Entities;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;
using System.Reflection;
using BackendAPI.Models.Repository.IRepository;
using BackendAPI.Models.Configuration;

namespace BackendAPI.Models.Repository
{
    public class FamilyGroupRepository : IFamilyGroupRepository
    {
        private readonly ConfigConnection _connection;

        public FamilyGroupRepository(IOptions<ConfigConnection> connection)
        {
            _connection = connection.Value;
        }

        public List<FamilyGroup> GetFamilyGroup()
        {
            List<FamilyGroup> _lista = new();

            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new("sp_GetFamilyGroup", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _lista.Add(new FamilyGroup
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
                        FechaNacimiento = (dr["FechaNacimiento"]).ToString(),
                        IdUser = Convert.ToInt32(dr["IdUser"])
                    });
                }
            }

            return _lista;
        }

        public bool SaveFamilyGroup(FamilyGroup FamilyGroup)
        {
            try
            {
                    using (var connection = new SqlConnection(_connection.StringSQL))
                    {
                        connection.Open();
                        SqlCommand cmd = new("sp_SaveFamilyGroup", connection);
                        cmd.Parameters.AddWithValue("Usuario", FamilyGroup.Usuario);
                        cmd.Parameters.AddWithValue("Cedula", FamilyGroup.Cedula);
                        cmd.Parameters.AddWithValue("Nombres", FamilyGroup.Nombres);
                        cmd.Parameters.AddWithValue("Apellidos", FamilyGroup.Apellidos);
                        cmd.Parameters.AddWithValue("Genero", FamilyGroup.Genero);
                        cmd.Parameters.AddWithValue("Parentesco", FamilyGroup.Parentesco);
                        cmd.Parameters.AddWithValue("Edad", FamilyGroup.Edad);
                        cmd.Parameters.AddWithValue("MenorEdad", FamilyGroup.Edad > 17 ? FamilyGroup.MenorEdad : 1);
                        cmd.Parameters.AddWithValue("FechaNacimiento", FamilyGroup.FechaNacimiento != null && FamilyGroup.Edad < 18 ? FamilyGroup.FechaNacimiento : DBNull.Value);
                        cmd.Parameters.AddWithValue("IdUser", FamilyGroup.IdUser);
                        cmd.CommandType = CommandType.StoredProcedure;

                        int filas_afectadas = cmd.ExecuteNonQuery();

                        if (filas_afectadas > 0)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EditFamilyGroup(FamilyGroup FamilyGroup)
        {
            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new("sp_EditFamilyGroup", connection);

                cmd.Parameters.AddWithValue("Usuario", FamilyGroup.Usuario);
                cmd.Parameters.AddWithValue("Cedula", FamilyGroup.Cedula);
                cmd.Parameters.AddWithValue("Nombres", FamilyGroup.Nombres);
                cmd.Parameters.AddWithValue("Apellidos", FamilyGroup.Apellidos);
                cmd.Parameters.AddWithValue("Genero", FamilyGroup.Genero);
                cmd.Parameters.AddWithValue("Parentesco", FamilyGroup.Parentesco);
                cmd.Parameters.AddWithValue("Edad", FamilyGroup.Edad);
                cmd.Parameters.AddWithValue("MenorEdad", FamilyGroup.Edad > 17 ? FamilyGroup.MenorEdad : 1);
                cmd.Parameters.AddWithValue("FechaNacimiento", FamilyGroup.FechaNacimiento != null && FamilyGroup.Edad < 18 ? FamilyGroup.FechaNacimiento : DBNull.Value);
                cmd.Parameters.AddWithValue("IdUser", FamilyGroup.IdUser);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = cmd.ExecuteNonQuery();
                if (filas_afectadas > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool DeleteFamilyGroup(string cedula)
        {
            using (var connection = new SqlConnection(_connection.StringSQL))
            {
                connection.Open();
                SqlCommand cmd = new("sp_DeleteFamilyGroup", connection);

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
