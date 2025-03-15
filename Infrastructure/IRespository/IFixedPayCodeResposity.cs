
using LabManagement.Models;

namespace LabManagement.Infrastructure.IRespository
{
    public interface IFixedPayCodeResposity
    {
        Task<List<FixedPayCode>> GetAll(string EmplID,string Search);

        Task<FixedPayCode> Get(int id);

        Task<FixedPayCode> Add(FixedPayCode model);

        Task<FixedPayCode> Update(FixedPayCode model);

        Task<int> Delete(int id);
    }
}
