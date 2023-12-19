using BackendAPI.Models.Entities;
using BackendAPI.Models.Repository.IRepository;

namespace BackendAPI.Models.Repository
{
    public class BasicLoginRepository : IBasicLoginRepository
    {
        List<BasicLogin> loginBasics = new List<BasicLogin>()
        {
            new BasicLogin() { Username = "jcloperachica@gmail.com", Password = "1234" }
        };

        public bool IsUser(string username, string password) =>
            loginBasics.Where(e => e.Username == username && e.Password == password).Count() > 0;
    }
}
