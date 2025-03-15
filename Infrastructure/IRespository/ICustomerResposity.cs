using LabManagement.Models;

namespace LabManagement.Infrastructure.IRespository
{
    public interface ICustomerResposity
    {
        Task<List<Customer>> GetAll(string CustomerID, string Search);       
        Task<Customer> Add(Customer model);
        Task<Customer> Update(Customer model);
        Task<int> Delete(int Id);

    }
}
