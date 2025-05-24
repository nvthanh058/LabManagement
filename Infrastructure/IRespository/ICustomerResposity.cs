using LabManagement.Models.SaleModels;

namespace LabManagement.Infrastructure.IRespository
{
    public interface ICustomerResposity
    {
        Task<List<Customer>> GetAll(string CustomerID, string Search);
        Task<Customer> GetByRecID(int RecID);

        Task<Customer> GetByCode(string CustomerCode);
        Task<Customer> Add(Customer model);
        Task<Customer> Update(Customer model);
        Task<int> Delete(int Id);
        Task<Customer> SaveCustomer(Customer model);
    }
}
