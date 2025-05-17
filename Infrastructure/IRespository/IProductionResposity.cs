using LabManagement.Models;

namespace LabManagement.Infrastructure.IRespository
{
    public interface IProductionResposity
    {
        Task<List<ProductLine>> GetAllProdLine(int RecID, string LineID, string LineGroup, string Search);
        Task<ProductLine> GetProdLine(int RecID);
        Task<ProductLine> AddProdLine(ProductLine model);
        Task<ProductLine> UpdateProdLine(ProductLine model);
        Task<int> DeleteProdLine(int RecID);

        Task<int> SaveAssignProductTask(ProductionTask model);

        Task<int> DeleteProductTask(ProductionTask model);
       
        Task<List<ProductionTask>> GetProductTasks(int RecID, string TaskRefID, string EmplRefID,string TaskID,string LineID);
        Task<List<ProductionTask>> GetProductTasksReports(string EmplRefID, DateTime? fromDate, DateTime? ToDate);

        Task<List<ProductionTask>> GetLatestProductTasks();

        Task<List<ProductionTask>> GetNotifyProductTasks(string EmplRefID);

        Task<ProductionTask> GetProductionTaskStatus(string TaskRefID, string EmplRefID);

        Task<ProdTable> GetProductOrder(string TRANSID,string CaseNo);
        Task<int> SaveTaskMessage(string TaskID,string FromUser,string ToUser,string MessageContent,string FilePath);

        Task<List<TaskMessage>> GetTaskMessages(int RecID,string TaskID);

        Task<List<TaskMessage>> GetTaskMessagesNotify(string UserID);

        Task<int> SaveProductTaskTrans(string TaskID, int ProdStatus);

        Task<CaseTracking> GetCasesTrackingBySalesID(string SalesID);
        Task<CaseTracking> GetCasesTrackingByID(int RecID);
        Task<List<CaseTracking>> GetCasesTracking(int RecID, DateTime? FromDate,DateTime? ToDate,string Search,string LabID);
        Task<CaseTracking> SaveCaseTracking(CaseTracking model);
        Task<int> DeleteCaseTracking(int RecID);
        Task<List<CaseTrackingTask>> GetCasesTrackingTask(int RecID, string TransID);
        Task<CaseTrackingTask> SaveCaseTrackingTask(CaseTrackingTask model);
        Task<int> DeleteCaseTrackingTask(int RecID);
        Task<List<CaseCommunicate>> GetCaseCommunicate(int RecID,string SalesID, string TransID);
        Task<CaseCommunicate> SaveCaseCommunicate(CaseCommunicate model);
        Task<int> DeleteCaseCommunicate(int RecID);

        Task<List<CaseStatus>> GetCaseStatus(int StatusGroup);


    }
}
