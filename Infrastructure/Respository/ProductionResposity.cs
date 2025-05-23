using Azure.Messaging;
using Dapper;
using LabManagement.Infrastructure.IRespository;
using LabManagement.Models;
using LabManagement.Models.ProductionModels;
using LabManagement.Models.SaleModels;
using System.Data;
using System.Reflection;

namespace LabManagement.Infrastructure.Respository
{
    public class ProductionResposity : IProductionResposity
    {
        private readonly IDapperServices _services;
        public ProductionResposity(IDapperServices services)
        {
            _services = services;
        }

        public async Task<int> DeleteProdLine(int RecID)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM LAB_ProductLines WHERE RecID=@RecID";
               
                dbParams.Add("@RecID", RecID);
             

                res = Task.FromResult(_services.ExcuteScaler<ProductLine>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<List<ProductLine>> GetAllProdLine(int RecID, string LineID,string LineGroup, string Search)
        {

            var query = @"Select * FROM LAB_ProductLines where 1=1";
            query += " AND (@RecID=0 OR RecID=@RecID)";
            query += " AND (@LineID='' OR LineID=@LineID)";
            query += " AND (@LineGroup='' OR LineGroup=@LineGroup)";
            query += " AND (@Search='' OR LineName=@Search)";

            var lst = new List<ProductLine>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@LineID", LineID);
                dbParams.Add("@Search", Search);
                dbParams.Add("@LineGroup", LineGroup);

                lst = Task.FromResult(_services.GetAll<ProductLine>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<ProductLine> GetProdLine(int RecID)
        {
            var query = @"Select * FROM LAB_ProductLines where RecID=@RecID";            
            var model = new ProductLine();
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);                

                model = Task.FromResult(_services.Get<ProductLine>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<ProductLine> AddProdLine(ProductLine model)
        {
            try
            {
                model.DATAAREAID = _services.DATAAREAID();

                var dbParams = new DynamicParameters();
                Type classType = typeof(ProductLine);

                PropertyInfo[] propertyInfos = classType.GetProperties();

                var fieldList = "";
                var fieldData = "";
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    var type = propertyInfo.PropertyType.Name;
                    var fieldName = propertyInfo.Name;

                    var attribute = (ModelAttribute)propertyInfo.GetCustomAttribute(typeof(ModelAttribute));

                    var fieldDesc = "";
                    if (attribute != null)
                    {
                        fieldDesc = attribute.Description;
                    }

                    if (!fieldDesc.Equals("NotTableField"))
                    {
                        if (fieldName != "RecID")
                        {
                            fieldList += "" + fieldName + ",";
                            fieldData += "@" + fieldName + ",";
                        }
                        dbParams.Add("@" + fieldName, propertyInfo.GetValue(model));
                    }

                }
                if(model.RecID==0)
                {
                    var query = "INSERT INTO LAB_ProductLines(" + fieldList.Trim(',') + ",LineID)  OUTPUT INSERTED.RecID VALUES(" + fieldData.Trim(',') + ",NewID())";
                    model.RecID = Task.FromResult(_services.ExcuteScaler<ProductLine>(query, dbParams, commandType: CommandType.Text)).Result;
                }
                

            }
            catch (Exception ex) { }

            return model;
        }

        public async Task<ProductLine> UpdateProdLine(ProductLine model)
        {
            try
            {
                var dbParams = new DynamicParameters();
                Type classType = typeof(ProductLine);

                PropertyInfo[] propertyInfos = classType.GetProperties();

                var updateData = "";
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    var type = propertyInfo.PropertyType.Name;
                    var fieldName = propertyInfo.Name;

                    var attribute = (ModelAttribute)propertyInfo.GetCustomAttribute(typeof(ModelAttribute));

                    var fieldDesc = "";
                    if (attribute != null)
                    {
                        fieldDesc = attribute.Description;
                    }

                    if (!fieldDesc.Equals("NotTableField"))
                    {
                        if (fieldName != "RecID")
                        {
                            updateData += fieldName + "=@" + fieldName + ",";
                        }

                        dbParams.Add("@" + fieldName, propertyInfo.GetValue(model));
                    }
                }

                var query = "UPDATE LAB_ProductLines SET " + updateData.Trim(',') + " WHERE RecID=@RecID";

                var res = Task.FromResult(_services.ExcuteScaler<ProductLine>(query, dbParams, commandType: CommandType.Text)).Result;

            }
            catch (Exception ex) { }

            return model;
        }

        public async Task<int> SaveAssignProductTask(ProductionTask model)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();

                model.DATAAREAID = _services!.DATAAREAID();

                dbParams.Add("@RecID", model.RecID);
                dbParams.Add("@TaskRefID", model.TaskRefID);
                dbParams.Add("@TransDate", model.TransDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@EmplRefID", model.EmplRefID);
                dbParams.Add("@LineID", model.LineID);
                dbParams.Add("@Notes", model.Notes);
                dbParams.Add("@JobType", model.JobType);
                dbParams.Add("@UserRefID", model.UserRefID);
                dbParams.Add("@DATAAREAID", model.DATAAREAID);

                var query = @"LAB_SaveProductTask";
                res = Task.FromResult(_services.ExcuteScaler<ProductionTask>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }

            return res;
        }

        public async Task<List<ProductionTask>> GetProductTasks(int RecID, string TaskRefID, string EmplRefID, string TaskID, string LineID)
        {
            var query = @"LAB_GetProductTasks";

            var lst = new List<ProductionTask>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@TaskRefID", TaskRefID);
                dbParams.Add("@EmplRefID", EmplRefID);
                dbParams.Add("@TaskID", TaskID);
                dbParams.Add("@LineID", LineID);

                lst = Task.FromResult(_services.GetAll<ProductionTask>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<List<ProductionTask>> GetProductTasksReports(string EmplRefID, DateTime? fromDate, DateTime? toDate)
        {
            var query = @"LAB_GetProductTasksReport";

            var lst = new List<ProductionTask>();

            try
            {
                var dbParams = new DynamicParameters();
                
                dbParams.Add("@EmplRefID", EmplRefID);
                dbParams.Add("@FromDate", fromDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@ToDate", toDate?.ToString("yyyy-MM-dd"));

                lst = Task.FromResult(_services.GetAll<ProductionTask>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<List<ProductionTask>> GetNotifyProductTasks(string EmplRefID)
        {
            var query = @"LAB_GetNotifyProductTasks";

            var lst = new List<ProductionTask>();

            try
            {
                var dbParams = new DynamicParameters();
                
                dbParams.Add("@EmplRefID", EmplRefID);

                lst = Task.FromResult(_services.GetAll<ProductionTask>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<ProdTable> GetProductOrder(string TRANSID,string CaseNo)
        {
            var query = @"LAB_GetProductOrder";

            var model = new ProdTable();

            try
            {
                var dbParams = new DynamicParameters();

                dbParams.Add("@INVENTTRANSID", TRANSID);
                dbParams.Add("@CaseNo", CaseNo);

                model = Task.FromResult(_services.Get<ProdTable>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<int> SaveTaskMessage(string TaskID, string FromUser, string ToUser, string MessageContent, string FilePath)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
               
                dbParams.Add("@TaskID", TaskID);
                dbParams.Add("@FromUserID", FromUser);                
                dbParams.Add("@ToUserID", ToUser);
                dbParams.Add("@MessageContent", MessageContent);
                dbParams.Add("@FilePath", FilePath);
               
                var query = @"LAB_SaveTaskMessage";
                res = Task.FromResult(_services.ExcuteScaler<TaskMessage>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }

            catch (Exception ex) { }

            return res;
        }

        public async Task<List<TaskMessage>> GetTaskMessages(int RecID, string TaskID)
        {
            var query = @"LAB_GetTaskMessages";

            var lst = new List<TaskMessage>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);               
                dbParams.Add("@TaskID", TaskID);

                lst = Task.FromResult(_services.GetAll<TaskMessage>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<ProductionTask> GetProductionTaskStatus(string TaskRefID, string EmplRefID)
        {
            var query = @"LAB_GetProductionTaskStatus";

            var model = new ProductionTask();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@TaskRefID", TaskRefID);
                dbParams.Add("@EmplRefID", EmplRefID);

                model = Task.FromResult(_services.Get<ProductionTask>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<int> SaveProductTaskTrans(string TaskID, int ProdStatus)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();

                dbParams.Add("@TaskID", TaskID);
                dbParams.Add("@ProdStatus", ProdStatus);
                dbParams.Add("@DATAAREAID", _services.DATAAREAID());
              
                var query = @"LAB_SaveProductTaskTrans";
                res = Task.FromResult(_services.ExcuteScaler<TaskMessage>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }

            catch (Exception ex) { }

            return res;
        }

        public async Task<List<TaskMessage>> GetTaskMessagesNotify(string UserID)
        {
            var query = @"LAB_GetTaskMessagesNotify";

            var lst = new List<TaskMessage>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@UserID", UserID);                

                lst = Task.FromResult(_services.GetAll<TaskMessage>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<int> DeleteProductTask(ProductionTask model)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM LAB_ProductAssignTask WHERE RecID=@RecID";
                
                dbParams.Add("@RecID", model.RecID);
                
                res = Task.FromResult(_services.ExcuteScaler<ProductionTask>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<List<ProductionTask>> GetLatestProductTasks()
        {
            var query = @"LAB_GetLatestProductTasks";

            var lst = new List<ProductionTask>();

            try
            {
                var dbParams = new DynamicParameters();

                //dbParams.Add("@EmplRefID", EmplRefID);

                lst = Task.FromResult(_services.GetAll<ProductionTask>(query, null, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<List<CaseTracking>> GetCasesTracking(int RecID, DateTime? FromDate, DateTime? ToDate,string Search,string LabID)
        {
            var query = @"LAB_GetCasesTracking";

            var lst = new List<CaseTracking>();

            try
            {
                var dbParams = new DynamicParameters();

                dbParams.Add("@RecID", RecID);
                dbParams.Add("@FromDate", FromDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@ToDate", ToDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@Search", Search);
                dbParams.Add("@LabID", LabID);

                lst = Task.FromResult(_services.GetAll<CaseTracking>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<CaseTracking> SaveCaseTracking(CaseTracking model)
        {
            try
            {
                var dbParams = new DynamicParameters();
                
                dbParams.Add("@RecID", model.RecID);
                dbParams.Add("@SalesID", model.SalesID);
                dbParams.Add("@CaseNo", model.CaseNo);
                dbParams.Add("@LabNum", model.LabNum);
                dbParams.Add("@PatientName", model.PatientName);
                dbParams.Add("@TransDate", model.TransDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@RushCategory", model.RushCategory);                
                dbParams.Add("@UserID", model.UserID);
                dbParams.Add("@LabID", model.LabID);

                var query = @"LAB_SaveCaseTracking";
                var res = Task.FromResult(_services.ExcuteScalerObject<CaseTracking>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;

                if (res != null)
                    model.RecID = Int32.Parse(res.ToString());
            }
            catch (Exception ex) { }

            return model;
        }

        public async Task<int> DeleteCaseTracking(int RecID)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM LAB_CasesTracking WHERE RecID=@RecID";

                dbParams.Add("@RecID", RecID);

                res = Task.FromResult(_services.ExcuteScaler<CaseTracking>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<List<CaseTrackingTask>> GetCasesTrackingTask(int RecID, string TransID)
        {
            var query = @"LAB_GetCaseTrackingTasks";

            var lst = new List<CaseTrackingTask>();

            try
            {
                var dbParams = new DynamicParameters();

                dbParams.Add("@RecID", RecID);
                dbParams.Add("@TransID", TransID);

                lst = Task.FromResult(_services.GetAll<CaseTrackingTask>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<CaseTrackingTask> SaveCaseTrackingTask(CaseTrackingTask model)
        {
            try
            {
                var dbParams = new DynamicParameters();

                dbParams.Add("@RecID", model.RecID);
                dbParams.Add("@TransID", model.TransID);
                dbParams.Add("@LineID", model.LineID);
                dbParams.Add("@TransDate", model.TransDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@Responsibility", model.Responsibility);                
                dbParams.Add("@LocationNotes", model.LocationNotes);                
                dbParams.Add("@UserID", model.UserID);
                dbParams.Add("@LineStatus", model.LineStatus);

                var query = @"LAB_SaveCaseTrackingTasks";
                var res = Task.FromResult(_services.ExcuteScalerObject<CaseTrackingTask>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;

                if (res != null)
                    model.RecID = Int32.Parse(res.ToString());
            }
            catch (Exception ex) { }

            return model;
        }

        public async Task<int> DeleteCaseTrackingTask(int RecID)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM LAB_CasesTrackingTasks WHERE RecID=@RecID";

                dbParams.Add("@RecID", RecID);

                res = Task.FromResult(_services.ExcuteScaler<CaseTrackingTask>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<List<CaseCommunicate>> GetCaseCommunicate(int RecID, string SalesID, string TransID)
        {
            var query = @"LAB_GetCaseCommunicate";

            var lst = new List<CaseCommunicate>();

            try
            {
                var dbParams = new DynamicParameters();

                dbParams.Add("@RecID", RecID);
                dbParams.Add("@SalesID", SalesID);
                dbParams.Add("@TransID", TransID);

                lst = Task.FromResult(_services.GetAll<CaseCommunicate>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<CaseCommunicate> SaveCaseCommunicate(CaseCommunicate model)
        {
            try
            {
                var dbParams = new DynamicParameters();

                dbParams.Add("@RecID", model.RecID);
                dbParams.Add("@SalesID", model.SalesID);
                dbParams.Add("@TransID", model.TransID);
                dbParams.Add("@ConcernIssue", model.ConcernIssue);
                dbParams.Add("@TechnicianSuggestion", model.TechnicianSuggestion);
                dbParams.Add("@Response", model.Response);
                dbParams.Add("@ResponseDate", model.ResponseDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@LabStatus", model.LabStatus);
                dbParams.Add("@FactoryStatus", model.FactoryStatus);
                dbParams.Add("@UserID", model.UserID);
                
                var query = @"LAB_SaveCaseCommunicate";
                var res = Task.FromResult(_services.ExcuteScalerObject<CaseCommunicate>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;

                if (res != null)
                {
                    if(model.RecID==0)
                    model.RecID = Int32.Parse(res.ToString());
                }
                    
            }
            catch (Exception ex) { }

            return model;
        }

        public async Task<int> DeleteCaseCommunicate(int RecID)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM LAB_CasesCommunicate WHERE RecID=@RecID";

                dbParams.Add("@RecID", RecID);

                res = Task.FromResult(_services.ExcuteScaler<CaseCommunicate>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<List<CaseStatus>> GetCaseStatus(int StatusGroup)
        {
            var query = @"Select * FROM LAB_CaseTrackingStatus WHERE StatusGroup=@StatusGroup";

            var lst = new List<CaseStatus>();

            try
            {
                var dbParams = new DynamicParameters();
                
                dbParams.Add("@StatusGroup", StatusGroup);
                lst = Task.FromResult(_services.GetAll<CaseStatus>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<CaseTracking> GetCasesTrackingByID(int RecID)
        {
            var query = @"LAB_GetCasesTrackingByID";
            var model = new CaseTracking();
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);

                model = Task.FromResult(_services.Get<CaseTracking>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<CaseTracking> GetCasesTrackingBySalesID(string SalesID)
        {
            var query = @"LAB_GetCasesTrackingBySalesID";
            var model = new CaseTracking();
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@SalesID", SalesID);

                model = Task.FromResult(_services.Get<CaseTracking>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<int> SaveCaseResponse(CaseResponse response)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = @"INSERT INTO LAB_CasesResponse(TransRefID,Response,ResponseDate,LabStatus,UserID) 
                                    values(@TransRefID,@Response,@ResponseDate,@LabStatus,@UserID)";

                dbParams.Add("@TransRefID", response.TransRefID);
                dbParams.Add("@Response", response.Response);
                dbParams.Add("@ResponseDate", response.ResponseDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@LabStatus", response.LabStatus);
                dbParams.Add("@UserID", response.UserID);

                res = Task.FromResult(_services.ExcuteScaler<CaseResponse>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<List<CaseResponse>> GetCaseResponses(string TransID)
        {
            var query = @"Select * FROM LAB_CasesResponse WHERE TransRefID=@TransID";
            var lst = new List<CaseResponse>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@TransID", TransID);

                lst = Task.FromResult(_services.GetAll<CaseResponse>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<int> DeleteResponse(CaseResponse response, string UserID)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM LAB_CasesResponse WHERE RecID=@RecID And UserID=@UserID";

                dbParams.Add("@RecID", response.RecID);
                dbParams.Add("@UserID", UserID);

                res = Task.FromResult(_services.ExcuteScaler<CaseResponse>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }
    }
}
