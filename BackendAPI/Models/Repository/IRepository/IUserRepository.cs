using BackendAPI.Models.Entities;

namespace BackendAPI.Models.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUser();
        Task<bool> SaveUser(User user);
        Task<bool> EditUser(User user);
        Task<bool> DeleteUser(int id);
        Task<bool> ValidateUser(string userName, string password);
    }
}
