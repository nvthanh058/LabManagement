using LabManagement.Models;

namespace LabManagement.Infrastructure.IRespository
{
    public interface ISystemResposity
    {
     
        Task<List<LabInfo>> GetAllLabs(int RecID, string DATAAREAID, string Search);
        Task<LabInfo> SaveLab(LabInfo model);
        Task<int> DeleteLab(int RecID);
    }
}
