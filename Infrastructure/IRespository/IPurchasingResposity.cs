using LabManagement.Models.PurchasingModels;
using LabManagement.Models.SaleModels;

namespace LabManagement.Infrastructure.IRespository
{
    public interface IPurchasingResposity
    {
        Task<List<Vendor>> GetVendors(string VendorID, string Search);       
        Task<Vendor> SaveVendor(Vendor model);        
        Task<int> DeleteVendor(int Id);

    }
}
