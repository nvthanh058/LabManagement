namespace LabManagement.Models
{
    public class ProductTableView
    {
        public CaseCommunicate Content { get; set; } = new();
        public IQueryable<OrderFile>? imageitems { get; set; } = default!;

    }
}
