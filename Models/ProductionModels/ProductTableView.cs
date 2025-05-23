using LabManagement.Models.SaleModels;

namespace LabManagement.Models.ProductionModels
{
    public class ProductTableView
    {
        public CaseCommunicate Content { get; set; } = new();
        public IQueryable<OrderFile>? imageitems { get; set; } = default!;

    }
}
