using Azure.Messaging;
using Dapper;
using LabManagement.Infrastructure.IRespository;
using LabManagement.Models;
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
                query += " AND DATAAREAID=@DATAAREAID";

                dbParams.Add("@RecID", RecID);
                dbParams.Add("@DATAAREAID", _services!.DATAAREAID());

                res = Task.FromResult(_services.ExcuteScaler<ProductLine>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<List<ProductLine>> GetAllProdLine(int RecID, string LineID, string Search)
        {

            var query = @"Select * FROM LAB_ProductLines where DATAAREAID=@DATAAREAID";
            query += " AND (@RecID=0 OR RecID=@RecID)";
            query += " AND (@LineID='' OR LineID=@LineID)";
            query += " AND (@Search='' OR LineName=@Search)";

            var lst = new List<ProductLine>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@LineID", LineID);
                dbParams.Add("@Search", Search);
                dbParams.Add("@DATAAREAID", _services!.DATAAREAID());

                lst = Task.FromResult(_services.GetAll<ProductLine>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<ProductLine> GetProdLine(int RecID)
        {
            var query = @"Select * FROM LAB_ProductLines where RecID=@RecID";
            query += " AND DATAAREAID=@DATAAREAID";
            var model = new ProductLine();
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@DATAAREAID", _services!.DATAAREAID());

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
                dbParams.Add("@TransDate", DateTime.Now.ToString("yyyy-MM-dd"));
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
    }
}
