using BackendAPI.Models.Entities;

namespace BackendAPI.Models.Repository.IRepository
{
    public interface IJsonPlaceHolderCommentRepository
    {
        Task<List<Comment>> GetComment();
        Task<bool> SaveComment(Comment comment);
        Task<bool> SaveCommentsJson(List<Comment> comment);
        Task<bool> EditComment(Comment comment);
        Task<bool> DeleteComment(int id);
    }
}
