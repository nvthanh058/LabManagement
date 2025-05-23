using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using LabManagement.Components.Infrastructure.IRespository;
using LabManagement.Infrastructure.IRespository;
using LabManagement.Models;
using LabManagement.Models.ProductionModels;
using LabManagement.Models.SaleModels;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LabManagement.Components.Infrastructure.Respository
{
    public class DashboardResposity : IDashboardResposity
    {
        private readonly IDapperServices _services;
        
        public DashboardResposity(IDapperServices services)
        {
            _services = services;
        }

        public async Task<List<DueIn2DaysCases>> GetDueDayCases(string FromDate, string ToDate)
        {
            var query = @"DASHBOARD_GetDueDayCases";

            var lst = new List<DueIn2DaysCases>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@FromDate", FromDate);
                dbParams.Add("@ToDate", ToDate);

                lst = Task.FromResult(_services.GetAll<DueIn2DaysCases>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<List<IncomingModel>> GetIncomingCases(string FromDate, string ToDate)
        {
            var query = @"DASHBOARD_GetIncomingCases";

            var lst = new List<IncomingModel>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@FromDate", FromDate);
                dbParams.Add("@ToDate", ToDate);
              
                lst = Task.FromResult(_services.GetAll<IncomingModel>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<List<LateCasesModel>> GetLateCases(string FromDate, string ToDate)
        {
            var query = @"DASHBOARD_GetLateCases";

            var lst = new List<LateCasesModel>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@FromDate", FromDate);
                dbParams.Add("@ToDate", ToDate);

                lst = Task.FromResult(_services.GetAll<LateCasesModel>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<List<LocationModel>> GetLocationCases(string FromDate, string ToDate)
        {
            var query = @"DASHBOARD_GetLocationCases";

            var lst = new List<LocationModel>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@FromDate", FromDate);
                dbParams.Add("@ToDate", ToDate);

                lst = Task.FromResult(_services.GetAll<LocationModel>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<ReportPeriod> GetReportPeriod()
        {
            var query = @"DASHBOARD_GetReportPeriod";
            var model = new ReportPeriod();
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", 0);

                model = Task.FromResult(_services.Get<ReportPeriod>(query, null, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<List<OutgoingModel>> GetShipCases(string FromDate, string ToDate)
        {
            var query = @"DASHBOARD_GetShipCases";

            var lst = new List<OutgoingModel>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@FromDate", FromDate);
                dbParams.Add("@ToDate", ToDate);

                lst = Task.FromResult(_services.GetAll<OutgoingModel>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<int> ImportReportCase(ReportCasesInfo model)
        {            
            try
            {
                var dbParams = new DynamicParameters();

              
                dbParams.Add("@RecID", model.RecID);
                dbParams.Add("@Invoice", model.Invoice);
                dbParams.Add("@Company", model.Company);
                dbParams.Add("@Doctor", model.Doctor);
                dbParams.Add("@Patient", model.Patient);
                dbParams.Add("@Pan", model.Pan);
                dbParams.Add("@Products", model.Products);
                dbParams.Add("@Units", model.Units);
                dbParams.Add("@OrderedDate", model.OrderedDate);
                dbParams.Add("@DueDate", model.DueDate);
                dbParams.Add("@Appt", model.Appt);
                dbParams.Add("@Dept", model.Dept);
                dbParams.Add("@UserID", model.UserID);
                dbParams.Add("@Status", model.Status);


                var query = @"LAB_SaveReportCases";
                var res =  Task.FromResult(_services.ExcuteScalerObject<ReportCasesInfo>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;

                if (res != null)
                    model.RecID = Int32.Parse(res.ToString());
            }
            catch (Exception ex) { }

            return model.RecID;
        }
    }
}
