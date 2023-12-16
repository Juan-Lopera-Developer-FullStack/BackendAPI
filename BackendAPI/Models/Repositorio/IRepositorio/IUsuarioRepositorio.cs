using BackendAPI.Models.Entidades;

namespace BackendAPI.Models.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        Task<List<Usuario>> ObtenerUsuario();
        Task<bool> GuardarUsuario(Usuario usuario);
        Task<bool> EditarUsuario(Usuario usuario);
        Task<bool> EliminarUsuario(int id);
        Task<bool> ValidarUsuario(string usuario, string contraseña);
    }
}
