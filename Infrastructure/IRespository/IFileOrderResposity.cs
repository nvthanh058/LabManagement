using LabManagement.Models;

namespace LabManagement.Infrastructure.IRespository
{
    public interface IFileOrderResposity
    {
        Task<List<CaseOrder>> GetOrders(int RecID, string OrderID, string UserID, string Search, DateTime? FromDate, DateTime? ToDate, string LoginUserID);
        Task<CaseOrder> GetOrder(int RecID);
        Task<CaseOrder> SaveOrder(CaseOrder model);
        Task<int> DeleteOrder(int RecID);

        Task<List<OrderFile>> GetFiles(string OrderID, int FileType);
        Task<int> AddFile(string OrderID, string PicturePath, int FileType, string UserID);
        Task<int> SaveDownloadStatus(string OrderID, string UserID, int FileRecID, int DownloadStatus);
        Task<int> DeleteFile(int RecID);
        Task<List<CaseOrderLine>> GetOrderLines(string OrderID);
        Task<CaseOrderLine> GetOrderLine(int OrderID);
        Task<CaseOrderLine> SaveOrderLine(CaseOrderLine model);
        Task<int> DeleteOrderLine(int RecID);

    }
}
