using BackendAPI.Models.Entidades;

namespace BackendAPI.Servicios.IServicios
{
    public interface IJsonPlaceHolder
    {
        Task<List<Post>> ObtenerPost();
        Task<List<Comment>> ObtenerComment();
    }
}
