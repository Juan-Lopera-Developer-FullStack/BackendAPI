using BackendAPI.Models.Entidades;

namespace BackendAPI.Models.Repositorio.IRepositorio
{
    public interface IJsonPlaceHolderPostRepositorio
    {
        Task<List<Posts>> ObtenerPost();
        Task<bool> GuardarPostsJson(List<Posts> posts);
        Task<bool> EditarPost(Posts posts);
        Task<bool> EliminarPost(int id);
        Task<bool> GuardarPost(Posts posts);
    }
}
