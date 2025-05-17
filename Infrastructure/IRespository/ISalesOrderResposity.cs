using LabManagement.Models;

namespace LabManagement.Infrastructure.IRespository
{
    public interface ISalesOrderResposity
    {
        string DateFormat { get; }
        Task<List<SalesTable>> GetOrders(int RecID, string SalesID,string CustomerID, string Search, 
            DateTime ? FromDate, DateTime ? ToDate, string LabID);
        Task<SalesTable> GetOrderByRecID(int RecID);
        Task<SalesTable> GetOrderBySalesID(string SalesID);

        Task<SalesTable> SaveOrder(SalesTable model);
        Task<int> DeleteOrder(int RecID);

        Task<List<SalesLine>> GetOrderLines(string SalesID);
        Task<SalesLine> GetOrderLine(int RecID);
        Task<SalesLine> SaveOrderLine(SalesLine model);

        Task<ProdTable> SaveProdOrder(SalesLine SL);

        Task<ProdTable> UpdateProdOrder(ProdTable model);

        Task<List<ProdTable>> GetProdOrders(int RecID,string SalesID,string LabID, string FromDate,string ToDate,string Search);

        Task<int> DeleteOrderLine(int RecID);

        Task<int> DeleteProdOrder(int RecID);
       
        Task<List<OrderFile>> GetFiles(string OrderID,int FileType);
        Task<int> AddFile(string OrderID,string PicturePath,int FileType);
        Task<int> DeleteFile(int RecID);
        Task<SequenceInfo> GetSequenceNum(string SeqID);

        Task<List<AssignmentInfo>> GetAssignment();
    }
}
