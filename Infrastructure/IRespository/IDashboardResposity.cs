using LabManagement.Models;

namespace LabManagement.Components.Infrastructure.IRespository
{
    public interface IDashboardResposity
    {
        Task<List<IncomingModel>> GetIncomingCases(string FromDate, string ToDate);
        Task<List<OutgoingModel>> GetShipCases(string FromDate, string ToDate);
        Task<List<DueIn2DaysCases>> GetDueDayCases(string FromDate, string ToDate);
        Task<List<LateCasesModel>> GetLateCases(string FromDate, string ToDate);

        Task<List<LocationModel>> GetLocationCases(string FromDate, string ToDate);

        Task<int> ImportReportCase(ReportCasesInfo model);
        Task<ReportPeriod> GetReportPeriod();
    }
}
