using BackendAPI.Models.Entidades;

namespace BackendAPI.Servicios.IServicios
{
    public interface IJsonPlaceHolderPosts
    {
        Task<List<Posts>> ObtenerPost();
    }
}
