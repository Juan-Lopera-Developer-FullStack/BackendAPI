using BackendAPI.Models.Entidades;

namespace BackendAPI.Models.Repositorio.IRepositorio
{
    public interface IJsonPlaceHolderCommentRepositorio
    {
        Task<List<Comment>> ObtenerComment();
        Task<bool> GuardarComment(Comment comments);
        Task<bool> GuardarCommentsJson(List<Comment> comments);
        Task<bool> EditarComment(Comment comment);
        Task<bool> EliminarComment(int id);
    }
}
