namespace BackendAPI.Models.Repository.IRepository
{
    public interface IBasicLoginRepository
    {
        public bool IsUser(string username, string password);
    }
}
