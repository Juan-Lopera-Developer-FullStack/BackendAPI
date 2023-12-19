namespace BackendAPI.Models.Repository.IRepository
{
    public interface ILogPetitionRepository
    {
        void LogPetition(string method, string route, bool exito, string messageError);
    }
}
