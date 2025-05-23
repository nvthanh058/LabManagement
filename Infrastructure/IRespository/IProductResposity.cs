using LabManagement.Models;
using LabManagement.Models.Commons;

namespace LabManagement.Infrastructure.IRespository
{
    public interface IProductResposity
    {
        Task<List<Product>> GetAll(int RecID, string ItemID, string Search);
        Task<Product> Get(int RecID);
        Task<Product> Save(Product model);       
        Task<int> Delete(int RecID);

        Task<List<ProductType>> GetProductTypes();
        Task<List<ProductGroup>> GetProductGroups();
        Task<List<TablePrice>> GetAllPrice(int RecID, string PriceID, string ItemID, string Search);
        Task<TablePrice> GetPrice(int RecID);
        Task<TablePrice> SavePrice(TablePrice model);
        Task<int> DeletePrice(int RecID);

        Task<List<TablePriceMaster>> GetPriceListMaster(int RecID, string PriceID, string Search);        
        Task<TablePriceMaster> SavePriceListMaster(TablePriceMaster model);
        Task<int> DeletePriceListMaster(int RecID);

        Task<List<ProductUnit>> GetAllUnits(int RecID, string UnitID, string Search);
        Task<ProductUnit> SaveUnit(ProductUnit model);
        Task<int> DeleteUnit(int RecID);

        Task<ProductGroup> SaveGroup(ProductGroup model);
        Task<int> DeleteGroup(int RecID);
    }
}
