using BackendAPI.Models.Entidades;

namespace BackendAPI.Models.Repositorio.IRepositorio
{
    public interface IJsonPlaceHolderPostRepositorio
    {
        Task<List<Post>> ObtenerPost();
        Task<bool> GuardarPostsJson(List<Post> posts);
        Task<bool> EditarPost(Post posts);
        Task<bool> EliminarPost(int id);
        Task<bool> GuardarPost(Post posts);
    }
}
