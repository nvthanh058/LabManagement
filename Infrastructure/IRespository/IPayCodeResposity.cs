using LabManagement.Models;

namespace LabManagement.Infrastructure.IRespository
{
    public interface IPayCodeResposity
    {
        Task<List<PayCodeInfo>> GetAll(int RecID, string PayCode,int PayType);

        Task<PayCodeInfo> Get(int id);

        Task<PayCodeInfo> Add(PayCodeInfo model);

        Task<PayCodeInfo> Update(PayCodeInfo model);

        Task <int> Delete(int id);
    }
}
