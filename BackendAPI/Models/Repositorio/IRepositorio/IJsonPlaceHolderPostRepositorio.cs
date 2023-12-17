using BackendAPI.Models.Entidades;

namespace BackendAPI.Models.Repositorio.IRepositorio
{
    public interface IJsonPlaceHolderPostRepositorio
    {
        Task<List<Posts>> ObtenerPosts();
        Task<bool> GuardarPostsJson(List<Posts> posts);
        Task<bool> EditarPosts(Posts posts);
        Task<bool> EliminarPosts(int id);
    }
}
