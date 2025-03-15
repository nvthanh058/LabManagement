using LabManagement.Models;

namespace LabManagement.Infrastructure.IRespository
{
    public interface IEmployeeResposity
    {
        Task<List<Employee>> GetAll(string EmplID, string Search);
        Task<List<Department>> GetAllDepartment(string DeptID, string Search);
        Task<Employee> Add(Employee model);
        Task<Employee> Update(Employee model);
        Task<int> Delete(int Id);

    }
}
