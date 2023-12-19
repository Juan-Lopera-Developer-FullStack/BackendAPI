using BackendAPI.Models.Entities;

namespace BackendAPI.Models.Repository.IRepository
{
    public interface IFamilyGroupRepository
    {
        List<FamilyGroup> GetFamilyGroup();
        bool SaveFamilyGroup(FamilyGroup familyGroup);
        bool EditFamilyGroup(FamilyGroup familyGroup);
        bool DeleteFamilyGroup(string cedula);
    }
}
